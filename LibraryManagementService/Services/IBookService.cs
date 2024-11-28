using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Services
{
    public interface IBookService
    {
        Task RegisterBook(BookRegisterModel book);
        Task UpdateBook(BookUpdateModel book);
        Task DeleteBook(Guid id);
        Task TakeBook(Guid bookId, Guid userId);
        Task ReturnBook(Guid bookId, Guid userId);

    }
}
