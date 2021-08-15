
## C# Web API With My SQL Book Service Rest API
This is a rest api project using c# .net Framework and Mysql as backend database.

## APIs
The project has two Web apis: 

### 1. /api/authors
Examples: 
POST:/api/authors,  body : { "Name": "Jane Austen }
GET: /api/authors, Response: [
    {
        "Id": 1,
        "Name": "Jane Austen"
    },
    {
        "Id": 2,
        "Name": "Charles Dickens"
    },
    {
        "Id": 3,
        "Name": "Miguel de Cervantes"
    }
]
Similar for PUT and Delete

### 2. /api/books
Examples: 
POST: body : {
    "Title": "Don Quixote",
    "Year": 1617,
    "Price": 8.95,
    "AuthorId": 3,
    "Genre": "Picaresque"
}

GET: Response: [
    {
        "Id": 1,
        "Title": "Pride and Prejudice",
        "Year": 1813,
        "Price": 9.99,
        "Genre": "Comedy of manners",
        "AuthorName": "Jane Austen"
    },
    {
        "Id": 2,
        "Title": "Northanger Abbey",
        "Year": 1817,
        "Price": 12.95,
        "Genre": "Gothic parody",
        "AuthorName": "Jane Austen"
    },
    {
        "Id": 3,
        "Title": "David Copperfield",
        "Year": 1850,
        "Price": 15.0,
        "Genre": "Bildungsroman",
        "AuthorName": "Charles Dickens"
    },
    {
        "Id": 4,
        "Title": "Don Quixote",
        "Year": 1617,
        "Price": 8.95,
        "Genre": "Picaresque",
        "AuthorName": "Miguel de Cervantes"
    }
]

## Logging
Logs are created using NLog in folder "MyLogs"

## Cloud
The whole architecture is deployed on Azure