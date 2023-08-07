using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly AppDbContext c;
        public PoliklinikController(AppDbContext context)
        {
            c = context;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var list = c.PoliklinikTable.ToList();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DoktorGetir(int id)
        {

            var list = c.DoktorTable.Where(x => x.PoliklinikID == id).ToList();
            ViewBag.data = c.PoliklinikTable.FirstOrDefault(x => x.PoliklinikID == id).PoliklinikAd;
            return View(list);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult RandevuGetir(int id)
        {
            var list = c.RandevuTable.Include(x => x.Doktor).Where(x => x.PoliklinikID == id).ToList();
            ViewBag.data = c.PoliklinikTable.FirstOrDefault(x => x.PoliklinikID == id).PoliklinikAd;
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult PoliklinikEkle()
        {
            List<SelectListItem> lst = (from x in c.HastaneTable.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.HastaneAd,
                                            Value = x.HastaneID.ToString()
                                        }).ToList();
            ViewBag.List = lst;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult PoliklinikEkle(Poliklinik poliklinik)
        {
            if (ModelState.IsValid)
            {
                c.PoliklinikTable.Add(poliklinik);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PoliklinikSil(int? id)
        {
            if (id == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            var poli = c.PoliklinikTable.Include(x => x.DoktorList).FirstOrDefault(x => x.PoliklinikID == id);
            if (poli == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if (poli.DoktorList.Count > 0)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            else
            {
                c.PoliklinikTable.Remove(poli);
                c.SaveChanges();
                TempData["msj"] = poli.PoliklinikAd + "Silindi.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PoliklinikDuzenle(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "HATAAA!!";
                return RedirectToAction("Index");
            }
            var pol = c.PoliklinikTable.FirstOrDefault(x => x.PoliklinikID == id);
            if (pol == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            return View(pol);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult PoliklinikDuzenle(int? id, Poliklinik poliklinik)
        {
            if (poliklinik.PoliklinikID != id)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    c.PoliklinikTable.Update(poliklinik);
                    c.SaveChanges();
                    TempData["msj"] = poliklinik.PoliklinikAd + "Güncellendi.";
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
