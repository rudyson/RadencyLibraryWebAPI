# RadencyLibraryWebAPI
> [ASP.NET Web API for library frontend on Angular](https://github.com/rudyson/RadencyLibraryFrontend)
### 1. Clone project
```
git clone https://github.com/rudyson/RadencyLibraryWebAPI.git
```

### 2. Locate project directory
```
cd ./RadencyLibraryWebAPI
```

### 3. Create configuration file ".env":
- %CONNECTIONSTRING% is a connection string to MS SQL Server
- %SECRET% is a secred which also has frontend to HTTP DELETE confirmation
```
MSSQLCONNECTIONSTRING=%CONNECTIONSTRING%
HTTPDELETE_SECRET=%SECRET%
```

### 4. Start project
```
dotnet run
```

### 5. Test ASP.NET Web API via Swagger
```
https://localhost:5000/swagger/index.html
```

### Web API contains following requests
|Http request code|Url|Description|
| ------- | ------- | ------- |
|GET|https://{{baseUrl}}/api/books?order=author|List of books sorted by order|
|GET|https://{{baseUrl}}/api/recommended?genre=horror|Recommendations to read (10+ reviews, 10 books)|
|GET|https://{{baseUrl}}/api/books/{id}|Get book by id|
|DELETE|https://{{baseUrl}}/api/books/{id}?secret=qwerty|Safe delete of book, its reviews and rating|
|POST|https://{{baseUrl}}/api/books/save|Updates book or saves if id not exists|
|PUT|https://{{baseUrl}}/api/books/{id}/review|Set your review of book|
|PUT|https://{{baseUrl}}/api/books/{id}/rate|Set your score of book|
