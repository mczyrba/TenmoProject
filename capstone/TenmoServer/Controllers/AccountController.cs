using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace TenmoServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly AccountSqlDao accountSqlDao;

        public AccountController(IUserDao _userDao, AccountSqlDao _accountSqlDao)
        {
            userDao = _userDao;
        }
        
        [HttpGet("/{account_id}")]
        public ActionResult<Account> GetBalance(int accountId)
        {
            //Account balance = null;
            return null;
        }
    }
}
