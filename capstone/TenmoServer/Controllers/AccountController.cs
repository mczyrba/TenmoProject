using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserDao userDao;

        public AccountController(IUserDao _userDao)
        {
            userDao = _userDao;
        }

        [HttpGet("/{account_id}")]
        public ActionResult<Account> GetBalance(int accountId)
        {
            Account balance = 
        }
    }
}
