using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Context;

namespace TestDB.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Book> BookRepository { get; }
        IRepository<Author> AutorRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
