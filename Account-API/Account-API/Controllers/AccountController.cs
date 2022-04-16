using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_API.Services;
using Account_API.ViewModels;
using Account_API.Models;

namespace Account_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountService accountService = new AccountService();

        [HttpGet]
        [Route("/get")]
        public Array GetAllAccounts()
        {
            var context = new WsContext();
            var All = context.Accounts.ToArray();

            return All;
        }
        
        [HttpPost]
        [Route("/Create")]
        public string CreateAccount(AccountDTO accountDTO)
        {
            return accountService.CreateAccount(accountDTO);
        }

        [HttpPost]
        [Route("/Login")]
        public string LoginAccount(string Email, string Password)
        {
            return accountService.Login(Email, Password);
        }

    }
}
