using Microsoft.AspNetCore.Mvc;

namespace AmhPorterTest.Controllers
{
    public class SearchController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }
    }
}
