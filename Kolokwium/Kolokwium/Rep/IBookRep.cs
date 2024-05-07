using Kolokwium.Models;

namespace Kolokwium.Rep;



public interface IBookRep
{

    Task<Book> GetBook(int id);

    Task<int> AddBook(Book book);

}
