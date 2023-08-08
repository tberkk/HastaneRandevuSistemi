using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace HastaneRandevuSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext c;
        public HomeController(AppDbContext context)
        {
            c = context;
        }

        [Authorize]
        public IActionResult RandevuSil(int? id)
        {
            var ran = c.RandevuTable.FirstOrDefault(x=> x.RandevuID == id);
            if(ran.UserID != null)
            {
                ran.UserID = null;
                c.RandevuTable.Update(ran);
                c.SaveChanges();
                return RedirectToAction("Randevularim");
            }
            return RedirectToAction("Randevularim");
        }

        [Authorize]
        public IActionResult Randevularim()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = c.RandevuTable.Include(x => x.Hastane).Include(x => x.Poliklinik).Include(x => x.Doktor).Where(x=> x.UserID == userId).ToList();
            return View(list);
        }
        [Authorize]
        public IActionResult Index()
        {
            var list = c.HastaneTable.ToList();
            return View(list);
        }
        [Authorize]
        public IActionResult PoliklinikGetir(int id)
        {
            var poli = c.PoliklinikTable.Where(x => x.HastaneID == id).ToList();
            ViewBag.data = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id).HastaneAd;
            return View(poli);
        }
        [Authorize]
        public IActionResult DoktorGetir(int id)
        {
            var doks = c.DoktorTable.Where(x => x.PoliklinikID == id).ToList();
            ViewBag.data = c.PoliklinikTable.FirstOrDefault(x => x.PoliklinikID == id).PoliklinikAd;
            return View(doks);
        }
        [Authorize]
        public IActionResult RandevuGetir(int id)
        {
            var rans = c.RandevuTable.Where(x => x.DoktorID == id).ToList();
            ViewBag.data = c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorAd + " " + c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorSoyad;
            return View(rans);
        }
        [Authorize]
        public IActionResult RandevuAl(int? id)
        {
            var ran = c.RandevuTable.FirstOrDefault(x => x.RandevuID == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ran != null)
            {
                ran.UserID = userId;
                c.RandevuTable.Update(ran);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
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