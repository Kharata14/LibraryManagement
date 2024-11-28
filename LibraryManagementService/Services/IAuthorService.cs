using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Services
{
    public interface IAuthorService
    {
        Task RegisterAuthor(AuthorCreateModel author);
        Task DeleteAuthor(Guid id);
        Task UpdateAuthor(AuthorUpdateModel author);
    }
}
