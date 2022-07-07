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

        public AccountController(IAccountDao _accountDao, ITransferDao _transferDao)
        {
            accountDao = _accountDao;
            transferDao = _transferDao;
        }

        [HttpGet()] // step 3
        public ActionResult<Account> GetBalance(int userId)
        {
            Account account = accountDao.GetAccountBalance(userId);

            if(account != null)
            {
                return account;
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
            else if(transferAmount <= 0 || transferAmount > accountDao.GetAccountBalance(fromAccount).Balance)
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

    }
}
