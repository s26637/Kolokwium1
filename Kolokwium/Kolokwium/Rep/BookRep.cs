using Kolokwium.Models;
using Microsoft.Data.SqlClient;

namespace Kolokwium.Rep;


public class BookRep : IBookRep
{
	
	
    private readonly IConfiguration _configuration;
    public BookRep(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    

    public async Task<Book> GetBook(int id)
    {
	    var query = @"SELECT 
							PK,
							title,
						FROM Book
						WHERE PK = @ID";
	    
	    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
	    await using SqlCommand command = new SqlCommand();

	    command.Connection = connection;
	    command.CommandText = query;
	    command.Parameters.AddWithValue("@ID", id);
	    
	    await connection.OpenAsync();

	    var reader = await command.ExecuteReaderAsync();

	    var bookIdOrdinal = reader.GetOrdinal("PK");
	    var bookTitleOrdinal = reader.GetOrdinal("title");
	    
	    Book book = null;


	    book = new Book()
	    {
		    Id = reader.GetInt32(bookIdOrdinal),
		    Title = reader.GetString(bookTitleOrdinal),
	
				    
	    };
	
	    if (book is null) throw new Exception();
        
        return book;
    }
    
    public async Task<int> AddBook(Book book)
    {
	    var insert = @"INSERT INTO Book VALUES(@title);
					   SELECT @@IDENTITY AS ID;";
	    
	    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
	    await using SqlCommand command = new SqlCommand();
	    
	    command.Connection = connection;
	    command.CommandText = insert;
	    
	    command.Parameters.AddWithValue("@Tittle", book.Title);
	    
	    await connection.OpenAsync();
	    
	    var id = await command.ExecuteScalarAsync();

	    if (id is null) throw new Exception();
	    
	    return Convert.ToInt32(id);
    }
    
    
    

}