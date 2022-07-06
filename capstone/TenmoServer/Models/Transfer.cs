using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public string TransferType { get; set; }
        public string TransferStatus { get; set; }

        public Transfer (int transferId, string transferType, string transferStatus)
        {
            TransferId = transferId;
            TransferType = transferType;
            TransferStatus = transferStatus;
        }
    }
}
