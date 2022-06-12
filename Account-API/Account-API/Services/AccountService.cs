using Account_API.Models;
using Account_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Account_API.Services
{
    public class AccountService 
    {
        private IConfiguration configuration;
        public AccountService(IConfiguration _config)
        {
            configuration = _config;
        }
        public string CreateAccount(AccountDTO accountDTO)
        {
            string Validation = ValidateInfo(accountDTO.Email, accountDTO.Password);

            if (Validation != "valid") return Validation;

            Account account = convertToDAL(accountDTO);
            SaveAccount(account);

            return "Success";
        }
        public string RemoveAccount(string email)
        {
            using (var context = new WsContext())
            {
                var CollisionList = context.Accounts.Where(x => x.Email == email).ToList();
                var CollidingEmail = CollisionList.Find(x => x.Email == email);

                if (CollidingEmail != null)
                {
                    context.Remove(CollidingEmail);

                    context.SaveChanges();
                    return "Removed";
                }
            }

            return "N/A";
        }
        private Account convertToDAL(AccountDTO accountDTO)
        {
            byte[] salt = PasswordHashing.CreateSalt();
            string hash = PasswordHashing.HashPassword(accountDTO.Password, salt);
            Password password = new Password() { Hash = hash, Salt = salt };
            Account account = new Account() { Surname = accountDTO.Surname, Lastname = accountDTO.Lastname, Email = accountDTO.Email, Password = password };

            return account;
        }
        private void SaveAccount(Account account)
        {
            using (var context = new WsContext())
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }
        private string ValidateInfo(string email, string password)
        {
            string passwordValidation = ValidatePassword(password);
            string emailValidation = ValidateEmail(email);
            if (passwordValidation != "valid") return passwordValidation;
            if (emailValidation != "valid") return emailValidation;

            return "valid";
        }
        private string ValidatePassword (string password)
        {
            if (password.Length <= 8) return "to short";
            if (password.Any(char.IsUpper) == false) return "no uppercase";
            if (password.Any(char.IsDigit) == false) return "no digits";
            if (password.Any(char.IsPunctuation) == false) return "no special characters";

            return "valid";
        }
        private string ValidateEmail(string email)
        {
            using (var context = new WsContext())
            {
                var CollisionList = context.Accounts.Where(x => x.Email == email).ToList();
                var CollidingEmail =CollisionList.Find(x=> x.Email == email);

                if (CollidingEmail != null) return "can't use same email";

            }
            return "valid";
        }
        public string Login(string email, string password)
        {
            Account _account;
            using (var context = new WsContext())
            {
                List<Account> CollisionList = context.Accounts.Where(x => x.Email == email).ToList();
                Account Collision= CollisionList.Find(x => x.Email == email);
                if (Collision == null) return "no email found";

                byte[] salt = context.Passwords.Single(x=> x.Account_Id == Collision.Id).Salt;

                string hash = PasswordHashing.HashPassword(password, salt);

                if (hash != Collision.Password.Hash) return "password hash does not match";

                _account = Collision;
            }

            string token = GenerateToken(_account);
            return token;
        }
        private string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Surname),
                new Claim(ClaimTypes.Email, account.Email),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
