using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Account
    {
        public int UserId { get; set; }

        public Account() {}

        public Account(int userId)
        {
            UserId = userId;
        }
    }
}
