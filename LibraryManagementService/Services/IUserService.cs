using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Services
{
    public interface IUserService
    {
        public Task RegisterUser(User user);
        public Task<User> FindUserById(Guid id);
        public Task DeleteUser(Guid id);

    }
}
