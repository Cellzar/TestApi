using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Context;
using TestDB.Interfaces;

namespace TestDB.Services
{
    public class AuthorService: IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TestContext _context;

        public AuthorService(IUnitOfWork unitOfWork, TestContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _unitOfWork.AutorRepository.GetById(id);
        }

        public List<Author> GetAuthors()
        {
            var authors = _unitOfWork.AutorRepository.GetAll();
            List<Author> result = new List<Author>();
            foreach (var autor in authors)
            {
                result.Add(autor);
            }
            return result;
        }

        public async Task InsertAuthor(Author autor)
        {
            await _unitOfWork.AutorRepository.Add(autor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateAuthor(Author autor)
        {
            var existingAutor = await _unitOfWork.AutorRepository.GetById(autor.Id);
            existingAutor.IdBook = autor.IdBook;
            existingAutor.FirstName = autor.FirstName;
            existingAutor.LastName = autor.LastName;

            _unitOfWork.AutorRepository.Update(existingAutor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            await _unitOfWork.AutorRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public bool InsertAllAuthor(DataTable tbl)
        {
            return _context.SaveAllAuthors(tbl);
        }
    }
}
