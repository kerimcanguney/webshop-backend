using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Account_API.Services;
using Account_API.ViewModels;
using Account_API.Models;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Account_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        AccountService accountService;
        public AccountController(IConfiguration _config)
        {
            this._config = _config;
            accountService = new AccountService(_config);
        }
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

        [HttpGet]
        [Authorize]
        [Route("/Settings")]
        public string AccountSettings()
        {
            Account account = GetCurrentUserViaHttpContext();
            
            return "Hello " + account.Surname;
        }
        private Account GetCurrentUserViaHttpContext()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;

            var userClaims = identity.Claims;

            return new Account
            {
                Surname = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,

            };
        }
    }
}
