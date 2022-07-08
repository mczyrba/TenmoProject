using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }


        public decimal GetAccountBalance(int userId)
        {
            RestRequest request = new RestRequest($"account/{userId}");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            CheckForError(response, "Get account balance");
            return response.Data;
        }

        public Transfer MakeTransfer(int fromUser, int toUser)
        {
            RestRequest request = new RestRequest($"transfer/{fromUser}");
            request.AddJsonBody(fromUser);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);

            CheckForError(response, "Make a transfer");
            return response.Data;
        }

        public void MakeTransferSend(int fromUser, int toUser, decimal transferAmount)  // uncertain about this... method returns void in TransferSqlDao
        {
            RestRequest request = new RestRequest($"transfer/{fromUser}");
            TransferRequest newTransfer = new TransferRequest();
            newTransfer.FromAccount = fromUser;
            newTransfer.ToAccount = toUser;
            newTransfer.TransferAmount = transferAmount;
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            CheckForError(response, "Make a transfer");
            
        }

        public List<Transfer> SeeTransfers(int accountId)
        {
            RestRequest request = new RestRequest($"transfer/{accountId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            CheckForError(response, "See a list of your transfers");
            return response.Data;
        }

        public Transfer TransferDetails(int transferId)
        {
            RestRequest request = new RestRequest($"transfer/{transferId}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);

            CheckForError(response, "See transfer details by Transfer ID");
            return response.Data;
        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest("account/");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);

            CheckForError(response, "Get Users");
            return response.Data;
        }
       



        private void CheckForError(IRestResponse response, string action)
        {
            string message = "";

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message = $"A network error occurred, could not reach the server: {response.ErrorException.Message}";
                
                throw new HttpRequestException(message);
            }

            if (!response.IsSuccessful)
            {
                message = $"An error occured in the call to the server! Action {action} resulted in {(int)response.StatusCode} {response.StatusDescription}";
             
                throw new HttpRequestException(message);
            }
        }

    }
}
