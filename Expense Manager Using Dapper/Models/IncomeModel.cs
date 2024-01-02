using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_Manager_Using_Dapper.Models
{
    public class IncomeModel
    {

    public int incomeId { get; set; }
    public int userId { get; set; }
    public string Source { get; set; }
    public string IncomeType { get; set; }
    public int Amount { get; set; }


    }
}