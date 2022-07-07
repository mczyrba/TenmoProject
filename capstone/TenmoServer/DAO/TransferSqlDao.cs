﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void MakeTransferSend(int fromUser, int toUser, decimal transferAmount)
        {
            //Transfer returnTransfer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO transfer(transfer_type_id, transfer_status_id, account_from, account_to, amount) VALUES (2, 2, @fromUser, @toUser, @transferAmount);" +
                    " UPDATE account SET balance -= @transferAmount WHERE user_id = @fromUser AND balance >= @transferAmount; UPDATE account SET balance += @transferAmount WHERE user_id = @toUser;", conn);
                    cmd.Parameters.AddWithValue("@fromUser", fromUser);
                    cmd.Parameters.AddWithValue("@toUser", toUser);
                    cmd.Parameters.AddWithValue("@transferAmount", transferAmount);

                    cmd.ExecuteNonQuery();

                    //SqlDataReader reader = cmd.ExecuteReader();

                    //if (reader.Read())
                    //{
                    //    returnTransfer = CreateTransferFromReader(reader);
                    //}

                    //return returnTransfer;
                }
            }
            catch (SqlException) { throw; }
        }

        public Transfer MakeTransferRequest(int fromUser, int toUser, decimal transferAmount)
        {
            Transfer returnTransfer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO transfer(transfer_type_id, transfer_status_id, account_from, account_to, amount)" +
                        " VALUES (1, 1, @fromUser, @toUser, @transferAmount );", conn);
                    cmd.Parameters.AddWithValue("@fromUser", fromUser);
                    cmd.Parameters.AddWithValue("@toUser", toUser);
                    cmd.Parameters.AddWithValue("@transferAmount", transferAmount);


                    SqlDataReader reader = cmd.ExecuteReader();
                   



                    if (reader.Read())
                    {
                        returnTransfer = CreateTransferFromReader(reader);
                    }

                    return returnTransfer;
                }
            }
            catch (SqlException) { throw; }
        }


        public List<Transfer> SeeTransfers(int accountId)
        {
            List<Transfer> transfers = new List<Transfer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer.* FROM transfer JOIN account ON account.account_id = transfer.account_from WHERE account_id = @account_id;", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = CreateTransferFromReader(reader);
                        transfers.Add(transfer);
                    }

                    return transfers;
                }
            }
            catch (SqlException) { throw; }
        }


        public Transfer TransferDetails(int transferId)
        {
            Transfer transfer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer.* FROM transfer JOIN transfer_type ON transfer_type.transfer_type_id = transfer.transfer_type_id" +
                        " JOIN transfer_status ON transfer_status.transfer_status_id = transfer.transfer_status_id WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        transfer = CreateTransferFromReader(reader);
                    }

                    return transfer;
                }
            }
            catch (SqlException) { throw; }
        }



        private Transfer CreateTransferFromReader(SqlDataReader reader)
        {
            int id = Convert.ToInt32(reader["transfer_id"]);
            string transferType = Convert.ToString(reader["transfer_type"]);
            string transferStatus = Convert.ToString(reader["transfer_status"]);

            Transfer transfer = new Transfer(id, transferType, transferStatus);

            return transfer;
        }


        //            ／＞　 フ
        //            | 　_ _| 
        //          ／` ミ＿xノ 
        //         /　　　　 |
        //        /　 ヽ ﾉ
        //        │　　|　|　|
        //   ／￣|　　 |　|　|
        //   (￣ヽ＿_ヽ_)__)
        //    ＼二)
    }
}
