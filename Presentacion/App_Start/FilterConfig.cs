using System.Web;
using System.Web.Mvc;
using Presentacion.Filters;

namespace Presentacion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CheckFilterAttributes() { IsCheck = true });
        }
    }
}
