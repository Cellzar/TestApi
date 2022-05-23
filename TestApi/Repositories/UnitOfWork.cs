using System.Threading.Tasks;
using TestDB.Context;
using TestDB.Interfaces;

namespace TestApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestContext _context;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _autorRepository;

        public UnitOfWork(TestContext context)
        {
            _context = context;
        }

        public IRepository<Book> BookRepository => _bookRepository ?? new BaseRepository<Book>(_context);

        public IRepository<Author> AutorRepository => _autorRepository ?? new BaseRepository<Author>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
