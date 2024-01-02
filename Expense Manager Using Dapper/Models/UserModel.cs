using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_Manager_Using_Dapper.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string Email { get; set; }
        public string userpassword { get; set; }
        public string Mobile { get; set; }

    }
}