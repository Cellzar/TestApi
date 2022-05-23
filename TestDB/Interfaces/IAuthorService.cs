using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Context;

namespace TestDB.Interfaces
{
    public interface IAuthorService
    {
        List<Author> GetAuthors();

        Task<Author> GetAuthor(int id);

        Task InsertAuthor(Author autor);
        bool InsertAllAuthor(DataTable tbl);

        Task<bool> UpdateAuthor(Author autor);

        Task<bool> DeleteAuthor(int id);
    }
}
