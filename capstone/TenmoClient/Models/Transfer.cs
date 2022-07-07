using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public decimal TransferAmount { get; set; }
        public int TransferId { get; set; }
        public int AccountId { get; set; }


        public Transfer() { }

        public Transfer(int fromUser, int toUser, decimal transferAmount, int transferId, int accountId)
        {
            FromUser = fromUser;
            ToUser = toUser;
            TransferAmount = transferAmount;
            TransferId = transferId;
            AccountId = accountId;
        }
    }
}
