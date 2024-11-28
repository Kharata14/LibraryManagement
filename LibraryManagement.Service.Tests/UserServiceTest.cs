using LibraryManagementService.Models;
using LibraryManagementService.Services;

namespace LibraryManagement.Service.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact (DisplayName ="მომხმარებელი უნდა დაემატოს ლისთში თუ ის ვალიდურია")]
        public async Task RegisterUser_ShouldAddUserToList_WhenUserIsValid()
        {
            // Arrange
            var user = new User
            {
                Id = new Guid(),
                Name = "John Doe",
                PersonalNumber = "12345678901",
                Email = "john.doe@example.com"
            };

            // Act
            await _userService.RegisterUser(user);
            var registeredUser = await _userService.FindUserById(user.Id);

            // Assert
            Assert.NotNull(registeredUser);
            Assert.Equal(user.Name, registeredUser.Name);
            Assert.Equal(user.PersonalNumber, registeredUser.PersonalNumber);
            Assert.Equal(user.Email, registeredUser.Email);
        }

        [Fact(DisplayName ="უნდა გავარდეს შეცდომა თუ მომხმარებლის სახელი არასწორია")]
        public async Task RegisterUser_ShouldThrowArgumentException_WhenNameIsInvalid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "",
                PersonalNumber = "12345678901",
                Email = "john.doe@example.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterUser(user));
        }

        [Fact(DisplayName ="უნდა გავარდეს შეცდომა თუ მომხმარებლის პირადი ნომერი არასწორია")]
        public async Task RegisterUser_ShouldThrowArgumentException_WhenPersonalNumberIsInvalid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PersonalNumber = "12345678",
                Email = "john.doe@example.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterUser(user));
        }

        [Fact(DisplayName ="უნდა გავარდეს შეცდომა თუ მომხმარებლის იმეილი არასწორია")]
        public async Task RegisterUser_ShouldThrowArgumentException_WhenEmailIsInvalid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PersonalNumber = "12345678901",
                Email = "invalid-email"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterUser(user));
        }

        [Fact(DisplayName ="მომხმარებელი უნდა წაიშალოს თუ კი ის არსებობს")]
        public async Task DeleteUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                PersonalNumber = "12345678902",
                Email = "jane.doe@example.com"
            };
            await _userService.RegisterUser(user);

            // Act
            await _userService.DeleteUser(user.Id);
            var deletedUser = await _userService.FindUserById(user.Id);

            // Assert
            Assert.Null(deletedUser);
        }

        [Fact(DisplayName ="წაშლის ბრძანებამ არ უნდა გამოიწვიოს შეცდომა თუ მომხმარებელი არ არსებობს")]
        public async Task DeleteUser_ShouldNotThrowException_WhenUserDoesNotExist()
        {
            // Act & Assert
            await _userService.DeleteUser(Guid.NewGuid());
        }

        [Fact(DisplayName ="ფუნქციამ უნდა იპოვოს მომხმარებელი და დააბრუნოს თუ ის არსებობს")]
        public async Task FindUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Alice Smith",
                PersonalNumber = "12345678903",
                Email = "alice.smith@example.com"
            };
            await _userService.RegisterUser(user);

            // Act
            var foundUser = await _userService.FindUserById(user.Id);

            // Assert
            Assert.NotNull(foundUser);
            Assert.Equal(user.Id, foundUser.Id);
        }

        [Fact(DisplayName ="ფუნქციამ უნდა დააბრუნოს Null თუ მომხმარებელი არ არსებობს")]
        public async Task FindUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var user = await _userService.FindUserById(Guid.NewGuid());

            // Assert
            Assert.Null(user);
        }
    }
}