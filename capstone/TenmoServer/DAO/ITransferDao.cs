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
        void MakeTransferSend(int fromUser, int toUser, decimal transferAmount);
        Transfer MakeTransferRequest(int fromUser, int toUser, decimal transferAmount);
        List<Transfer> SeeTransfers(int accountId);
        Transfer TransferDetails(int transferId);
    }
}
