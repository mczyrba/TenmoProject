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
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountDao accountDao;
        private readonly ITransferDao transferDao;
        private readonly IUserDao userDao;

        public AccountController(IAccountDao _accountDao, ITransferDao _transferDao, IUserDao _userDao)
        {
            accountDao = _accountDao;
            transferDao = _transferDao;
            userDao = _userDao;
        }

        [HttpGet("/account/{userId}")] // step 3
        public ActionResult<Account> GetBalance(int userId)
        {
            Account account = accountDao.GetAccount(userId);

            if(account != null)
            {
                return Ok(account.Balance);
            }
            else
            {
                return NotFound();
            }
        }
        

        [HttpPost("transfer/fromAccount")] // step 4
        public ActionResult MakeTransfer(int fromAccount, int toAccount, decimal transferAmount)
        { 
            if(toAccount == fromAccount)
            {
                return BadRequest(new { message = "Invalid transfer request. Cannot transfer to same account." });
            }
            else if(transferAmount <= 0 || transferAmount > accountDao.GetAccount(fromAccount).Balance)
            {
                return BadRequest(new { message = "Invalid transfer request. Transfer amount must be greater than 0." });
            }

            transferDao.MakeTransferSend(fromAccount, toAccount, transferAmount);

            return Ok();
        }

        [HttpGet("transfer/{account_id}")] // step 5
        public ActionResult<List<Transfer>> GetTransfers(int accountId)
        {
            List<Transfer> seeTransfers = transferDao.SeeTransfers(accountId);

            if (seeTransfers == null)
            {
                return NotFound();
            }

            return seeTransfers;
        }

        [HttpGet("transfer/details")] // step 6
        public ActionResult<Transfer> GetTransferDetails(int transferId)
        {
            Transfer transferDetails = transferDao.TransferDetails(transferId);

            if (transferDetails == null)
            {
                return NotFound();
            }

            return transferDetails;
            
        }

        [HttpGet("account/")]
        public ActionResult<List<User>> GetAllUsers()
        {
            List<User> users = userDao.GetUsers();
            return users;
        }

    }
}
