using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;
using System.Net.Http;

namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                Console.WriteLine($"Your Balance is: {tenmoApiService.GetAccountBalance(tenmoApiService.UserId)}"); 
                console.Pause();
            }

            if (menuSelection == 2)
            {
                foreach(Transfer item in tenmoApiService.SeeTransfers(2001))
                {
                    Console.WriteLine($"From: {item.FromUser}, To: {item.ToUser}, Ammount: {item.TransferAmount}");

                }
                
                console.Pause();
            }

            if (menuSelection == 3)
            {
                Console.WriteLine(tenmoApiService.TransferDetails(tenmoApiService.UserId));
                console.Pause();
            }

            if (menuSelection == 4)
            {
                int i = 1;
                List<User> users = tenmoApiService.GetUsers();
                foreach(User item in users)
                {
                    Console.WriteLine($"({i}) User: {item.Username}, ID: {item.UserId}");
                    i++;
                }
                
                Console.WriteLine("Enter user ID of account to transfer to.");
                int transferSelection = int.Parse(Console.ReadLine());
                Console.WriteLine($"Enter the amount you would like to transfer to account {transferSelection}.");
                decimal transferAmount = decimal.Parse(Console.ReadLine());

                tenmoApiService.MakeTransferSend(tenmoApiService.UserId, transferSelection, transferAmount);
                Console.WriteLine($"Transfer of ${transferAmount} successful to account {transferSelection}.");
                console.Pause();


                
                //int selectedUser = 0;
                //int selection = 1;

                //for(int x = 1; x < users.Count; x++)
                //{
                //    if(transferSelection == selection)
                //    {

                //    }
                //    selection++;
                //    selectedUser++;
                //}
                
                //tenmoApiService.MakeTransfer(tenmoApiService.UserId, tenmoApiService.UserId); // need to differentiate between from and to userzzxxcjzj

            }

            if (menuSelection == 5)
            {
                // Request TE bucks OPTIONAL..... 
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }
    }
}
