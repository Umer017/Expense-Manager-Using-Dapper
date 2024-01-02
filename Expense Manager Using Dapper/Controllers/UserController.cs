using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Dapper;
using Expense_Manager_Using_Dapper.Models;
using Microsoft.Ajax.Utilities;

namespace Expense_Manager_Using_Dapper.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source = DESKTOP-9SVH54V\\SQLEXPRESS;Initial Catalog=ExpenseManagerDB;Integrated Security=true");
        // GET: User
        public ActionResult Index()
        {

            return RedirectToAction("Login");
        }

        public ActionResult UsersList()
        {
            //con.Query<UserModel>("sp_CreateUser", param: prameters, commandType: CommandType.StoredProcedure);
            return View();
        }

        public UserModel GetUserByEmail(UserModel user)
        {
            var prameters = new DynamicParameters();
            prameters.Add("@email", user.Email);
            UserModel userdata = con.QuerySingleOrDefault<UserModel>("sp_getUserByEmail", param: prameters, commandType: CommandType.StoredProcedure);
            return userdata;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            UserModel userdata = GetUserByEmail(user);

            if (userdata != null)
            {
                ViewBag.Error = "User Already Exist";
                return RedirectToAction("Register");
            }
            
            var prameters = new DynamicParameters();
            prameters.Add("@username", user.username);
            prameters.Add("@Email", user.Email);
            prameters.Add("@userpassword", user.userpassword);
            prameters.Add("@Mobile", user.Mobile);
            int num = con.Execute("sp_CreateUser", param: prameters, commandType: CommandType.StoredProcedure);
            if(num > 0)
            {
                ViewBag.Error = "User Created Successfully";
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.Error = "User is not created ";
                return RedirectToAction("Register");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [ActionName("Login")]
        [HttpPost]
        public ActionResult LoginConfirm(UserModel user)
        {
            var prameters = new DynamicParameters();
            prameters.Add("@Email", user.Email);
            UserModel userdata = con.QuerySingle<UserModel>("sp_getUserByEmail", param: prameters, commandType: CommandType.StoredProcedure);
            if (userdata != null)
            {
                if (user.userpassword == userdata.userpassword)
                {
                    Session["Email"] = userdata.Email;
                    Session["UserId"] = userdata.Id;
                    Session["UserName"] = userdata.username;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Error = "Invalid Email or Password";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.Error = "User Does not Exist";
                return RedirectToAction("Login");
            }
            
            
        }

        public List<ExpensesModel> getAllExpensesForUser()
        {
            var paramters = new DynamicParameters();
            paramters.Add("@userid", (Int32)Session["UserId"] );
            IEnumerable<ExpensesModel> Expenselist = con.Query<ExpensesModel>("getexpensesforuser",param:paramters, commandType: CommandType.StoredProcedure);
            return Expenselist.ToList();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        public List<IncomeModel> getAllIncomesForUser()
        {
            var paramters = new DynamicParameters();
            paramters.Add("@userid", (Int32)Session["UserId"]);
            IEnumerable<IncomeModel> Incomelist = con.Query<IncomeModel>("getincomesforuser",param:paramters, commandType: CommandType.StoredProcedure);
            return Incomelist.ToList();
        }

        public AllExpenseTotal getTotalExpense()
        {
            var paramters = new DynamicParameters();
            paramters.Add("id", (Int32)Session["UserId"]);
            AllExpenseTotal total = con.QuerySingle<AllExpenseTotal>("sp_getTotalExpense", param: paramters, commandType: CommandType.StoredProcedure);
            return total;
        }
        public AllIncomesTotal getTotalIncomes()
        {
            var paramters = new DynamicParameters();
            paramters.Add("id", (Int32)Session["UserId"]);
            AllIncomesTotal total = con.QuerySingle<AllIncomesTotal>("sp_getTotalIncome", param: paramters, commandType: CommandType.StoredProcedure);
            return total;
        }

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                AllExpenseAndIncomes all = new AllExpenseAndIncomes();

                List<ExpensesModel> expenses = getAllExpensesForUser();
                List<IncomeModel> income = getAllIncomesForUser();
                AllIncomesTotal TotalIncome = getTotalIncomes();
                AllExpenseTotal totalExpense = getTotalExpense();

                all.totalIncome = TotalIncome;
                all.totalExpense = totalExpense;
                all.Expenses = expenses;
                all.incomes = income;

                return View(all);
            }
        }
    }
}