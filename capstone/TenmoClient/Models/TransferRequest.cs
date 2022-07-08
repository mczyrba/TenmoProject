using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class TransferRequest
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }

        public TransferRequest() { }
    }
}
