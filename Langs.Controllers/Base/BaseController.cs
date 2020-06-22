using Langs.Models;
using Langs.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Langs.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult ShowErrorViewForNotFoundBook(int id) => ShowErrorView(id, "Book with ID {0} not found.");
        public IActionResult ShowErrorViewForNotFoundWord(int id) => ShowErrorView(id, "Word with ID {0} not found.");

        public IActionResult ShowErrorView(int id, string msg)
        {
            return View("Error", new ErrorViewModel
            {
                Exception = new Exception(string.Format(msg, id)),
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public void TryOrAlert(IAlertMessageModel model, Action ac)
        {
            try
            {
                ac.Invoke();
            }
            catch (Exception e)
            {
                model.AlertMessage = "Something went wrong: " + e.Message + " " + e.InnerException?.Message;
                model.AlertType = AlertType.Error;
            }
        }
    }
}
