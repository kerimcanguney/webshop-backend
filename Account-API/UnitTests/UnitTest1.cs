using System;
using Xunit;
using Account_API.Controllers;
using Account_API.ViewModels;


namespace UnitTests
{
    public class UnitTest1
    {

        [Fact]
        public void Create_Valid_Account()
        {
            AccountController accountController = new AccountController(null);
            
            var res = accountController.CreateAccount(new AccountDTO()
            {
                Email = "legit@correct.com",
                Surname = "acceptius",
                Lastname = "validius",
                Password = "Avalidp@ssw0rd",
            });

            Assert.Equal("Success", res);
        }

        [Fact]
        public void Create_Invalid_Account_Password_To_Short()
        {
            AccountController accountController = new AccountController(null);

            var res = accountController.CreateAccount(new AccountDTO()
            {
                Email = "legit@correct.com",
                Surname = "acceptius",
                Lastname = "validius",
                Password = "short5@",
            });

            Assert.Equal("to short", res);
        }

        [Fact]
        public void Create_Invalid_Account_Password_No_Special()
        {
            AccountController accountController = new AccountController(null);

            var res = accountController.CreateAccount(new AccountDTO()
            {
                Email = "legit@correct.com",
                Surname = "acceptius",
                Lastname = "validius",
                Password = "Short123abc",
            });

            Assert.Equal("no special characters", res);
        }

        [Fact]
        public void Create_Invalid_Account_Password_No_Digits()
        {
            AccountController accountController = new AccountController(null);

            var res = accountController.CreateAccount(new AccountDTO()
            {
                Email = "legit@correct.com",
                Surname = "acceptius",
                Lastname = "validius",
                Password = "Short@($)*@!(",
            });

            Assert.Equal("no digits", res);
        }

        [Fact]
        public void remove_account()
        {
            Create_Valid_Account();

            var accountController = new AccountController(null);

            var res = accountController.RemoveAccount("legit@correct.com");

            Assert.Equal("Removed", res);
        }
    }
}
