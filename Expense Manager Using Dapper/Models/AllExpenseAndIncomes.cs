using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_Manager_Using_Dapper.Models
{

    public class AllIncomesTotal
    {
        public int noOfIncomes { get; set; }
        public int TotalIncome { get; set; }
    }

    public class AllExpenseTotal
    {
        public int noOfExpense { get; set; }
        public int TotalExpense { get; set; }
    }

    public class AllExpenseAndIncomes
    {
        public List<ExpensesModel> Expenses { get; set; }
        public List<IncomeModel> incomes { get; set; }

        public AllExpenseTotal totalExpense { get; set; }
        public AllIncomesTotal totalIncome { get; set; }

    }

   

}