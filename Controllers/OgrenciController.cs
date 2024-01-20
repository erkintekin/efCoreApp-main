using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace efCoreApp.Controllers
{

    public class OgrenciController : Controller
    {
        // Önemli
        private readonly DataContext _context;


        // Dependency Injection
        public OgrenciController(DataContext context)
        {
            // Dependency Injection - Veritabanına bağlılığı en aza indirmek. Yeni nesne devamlı üretmemek. Mevcut nesneyi referans olarak çağırmak.
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci? model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); // Index sayfasına HomeController gönderdik. Buradaki Index boş çünkü.
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();

            return View(ogrenciler);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(s => s.OgrenciId == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);


        }

        [HttpPost]
        public async Task<IActionResult> DeleteX([FromForm] int? id)
        {
            Ogrenci? selectedStudent = await _context.Ogrenciler.FirstOrDefaultAsync(s => s.OgrenciId == id);

            if (selectedStudent == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(selectedStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Ogrenci? selectedStudent = await _context.Ogrenciler.Include(o => o.KursKayitlar).ThenInclude(o => o.Kurs).FirstOrDefaultAsync(s => s.OgrenciId == id);

            if (selectedStudent == null)
            {
                return NotFound();
            }

            return View(selectedStudent);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ogrenci? model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Ogrenciler.Update(model);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(x => x.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                return RedirectToAction("Index");
            }
            return View(model);


        }


    }
}