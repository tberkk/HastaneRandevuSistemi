using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HastaneRandevuSistemi.Controllers
{
    public class HastaneController : Controller
    {
        private readonly AppDbContext c;
        public HastaneController(AppDbContext context)
        {
            c = context;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var list = c.HastaneTable.ToList();
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PoliklinikGetir(int id)
        {
            var poli = c.PoliklinikTable.Where(x => x.HastaneID == id).ToList();
            ViewBag.data = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id).HastaneAd;
            return View(poli);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DoktorGetir(int id)
        {
            var doks = c.DoktorTable.Include(x =>x.Poliklinik).Where(x => x.HastaneID == id).ToList();
            ViewBag.data = c.HastaneTable.FirstOrDefault(x => x.HastaneID == id).HastaneAd;
            return View(doks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult HastaneEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
