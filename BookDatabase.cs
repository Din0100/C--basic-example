using System;
using System.Collections.Generic;
using System.Linq;

public class BookDatabase {

    private static BookDatabase db_instance = null;
    private BookDatabase(){

    }
    public static BookDatabase Db_instance{
        get {
            if (db_instance == null){
                db_instance = new BookDatabase();
                db_instance.books.Add(new Book(100,"Title","Author"));
                db_instance.books.Add(new Book(101,"Title1","Author1"));
                db_instance.books.Add(new Book(102,"Title2","Author2"));
            }

            return db_instance;
        }
    }
    private List<Book> books = new List<Book>();

    public List<Book> getBooks(){
        return books;
    }

    public Book getBookById(string _id){
        int id = Int32.Parse(_id);
        Book _book = books.FirstOrDefault(book => book.ID == id);
        return _book;
    }

    public bool addBook(Book book){
        try{
            books.Add(new Book(book.ID, book.Name, book.Author));
            return true;
        } catch {
            return false;
        }
    }

    public bool updateBook(Book _book){
        int id = _book.ID;
        bool exists = books.Any(book => book.ID == id);
        if (exists){
            int index = books.FindIndex(book => book.ID == id);
            if (index != -1){
                books[index] = _book;
                return true;
            }
        }
        return false;
    }

    public void deleteBookById(string _id){
        int id = Int32.Parse(_id);
        books.RemoveAll(book => book.ID == id);
    }
}