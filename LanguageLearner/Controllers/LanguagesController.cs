using LangData.Objects;
using LangServices;
using LanguageLearner.Models;
using LanguageLearner.Models.Languages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace LanguageLearner.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ILanguagesService LanguagesService;
        public LanguagesController(ILanguagesService LanguagesService)
        {
            this.LanguagesService = LanguagesService;
        }

        public IActionResult Index()
        {
            try
            {
                var langs = LanguagesService.GetAll().ToArray();
                var model = new LanguagesModel() { AvailableLanguages = langs };

                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { Exception = e, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult ErrorIndex(string errorMsg)
        {
            var langs = LanguagesService.GetAll().ToArray();
            var model = new LanguagesModel() { AvailableLanguages = langs, AlertMessage = errorMsg, AlertType = AlertType.Error };

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult EditLanguages(string buttonName, string languageName)
        {
            Action ac = () => { };

            if (buttonName == "New")
            {
                if (string.IsNullOrEmpty(languageName))
                    return RedirectToAction("ErrorIndex", new { errorMsg = "Language name cannot be empty." });

                var lang = new Language(languageName);
                ac = () => LanguagesService.Add(lang);
            }
            else
            {
                var id = int.Parse(buttonName.Split("_")[1]);
                var lang = LanguagesService.Get(id);

                if (buttonName.StartsWith("Rename"))
                {
                    if (string.IsNullOrEmpty(languageName))
                        return RedirectToAction("ErrorIndex", new { errorMsg = "Language name cannot be empty." });

                    lang.Name = languageName;
                    ac = () => LanguagesService.Update(lang);
                }
                else if (buttonName.StartsWith("Delete"))
                {
                    try
                    {
                        LanguagesService.Remove(lang);
                    }
                    catch
                    {
                        return RedirectToAction("ErrorIndex", new { errorMsg = "Cannot delete language because there are words still using it." });
                    }
                }
            }

            try
            {
                ac.Invoke();
            }
            catch (Exception e)
            {
                return RedirectToAction("ErrorIndex", new { errorMsg = "Something went wrong: " + e.Message });
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
