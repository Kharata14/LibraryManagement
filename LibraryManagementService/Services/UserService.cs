using LibraryManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;


namespace LibraryManagementService.Services;

public class UserService : IUserService
{
    private List<User> _users = new List<User>();
    
    private bool IsValidEmail(string email)
    {
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex);
    }

    public async Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Name) || user.Name.Length > 100)
        {
            throw new ArgumentException("The name must be less than 100 characters.");
        }
        if (string.IsNullOrEmpty(user.PersonalNumber) || user.PersonalNumber.Length != 11 || !user.PersonalNumber.All(char.IsDigit))
        {
            throw new ArgumentException("Personal number must contain 11 digits.");
        }
        if (_users.Any(u => u.PersonalNumber == user.PersonalNumber))
        {
            throw new ArgumentException("Persolan number must be unique");
        }
        if (string.IsNullOrEmpty(user.Email) || user.Email.Length > 100 || !IsValidEmail(user.Email))
        {
            throw new ArgumentException("Email should not be empty or too long");
        }
        if (_users.Any(u => u.Email == user.Email))
        {
            throw new ArgumentException("ელფოსტა უნდა იყოს უნიკალური");
        }

        _users.Add(user);
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await FindUserById(id);

        if (user != null)
        {
            _users.Remove(user);
        }
    }

    public async Task<User> FindUserById(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}

