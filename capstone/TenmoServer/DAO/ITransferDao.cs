using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer MakeTransfer(int fromUser, int toUser, decimal transferAmount);

        List<Transfer> SeeTransfers();
        Transfer TransferDetails(int transferId);
    }
}
