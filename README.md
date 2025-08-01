## Environment:
- .NET version: 6.0

## Read-Only Files:
- BooksService.Tests/IntegrationTests.cs
- BooksService.WebAPI/Controllers/BooksController.cs

## Data:
Example of a book data JSON object:
```
{
   "title": "Initial Professional Development for Civil Engineers",
   "author": "Patrick Waterhouse",
   "publicationDate": "2017-09-08T19:04:14.480Z"
}
```

## Requirements:

A company is launching a service that can validate a book model. The service should be a web API layer using .NET. You already have a prepared infrastructure and need to implement validation logic for the book model in "Models/Book.cs" as per the guidelines below. Perform validation in models, not in controllers. The controller file "Controllers/BooksController.cs" is read-only.

Each book data is a JSON object describing the details of the book. Each object has the following properties:

- title: the title of the book. [String]
- author: the author of the book. [String]
- publicationDate: the publication date of the book. [DateTime]

The following API needs to be implemented:

`POST` request to `api/books`:

- The HTTP response code should be 200 on success.
- For the body of the request, please use the JSON example of the book model given above.
- If the book model is invalid, return status code 400. When you send 400, add an appropriate error message to the response as described below.

The book model should be validated based on the following rules:

- The title field is required and must contain a minimum of 5 characters and a maximum of 255, and the first letter should be in upper case. If the field is invalid, add this error message: `"Title is invalid: Title must contain a minimum of 5 characters and a maximum of 255, and the first letter should be in upper case"`.
- The author field is required and must contain a minimum of 3 characters and a maximum of 30, and the first letter should be in upper case. If the field is invalid, add this error message: `"Author is invalid: Author must contain a minimum of 3 characters and a maximum of 30, and the first letter should be in upper case"`.
- The publicationDate field is required and must be after 01/01/1900 and before the current date. If the field is invalid, add this error message: `"PublicationDate is invalid: PublicationDate must be after 01/01/1900 and before the current date"`.
