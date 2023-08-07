using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Controllers
{
    public class DoktorController : Controller
    {
        private readonly AppDbContext c;
        public DoktorController(AppDbContext context)
        {
            c = context;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var list = c.DoktorTable.ToList();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RandevuGetir(int id)
        {
            var list = c.RandevuTable.Include(x => x.Doktor).Where(x => x.DoktorID == id).ToList();
            ViewBag.data = c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorAd + c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorSoyad;
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DoktorEkle()
        {
            List<SelectListItem> plist = (from x in c.PoliklinikTable.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.PoliklinikAd,
                                             Value = x.PoliklinikID.ToString()
                                         }).ToList();
            List<SelectListItem> hlist = (from x in c.HastaneTable.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.HastaneAd,
                                             Value = x.HastaneID.ToString()
                                         }).ToList();
            ViewBag.plist = plist;
            ViewBag.hlist = hlist;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DoktorEkle(Doktor doktor)
        {
            if(ModelState.IsValid)
            {
                c.DoktorTable.Add(doktor);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Hataa";
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DoktorSil(int? id)
        {
            if (id == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            var dok = c.DoktorTable.Include(x => x.RandevuList).FirstOrDefault(x => x.DoktorID == id);
            if (dok == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if (dok.RandevuList.Count > 0)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            else
            {
                c.DoktorTable.Remove(dok);
                c.SaveChanges();
                TempData["msj"] = dok.DoktorAd + dok.DoktorSoyad + "Silindi.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DoktorDuzenle(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "HATAAA!!";
                return RedirectToAction("Index");
            }
            var dok = c.DoktorTable.FirstOrDefault(x => x.DoktorID == id);
            if (dok == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            return View(dok);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DoktorDuzenle(int? id, Doktor doktor)
        {
            if (doktor.DoktorID != id)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    c.DoktorTable.Update(doktor);
                    c.SaveChanges();
                    TempData["msj"] = doktor.DoktorAd + doktor.DoktorSoyad + "Güncellendi.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            else
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
        }
    }
}
