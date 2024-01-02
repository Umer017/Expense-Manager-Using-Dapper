using System.Web;
using System.Web.Mvc;

namespace Expense_Manager_Using_Dapper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
