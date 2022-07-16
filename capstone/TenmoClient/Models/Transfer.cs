using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferType { get; set; }
        public int TransferStatus { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }

        public Transfer() { }

        public Transfer(int transferId, int transferType, int transferStatus, int accountFrom, int accountTo, decimal amount)
        {
            TransferId = transferId;
            TransferType = transferType;
            TransferStatus = transferStatus;
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            Amount = amount;
        }
    }
}
