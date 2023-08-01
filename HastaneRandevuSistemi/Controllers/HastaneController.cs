using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HastaneRandevuSistemi.Controllers
{
    public class HastaneController : Controller
    {
        AppDbContext c = new AppDbContext();
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
            var doks = c.DoktorTable.Include(x =>x.Poliklinik).Where(x => x.HastaneID == id).ToList();
            ViewBag.data = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id).HastaneAd;
            return View(doks);
        }

        [HttpGet]
        public IActionResult HastaneEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HastaneEkle(Hastane hastane)
        {
            if(ModelState.IsValid)
            {
                c.HastaneTable.Add(hastane);
                c.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.msj = "Ekleme Başarısız.";
            return View();
        }

        public IActionResult HastaneDuzenle(int? id)
        {
            if(id is null)
            {
                TempData["msj"] = "HATAAA!!";
                return RedirectToAction("Index");
            }
            var has = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id);
            if(has == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            return View(has);
        }

        [HttpPost]
        public IActionResult HastaneDuzenle(int? id,Hastane hastane)
        {
            if(hastane.HastaneID != id)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if(ModelState.IsValid)
            {
                try
                {
                    c.HastaneTable.Update(hastane);
                    c.SaveChanges();
                    TempData["msj"] = hastane.HastaneAd + "Güncellendi.";
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
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

        public IActionResult HastaneSil(int? id)
        {
            if(id == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            var has = c.HastaneTable.Include(x=> x.PoliklinikList).FirstOrDefault(x=> x.HastaneID == id);
            if(has == null)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            if(has.PoliklinikList.Count > 0)
            {
                TempData["msj"] = "HATAAA";
                return RedirectToAction("Index");
            }
            else
            {
                c.HastaneTable.Remove(has);
                c.SaveChanges();
                TempData["msj"] = has.HastaneAd + "Silindi.";
                return RedirectToAction("Index");
            }
        }

    }
}
