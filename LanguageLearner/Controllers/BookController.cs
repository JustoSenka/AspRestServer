using LanguageLearnerData.Models;
using LanguageLearnerData.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanguageLearner.Controllers
{
    public class BookController : Controller
    {
        public BookModel bookModel;
        public IActionResult Index()
        {
            bookModel = new BookModel() { LanguageFrom = new Language() { Name = "Spanish" } };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
