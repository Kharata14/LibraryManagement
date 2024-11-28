using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Services
{
    public class BookService : IBookService
    {
        private static List<Book> _books = new List<Book>();
        private readonly AuthorService _authorService;
        private UserService _userService;

        public BookService(AuthorService authorService, UserService userService)
        {
            _authorService = authorService;
            _userService = userService;

        }


        public async Task RegisterBook(BookRegisterModel regBookModel)
        {

            if (string.IsNullOrWhiteSpace(regBookModel.Name) || regBookModel.Name.Length > 250)
            {
                throw new ArgumentException("The name is required and must be between 1 and 250 characters.");
            }

            if (!DateTime.TryParse(regBookModel.PublishTime, out DateTime publishTime))
            {
                throw new ArgumentException("The publication date is in the wrong format.");
            }

            if (publishTime > DateTime.Now)
            {
                throw new ArgumentException("The publication date must not exceed the current date.");
            }

            var findAuthor = _authorService.GetAuthors().Any(a => a.Id == regBookModel.Author.Id);
            if (!findAuthor)
            {
                throw new ArgumentException("The specified author could not be found in the directory.");
            }

            if (regBookModel.PageCount < 1)
            {
                throw new ArgumentException("The number of pages must be at least 1.");
            }

            if (!string.IsNullOrWhiteSpace(regBookModel.Description) &&
                (regBookModel.Description.Length < 1 || regBookModel.Description.Length > 1000))
            {
                throw new ArgumentException("Description must be between 1 and 1000 characters, or empty.");
            }


            if (regBookModel.BookCount < 0)
            {
                throw new ArgumentException("The number must not be negative.");
            }

            var book = new Book()
            {
                Id = new Guid(),
                Name = regBookModel.Name,
                PublishTime = DateTime.Parse(regBookModel.PublishTime),
                Author = regBookModel.Author,
                PageCount = regBookModel.PageCount,
                Description = regBookModel.Description,
                BookCount = regBookModel.BookCount
            };
            _books.Add(book);
        }

        public async Task UpdateBook(BookUpdateModel updateBookModel)
        {
            var book = _books.FirstOrDefault(x => x.Id == updateBookModel.Id);
            if (book != null)
            {

                if (string.IsNullOrWhiteSpace(updateBookModel.Name) || updateBookModel.Name.Length < 1 || updateBookModel.Name.Length > 250)
                {
                    throw new ArgumentException("The name is required and must be between 1 and 250 characters.");
                }

                if (!DateTime.TryParse(updateBookModel.PublishTime, out DateTime publishTime))
                {
                    throw new ArgumentException("The publication date is in the wrong format.");
                }
                if (publishTime > DateTime.Now)
                {
                    throw new ArgumentException("The publication date must not exceed the current date.");
                }

                var authorExists = _authorService.GetAuthors().Any(a => a.Id == updateBookModel.Author.Id);
                if (!authorExists)
                {
                    throw new ArgumentException("The specified author could not be found in the directory.");
                }

                if (updateBookModel.PageCount < 1)
                {
                    throw new ArgumentException("The number of pages must be at least 1.");
                }

                if (!string.IsNullOrWhiteSpace(updateBookModel.Description) &&
                    (updateBookModel.Description.Length < 1 || updateBookModel.Description.Length > 1000))
                {
                    throw new ArgumentException("Description must be between 1 and 1000 characters, or empty.");
                }

                book.Name = updateBookModel.Name;
                book.PublishTime = DateTime.Parse(updateBookModel.PublishTime);
                book.Author = updateBookModel.Author;
                book.PageCount = updateBookModel.PageCount;
                book.Description = updateBookModel.Description;
            }
            else
            {
                throw new ArgumentException("Book not found.");
            }
        }
        public async Task DeleteBook(Guid id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                if (book.TakenBook.HasValue)
                {
                    throw new ArgumentException("Book cannot be deleted because it is currently taken out");
                }
                _books.Remove(book);
            }
        }

        public async Task TakeBook(Guid bookId, Guid userId)
        {
            var book = _books.FirstOrDefault(x => x.Id == bookId);
            var user = await _userService.FindUserById(userId);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            if (user.TakenBookHistory.Count >= 5)
            {
                throw new ArgumentException("User cannot take more than 5 books at a time.");
            }

            if (book.BookCount <= 0)
            {
                throw new ArgumentException("No available copies of the book.");
            }

            var takeBookRecord = new TakeBookRecord
            {
                Id = new Guid(),
                BookId = bookId,
                UserId = userId,
                TakeDate = DateTime.Now
            };
            user.TakenBookHistory.Add(takeBookRecord);
            book.BookCount--;

        }

        public async Task ReturnBook(Guid bookId, Guid userId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }

            var user = await _userService.FindUserById(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var takenRecord = user.TakenBookHistory.FirstOrDefault(r => r.BookId == bookId && r.UserId == userId && r.ReturnDate == null);

            if (takenRecord == null)
            {
                throw new ArgumentException("No active record found for this book and user.");
            }

            takenRecord.ReturnDate = DateTime.Now;

            book.BookCount++;

        }
    }
}
