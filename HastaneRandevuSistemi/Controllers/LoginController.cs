using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Controllers
{
    public class LoginController : Controller
    {
        AppDbContext c = new AppDbContext();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User u)
        {
            var user = c.UserTable.FirstOrDefault(x => x.KullaniciAdi == u.KullaniciAdi && x.Sifre == u.Sifre);
            if(user != null)
            {
                //işlemler
                return RedirectToAction("Index","Hastane");
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
