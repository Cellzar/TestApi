using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Context;

namespace TestDB.Interfaces
{
    public interface IBookService
    {
        List<Book> GetBooks();

        Task<Book> GetBook(int id);

        Task InsertBook(Book book);
        bool InsertAllBook(DataTable tbl);

        Task<bool> UpdateBook(Book post);

        Task<bool> DeleteBook(int id);
    }
}
