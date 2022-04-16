using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account_API.Services;
namespace Account_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpGet]
        [Route("/get")]
        public Array GetAllAccounts()
        {
            var context = new WsContext();
            var All = context.Accounts.ToArray();

            return All;
        }

        [HttpPost]
        [Route("/post")]
        public string CreatePassword(string password)
        {
            string HashedPw = PasswordHashing.HashNewPassword(password);

            return HashedPw;
        }

    }
}
