using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer Transfer(int fromUser, int toUser, decimal transferAmount);
        List<Transfer> SeeTransfers();
        Transfer TransferDetails(int transferId, string transferType);
    }
}
