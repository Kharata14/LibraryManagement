using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryManagementService.Services
{
    public class AuthorService : IAuthorService
    {
        private static List<Author> _authors = new List<Author>();
        public List<Author> GetAuthors()
        {
            return _authors;
        }
        public async Task RegisterAuthor(AuthorCreateModel author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                throw new ArgumentException("Name is required");
            }
            if (author.Name.Length < 1 || author.Name.Length > 100)
            {
                throw new ArgumentException("Name should be min1 and max 100 symbols");
            }
            if (!DateTime.TryParse(author.Birthday, out DateTime birthday))
            {
                throw new ArgumentException("Wrong format");
            }
            if (birthday > DateTime.Now)
            {
                throw new ArgumentException("Birth year must not exceed the current date");
            }

            var auth = new Author()
            {
                Id = new Guid(),
                Name = author.Name,
                Biography = author.Biography,
                Birthday = DateTime.Parse(author.Birthday),
            };
            _authors.Add(auth);
        }

        public async Task DeleteAuthor(Guid id)
        {
            var auth = _authors.FirstOrDefault(x => x.Id == id);
            if (auth != null && !auth.Books.Any())
            {
                _authors.Remove(auth);
            }
        }

        public async Task UpdateAuthor(AuthorUpdateModel author)
        {
            var auth = _authors.FirstOrDefault(x => x.Id == author.Id);
            if (auth != null)
            {
                if (string.IsNullOrWhiteSpace(author.Name) || author.Name.Length > 100)
                {
                    throw new ArgumentException("The name is required and must be between 1 and 100 characters.");
                }
                if (!DateTime.TryParse(author.Birthday, out DateTime birthday))
                {
                    throw new ArgumentException("The birth year is in the wrong format.");
                }
                if (birthday > DateTime.Now)
                {
                    throw new ArgumentException("The year of birth must not exceed the current date.");
                }

                auth.Name = author.Name;
                auth.Birthday = birthday;
                auth.Biography = author.Biography;
            }
            else
            {
                throw new ArgumentException("Author is not found");
            }
        }

    }
}
