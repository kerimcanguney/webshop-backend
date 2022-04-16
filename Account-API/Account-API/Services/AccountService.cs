using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account_API.Models;
using Account_API.ViewModels;
namespace Account_API.Services
{
    public class AccountService
    {
        public string CreateAccount(AccountDTO accountDTO)
        {
            string Validation = ValidateInfo(accountDTO.Email, accountDTO.Password);

            if (Validation != "valid") return Validation;

            Account account = convertToDAL(accountDTO);
            SaveAccount(account);

            return "Success";
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
            using (var context = new WsContext())
            {
                List<Account> CollisionList = context.Accounts.Where(x => x.Email == email).ToList();
                Account Collision= CollisionList.Find(x => x.Email == email);
                if (Collision == null) return "no email found";

                byte[] salt = context.Passwords.Single(x=> x.Account_Id == Collision.Id).Salt;

                string hash = PasswordHashing.HashPassword(password, salt);

                if (hash != Collision.Password.Hash) return "password hash does not match";

            }
            return "valid";
        }
    }
}
