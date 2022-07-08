using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferType { get; set; }
        public int TransferStatus { get; set; }

        public Transfer (int transferId, int transferType, int transferStatus)
        {
            TransferId = transferId;
            TransferType = transferType;
            TransferStatus = transferStatus;
        }
    }
}
