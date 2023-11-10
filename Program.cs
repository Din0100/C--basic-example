using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/books", (HttpContext context) => {
    string _queryId = context.Request.Query["id"];
    if (_queryId != null){
        Book book = BookDatabase.Db_instance.getBookById(_queryId);
        if (book != null) {
            context.Response.WriteAsJsonAsync(book);
        }
        else {
            context.Response.StatusCode = 404;
            context.Response.WriteAsync("Book not found");
        }
    }
    else {
        List<Book> books = BookDatabase.Db_instance.getBooks();
        context.Response.WriteAsJsonAsync(books);
    }
    return Task.CompletedTask;
});

app.MapPost("/addBook", (Book book, HttpContext context) => {
    bool success = BookDatabase.Db_instance.addBook(book);
    if (success){
        context.Response.WriteAsync("Book Added");
    } else {
        context.Response.WriteAsync("Could not add book");
    }
    return Task.CompletedTask;
});

app.MapDelete("/deleteBookById", (HttpContext context) => {
    string _queryId = context.Request.Query["id"];
    if (_queryId != null){
        BookDatabase.Db_instance.deleteBookById(_queryId);
        context.Response.WriteAsync("Book Added");
    } else {
        context.Response.StatusCode = 404;
        context.Response.WriteAsync("Book not in database");
    }
    return Task.CompletedTask;
});

app.MapPut("/book", (Book book, HttpContext context) => {
    bool success = BookDatabase.Db_instance.updateBook(book);
    if (success){
        context.Response.WriteAsync("Book updated");
    } else {
        context.Response.WriteAsync("Book not in database");
    }
    return Task.CompletedTask;
});


app.Run();
