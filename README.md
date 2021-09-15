# Product API

Product API is for retrieving, creating and updating products.

## Installation

Clone the repository, build and run through IISExpress in Visual Studio

## Requests

### Create a product

HttpMethod: POST

URL: https://localhost:44397/product

Headers: 
- Content-Type: application/json

Request body:
```json
{
    "id": [int],
    "name": "[string]"
}
```

Response codes:
- 201
- 400

Example request: https://localhost:44397/product

Body:
```json
{
    "id": 20,
    "name": "Blueberry"
}
```

Example response:
```json
{
    "id": 20,
    "name": "Blueberry"
}
```

### Get product by id

HttpMethod: GET

URL: https://localhost:44397/product/[int] (IISExpress default)

Response codes:
- 200
- 400
- 404

Example request: https://localhost:44397/product/1

Example response: 
```json
{
    "id": 1,
    "name": "Apple"
}
```

### List products by name

HttpMethod: GET

URL: https://localhost:44397/product/[string]?page=[int?]&pageSize=[int?] (if page and pageSize are not specified, the default values: 1, 10 are applied)

Response codes:
- 200
- 400
- 404

Example request: https://localhost:44397/product/Apple?page=1&pageSize=10

Example response: 
```json
{
    "results": [
        {
            "id": 1,
            "name": "Apple"
        }
    ],
    "page": 1,
    "pageSize": 10,
    "totalPages": 1,
    "totalResults": 1
}
```

### Update a product

HttpMethod: PUT

URL: https://localhost:44397/product

Headers: 
- Content-Type: application/json

Request body:
```json
{
    "id": [int],
    "name": "[string]"
}
```

Response codes:
- 200
- 201
- 400

Example request: https://localhost:44397/product

Body:
```json
{
    "id": 1,
    "name": "Peach"
}
```

Example response: empty for update, json object for creation if the object (by id) does not exist

## Mock data
```json
[
  {
    "id": 1,
    "name": "Apple"
  },
  {
    "id": 2,
    "name": "Pear"
  },
  {
    "id": 3,
    "name": "Orange"
  },
  {
    "id": 4,
    "name": "Orange"
  },
  {
    "id": 5,
    "name": "Watermelon"
  },
  {
    "id": 6,
    "name": "Pear"
  },
  {
    "id": 7,
    "name": "Orange"
  },
  {
    "id": 8,
    "name": "Orange"
  },
  {
    "id": 9,
    "name": "Orange"
  },
  {
    "id": 10,
    "name": "Orange"
  },
  {
    "id": 11,
    "name": "Orange"
  },
  {
    "id": 12,
    "name": "Orange"
  },
  {
    "id": 13,
    "name": "Orange"
  },
  {
    "id": 14,
    "name": "Orange"
  },
  {
    "id": 15,
    "name": "Orange"
  },
  {
    "id": 16,
    "name": "Orange"
  },
  {
    "id": 17,
    "name": "Orange"
  },
  {
    "id": 18,
    "name": "Orange"
  }
]
```
