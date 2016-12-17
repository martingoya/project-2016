using System.Web;
using System.Web.Mvc;

namespace Dataloader___Oh_lala_Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
