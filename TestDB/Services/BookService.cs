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
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TestContext _context;

        public BookService(IUnitOfWork unitOfWork, TestContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Book> GetBook(int id)
        {
            return await _unitOfWork.BookRepository.GetById(id);
        }

        public List<Book> GetBooks()
        {
            var books = _unitOfWork.BookRepository.GetAll();

            List<Book> booksList = new List<Book>();
            foreach (Book book in books)
            {
                booksList.Add(book);
            }    

            return booksList;
        }

        public async Task InsertBook(Book book)
        {
            await _unitOfWork.BookRepository.Add(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var existingBook = await _unitOfWork.BookRepository.GetById(book.Id);
            existingBook.Description = book.Description;
            existingBook.Title = book.Title;
            existingBook.Excerpt = book.Excerpt;
            existingBook.PublicDate = book.PublicDate;
            existingBook.PageCount = book.PageCount;

            _unitOfWork.BookRepository.Update(existingBook);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            await _unitOfWork.BookRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public bool InsertAllBook(DataTable tbl)
        {
            return _context.SaveAllBooks(tbl);
        }
    }
}
