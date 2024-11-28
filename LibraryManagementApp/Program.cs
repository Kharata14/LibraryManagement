


using LibraryManagementService.Models;
using LibraryManagementService.Services;


    var userService = new UserService();

    var newUser = new User
    {
        Id = Guid.NewGuid(),
        Name = "John Doe",
        PersonalNumber = "12345678901",
        Email = "john.doe@example.com"
    };

    await userService.RegisterUser(newUser);

    var registeredUser = await userService.FindUserById(newUser.Id);


    if (registeredUser != null)
    {
        Console.WriteLine("User registered successfully!");
        Console.WriteLine($"Name: {registeredUser.Name}");
        Console.WriteLine($"Personal Number: {registeredUser.PersonalNumber}");
        Console.WriteLine($"Email: {registeredUser.Email}");
    }
    else
    {
        Console.WriteLine("User not found.");
    }
