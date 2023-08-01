using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Controllers
{
    public class RandevuController : Controller
    {
        AppDbContext c = new AppDbContext();
        public IActionResult Index()
        {
            var list = c.RandevuTable.Include(x => x.Hastane).Include(x => x.Poliklinik).Include(x => x.Doktor).ToList();

            return View(list);
        }

        public IActionResult RandevuDuzenle(int? id)
        {
            List<SelectListItem> gunlist = new List<SelectListItem>();
            var gun = new[]
            {
                new SelectListItem{Value="Pazartesi",Text="Pazartesi"},
                new SelectListItem{Value="Salı",Text="Salı"},
                new SelectListItem{Value="Çarşamba",Text="Çarşamba"},
                new SelectListItem{Value="Perşembe",Text="Perşembe"},
                new SelectListItem{Value="Cuma",Text="Cuma"},
                new SelectListItem{Value="Cumartesi",Text="Cumartesi"},
                new SelectListItem{Value="Pazar",Text="Pazar"},
            };
            gunlist = gun.ToList();
            List<SelectListItem> saatlist = new List<SelectListItem>();
            var saat = new[]
            {
                new SelectListItem{Value="09.00",Text="09.00"},
                new SelectListItem{Value="10.00",Text="10.00"},
                new SelectListItem{Value="11.00",Text="11.00"},
                new SelectListItem{Value="12.00",Text="12.00"},
                new SelectListItem{Value="13.00",Text="13.00"},
                new SelectListItem{Value="14.00",Text="14.00"},
                new SelectListItem{Value="15.00",Text="15.00"},
                new SelectListItem{Value="16.00",Text="16.00"},
                new SelectListItem{Value="17.00",Text="17.00"},
                new SelectListItem{Value="18.00",Text="18.00"},
            };
            saatlist = saat.ToList();
            List<SelectListItem> dlist = (from x in c.DoktorTable.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DoktorAd + " " + x.DoktorSoyad,
                                              Value = x.DoktorID.ToString()
                                          }).ToList();
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
            ViewBag.saatlist = saatlist;
            ViewBag.gunlist = gunlist;
            ViewBag.hlist = hlist;
            ViewBag.plist = plist;
            ViewBag.dlist = dlist;
            if (id is null)
            {
                TempData["msj"] = "HATAAA!!";
                return RedirectToAction("Index");
            }
            var ran = c.RandevuTable.FirstOrDefault(x => x.RandevuID == id);
            if (ran == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            return View(ran);
        }

        [HttpPost]
        public IActionResult RandevuDuzenle(int? id, Randevu randevu)
        {
            if (randevu.RandevuID != id)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    c.RandevuTable.Update(randevu);
                    c.SaveChanges();
                    TempData["msj"] = randevu.RandevuGun + randevu.RandevuSaat + "Randevusu Güncellendi.";
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

        public IActionResult RandevuSil(int? id)
        {
            if (id == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            var ran = c.RandevuTable.FirstOrDefault(x => x.RandevuID == id);
            if (ran == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }else
            {
                c.RandevuTable.Remove(ran);
                c.SaveChanges();
                TempData["msj"] = ran.RandevuGun + ran.RandevuSaat + "Randevusu Silindi.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult RandevuEkle()
        {
            List<SelectListItem> gunlist = new List<SelectListItem>();
            var gun = new[]
            {
                new SelectListItem{Value="Pazartesi",Text="Pazartesi"},
                new SelectListItem{Value="Salı",Text="Salı"},
                new SelectListItem{Value="Çarşamba",Text="Çarşamba"},
                new SelectListItem{Value="Perşembe",Text="Perşembe"},
                new SelectListItem{Value="Cuma",Text="Cuma"},
                new SelectListItem{Value="Cumartesi",Text="Cumartesi"},
                new SelectListItem{Value="Pazar",Text="Pazar"},
            };
            gunlist = gun.ToList();
            List<SelectListItem> saatlist = new List<SelectListItem>();
            var saat = new[]
            {
                new SelectListItem{Value="09.00",Text="09.00"},
                new SelectListItem{Value="10.00",Text="10.00"},
                new SelectListItem{Value="11.00",Text="11.00"},
                new SelectListItem{Value="12.00",Text="12.00"},
                new SelectListItem{Value="13.00",Text="13.00"},
                new SelectListItem{Value="14.00",Text="14.00"},
                new SelectListItem{Value="15.00",Text="15.00"},
                new SelectListItem{Value="16.00",Text="16.00"},
                new SelectListItem{Value="17.00",Text="17.00"},
                new SelectListItem{Value="18.00",Text="18.00"},
            };
            saatlist = saat.ToList();
            List<SelectListItem> dlist = (from x in c.DoktorTable.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DoktorAd + " " + x.DoktorSoyad,
                                              Value = x.DoktorID.ToString()
                                          }).ToList();
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
            ViewBag.gunlist = gunlist;
            ViewBag.saatlist = saatlist;
            ViewBag.dlist = dlist;
            ViewBag.plist = plist;
            ViewBag.hlist = hlist;
            return View();
        }
        [HttpPost]
        public IActionResult RandevuEkle(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                c.RandevuTable.Add(randevu);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Hataa";
            return View();
        }
    }
}
