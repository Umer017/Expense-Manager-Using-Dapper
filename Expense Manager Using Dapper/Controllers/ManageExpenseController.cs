using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Expense_Manager_Using_Dapper.Models;

namespace Expense_Manager_Using_Dapper.Controllers
{
   
    public class ManageExpenseController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-9SVH54V\\SQLEXPRESS;Initial Catalog=ExpenseManagerDB;Integrated Security=true");
        // GET: ManageExpense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewExpenses()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login","User");
            }
            var paramters = new DynamicParameters();
            paramters.Add("@userid", (Int32)Session["UserId"]);
            IEnumerable<ExpensesModel> Expenselist = con.Query<ExpensesModel>("getexpensesforuser", param: paramters, commandType: CommandType.StoredProcedure);
            return View(Expenselist);
        }

        public ActionResult AddExpense()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddExpense(ExpensesModel expense)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var prameters = new DynamicParameters();

            prameters.Add("@userId", (Int32)Session["UserId"]);
            prameters.Add("@Amount", expense.Amount);
            prameters.Add("@Purpose", expense.Purpose);
            prameters.Add("@expenseType", expense.expenseType);

            int num = con.Execute("sp_CreateExpense", param: prameters, commandType: System.Data.CommandType.StoredProcedure);

            if (num > 0) { 
                
                return RedirectToAction("ViewExpenses");
            
            }
                return View();
        }

        public ExpensesModel ExpenseById(int id)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            ExpensesModel expense = con.QuerySingleOrDefault<ExpensesModel>("sp_ExpenseById", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return expense;
        }

        public ActionResult DeleteExpense(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View(ExpenseById(id));
        }

        [ActionName("DeleteExpense")]
        [HttpPost]
        public ActionResult DeleteExpenseConfirm(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var prams = new DynamicParameters();
            prams.Add("@id", id);

            int num = con.Execute("sp_deleteExpenseById", param: prams, commandType: System.Data.CommandType.StoredProcedure);

            if(num > 0)
            {
                return RedirectToAction("ViewExpenses");
            }
            return View();
        }

        public ActionResult UpdateExpense(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(ExpenseById(id));
        }

        [ActionName("UpdateExpense")]
        [HttpPost]
        public ActionResult UpdateExpenseCofirm(ExpensesModel expense)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@expenseId", expense.expenseId);
            parameters.Add("@Amount", expense.Amount);
            parameters.Add("@Purpose", expense.Purpose);
            parameters.Add("@expenseType", expense.expenseType);
            int num = con.Execute("sp_updateExpense", param:parameters, commandType:System.Data.CommandType.StoredProcedure);
            if(num > 0)
            {
                return RedirectToAction("ViewExpenses");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ViewDetails(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(ExpenseById(id));
        }

        public ActionResult ViewAllIncomes()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var paramters = new DynamicParameters();
            paramters.Add("@userid", (Int32)Session["UserId"]);
            IEnumerable<IncomeModel> Incomelist = con.Query<IncomeModel>("getincomesforuser", param: paramters, commandType: CommandType.StoredProcedure);
            return View(Incomelist);
        }

        public ActionResult AddIncome()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddIncome(IncomeModel model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@userId", (Int32)Session["UserId"]);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@Source", model.Source);
            parameters.Add("@IncomeType",model.IncomeType);

            int num = con.Execute("sp_CreateIncome", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            if(num > 0)
            {
                return RedirectToAction("ViewAllIncomes");
            }
            else
            {
                return View();
            }
        }

        public IncomeModel IncomeById(int id)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            IncomeModel income = con.QuerySingleOrDefault<IncomeModel>("sp_IncomeById", param:parameters,commandType:System.Data.CommandType.StoredProcedure);
            return income;
        }

        public ActionResult UpdateIncome(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(IncomeById(id));
        }

        [HttpPost]
        public ActionResult UpdateIncome(IncomeModel model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@incomeId", model.incomeId);
            parameters.Add("@Amount", model.Amount);
            parameters.Add("@Source", model.Source);
            parameters.Add("@IncomeType", model.IncomeType);
            int num = con.Execute("sp_UpdateIncome",param:parameters,commandType:System.Data.CommandType.StoredProcedure);
            if(num > 0)
            {
                return RedirectToAction("ViewAllIncomes");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DeleteIncome(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(IncomeById(id));
        }

        [ActionName("DeleteIncome")]
        [HttpPost]
        public ActionResult DeleteIncomeConfirm(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@id",id);
            int num = con.Execute("sp_DeleteIncomeById",param:parameters,commandType:System.Data.CommandType.StoredProcedure);
            if(num > 0)
            {
                return RedirectToAction("ViewAllIncomes");
            }
            else
            {
                return View();
            }
        }
       
        public ActionResult IncomeDetails(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(IncomeById(id));
        }


    }

}