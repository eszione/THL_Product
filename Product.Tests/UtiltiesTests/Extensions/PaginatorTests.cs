using Product.Types.Constants;
using Product.Types.Models;
using Product.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product.Tests.UtiltiesTests.Extensions
{
    public class PaginatorTests
    {
        [Fact]
        public void Given_small_list_when_zero_value_parameters_then_return_default()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var expected = new PagedResults<int>
            {
                Results = list,
                Page = Integers.MIN_PAGE_NUMBER,
                PageSize = Integers.MIN_PAGE_SIZE,
                TotalPages = 1
            };

            var actual = list.Paginate(0, 0);

            Assert.Equal(expected.TotalResults, actual.TotalResults);
        }

        [Fact]
        public void Given_small_list_when_negative_value_parameters_then_return_default()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var expected = new PagedResults<int>
            {
                Results = list,
                Page = Integers.MIN_PAGE_NUMBER,
                PageSize = Integers.MIN_PAGE_SIZE,
                TotalPages = 1
            };

            var actual = list.Paginate(-1, -1);

            Assert.Equal(expected.TotalResults, actual.TotalResults);
        }

        [Fact]
        public void Given_small_list_when_large_pagesize_parameter_then_return_default()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            var expected = new PagedResults<int>
            {
                Results = list,
                Page = 1,
                PageSize = 15,
                TotalPages = 1
            };

            var actual = list.Paginate(1, 15);

            Assert.Equal(expected.TotalResults, actual.TotalResults);
            Assert.Equal(expected.Page, actual.Page);
            Assert.Equal(expected.PageSize, actual.PageSize);
        }

        [Fact]
        public void Given_large_list_when_large_page_two_parameter_then_return_page_two()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var expected = new PagedResults<int>
            {
                Results = list.Skip(10).Take(10),
                Page = 2,
                PageSize = 10,
                TotalPages = 2
            };

            var actual = list.Paginate(2, 10);

            Assert.Equal(expected.TotalResults, actual.TotalResults);
            Assert.Equal(expected.Page, actual.Page);
            Assert.Equal(expected.PageSize, actual.PageSize);
        }

        [Fact]
        public void Given_large_list_when_page_two_and_large_pagesize_parameters_then_return_no_results()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var expected = new PagedResults<int>
            {
                Results = list,
                Page = Integers.MIN_PAGE_NUMBER,
                PageSize = list.Count,
                TotalPages = Integers.MIN_PAGE_NUMBER
            };

            var actual = list.Paginate(2, 20);

            Assert.Equal(expected.TotalResults, actual.TotalResults);
            Assert.Equal(expected.Page, actual.Page);
            Assert.Equal(expected.PageSize, actual.PageSize);
        }
    }
}
