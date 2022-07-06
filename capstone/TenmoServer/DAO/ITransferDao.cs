using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    interface ITransferDao
    {
        Transfer Transfer(int fromUser, int toUser);
        List<Transfer> SeeTransfers();
        Transfer TransferDetails(int transferId, string transferType);
    }
}
