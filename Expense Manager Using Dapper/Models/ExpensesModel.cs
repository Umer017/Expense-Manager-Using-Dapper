using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_Manager_Using_Dapper.Models
{
    public class ExpensesModel
    {
  /* expenseId ,
    userId,
    Purpose,
    expenseType,
    Amount*/

        public int expenseId { get; set; }
        public int userId { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
        public string expenseType { get; set; }
        

    }
}