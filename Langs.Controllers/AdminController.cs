using Langs.Data.Context;
using Langs.Models;
using Langs.Services;
using Langs.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Langs.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext m_Context;
        public AdminController(DatabaseContext context)
        {
            this.m_Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Clean()
        {
            DatabaseUtils.ClearDB(m_Context);
            return RedirectToAction("Index");
        }

        public IActionResult AddWords()
        {
            DatabaseUtils.PopulateWithTestData(m_Context);
            return RedirectToAction("Index");
        }

        public IActionResult Migrate()
        {
            DatabaseUtils.MigrateDB(m_Context);
            return RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            DatabaseUtils.DeleteDB(m_Context);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
