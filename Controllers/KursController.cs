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

    public class KursController : Controller
    {

        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            if (ModelState.IsValid)
            {

                _context.Kurslar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Kurs model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Kurslar.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kurslar.Any(x => x.KursId == model.KursId))
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

        public async Task<IActionResult> Delete(int? id)
        {

            var courses = await _context.Kurslar.FirstOrDefaultAsync(s => s.KursId == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);


        }

        [HttpPost]
        public async Task<IActionResult> DeleteX(Kurs model)
        {

            _context.Kurslar.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}