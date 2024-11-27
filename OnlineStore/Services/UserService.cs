/*using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace OnlineStore.Services
{
    public class UserService
    {
        private readonly List<User> users;

        public UserService()
        {
            users = new List<User>();
        }

        private bool isValidEmail(string input)
        {

            return input.Contains("@gmail.com");
        }


      
        public string RegisterUser(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
            {
                return "Username must have at least 1 and Max 100 charachter";
            }
            if(string.IsNullOrWhiteSpace(email) || email.Length > 100)
            {
                return "email must have at least 1 and Max 100 charachter";

            }
             
            if (string.IsNullOrWhiteSpace(password) || password.Length <8 || password.Length >16)
            {
                return "Password must be between 8 and 16 characters.";
            }
            foreach (var u in users)
            {
                if (u.Email == email)
                {
                    return "User with that email already exists";
                }
                else if (u.Name == username)
                {
                    return "User with that name already exists";
                }
            }
            if (!isValidEmail(email))
            {
                return "Invalid email address.";
            }
            
           
            var newUser = new User(Guid.NewGuid(),username, email, password);
            users.Add(newUser);
            return "User registered successfully.";


        }


    }
}*/
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Services
{
    public class UserService
    {
        private readonly List<User> users;

        public UserService()
        {
            users = new List<User>();
        }

        private bool IsValidEmail(string input)
        {
            return input.Contains("@gmail.com");
        }

        public User RegisterUser(string username, string email, string password)
        {
            // Validate username
            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                throw new ArgumentException("Username must have at least 1 and max 100 characters.");

            // Validate email
            if (string.IsNullOrWhiteSpace(email) || email.Length > 100)
                throw new ArgumentException("Email must have at least 1 and max 100 characters.");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email address. Only Gmail addresses are allowed.");

            // Validate password
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || password.Length > 16)
                throw new ArgumentException("Password must be between 8 and 16 characters.");

            // Check for existing user with the same email or username
            if (users.Any(u => u.Email == email))
                throw new ArgumentException("A user with this email already exists.");

            if (users.Any(u => u.Name == username))
                throw new ArgumentException("A user with this username already exists.");

            // Create and register a new user
            var newUser = new User(Guid.NewGuid(), username, email, password)
            {
                Cart = new Cart() // Initialize an empty cart
            };
            users.Add(newUser);

            return newUser;
        }
    }
}
