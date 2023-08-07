using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace HastaneRandevuSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext c;
        public HomeController(AppDbContext context)
        {
            c = context;
        }

        public IActionResult Index()
        {
            var list = c.HastaneTable.ToList();
            return View(list);
        }
        public IActionResult PoliklinikGetir(int id)
        {
            var poli = c.PoliklinikTable.Where(x => x.HastaneID == id).ToList();
            ViewBag.data = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id).HastaneAd;
            return View(poli);
        }
        public IActionResult DoktorGetir(int id)
        {
            var doks = c.DoktorTable.Where(x => x.PoliklinikID == id).ToList();
            ViewBag.data = c.PoliklinikTable.FirstOrDefault(x => x.PoliklinikID == id).PoliklinikAd;
            return View(doks);
        }
        public IActionResult RandevuGetir(int id)
        {
            var rans = c.RandevuTable.Where(x => x.DoktorID == id).ToList();
            ViewBag.data = c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorAd + c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorSoyad;
            return View(rans);
        }
        /*public IActionResult RandevuAl(int? id)
        {
            var ran = c.RandevuTable.FirstOrDefault(x => x.RandevuID == id);
            if(ran != null)
            {
                c.RandevuTable.Update(ran);
                c.SaveChanges();
                return RedirectToAction("RandevuGetir");
            }
            else
            {
                return RedirectToAction("RandevuGetir");
            }
            
        }*/
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}