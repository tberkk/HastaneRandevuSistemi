using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Controllers
{
    public class DoktorController : Controller
    {
        AppDbContext c = new AppDbContext();
        public IActionResult Index()
        {
            var list = c.DoktorTable.ToList();
            return View(list);
        }

        public IActionResult RandevuGetir(int id)
        {
            var list = c.RandevuTable.Include(x => x.Doktor).Where(x => x.DoktorID == id).ToList();
            ViewBag.data = c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorAd + c.DoktorTable.FirstOrDefault(x => x.DoktorID == id).DoktorSoyad;
            return View(list);
        }

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
