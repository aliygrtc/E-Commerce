﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wooland.BL.Repositories;
using Wooland.DAL.Entities;

namespace Wooland.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class SlideController : Controller
    {
        IRepository<Slide> repoSlide;
        public SlideController(IRepository<Slide> _repoSlide)
        {
            repoSlide = _repoSlide;
        }
        public IActionResult Index()
        {
            return View(repoSlide.GetAll().OrderByDescending(x => x.ID));
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(Slide model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/images/slide/" + dosyaAdi;
                }
                repoSlide.Add(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }
        public IActionResult Edit(int id)
        {
            Slide slide = repoSlide.GetBy(x => x.ID == id);
            if (slide != null) return View(slide);
            else return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slide model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide")))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide"));
                    }
                    string dosyaAdi = Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "slide", dosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/images/slide/" + dosyaAdi;
                }
                repoSlide.Update(model);

                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");
        }

        public IActionResult Delete(int id)
        {
            Slide slide = repoSlide.GetBy(x => x.ID == id);
            if (slide != null)
            {
                if (!string.IsNullOrEmpty(slide.Picture))
                {
                    string _pathFile = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + slide.Picture.Replace("/", "\\");
                    FileInfo fileInfo = new FileInfo(_pathFile);
                    if (fileInfo.Exists) fileInfo.Delete();
                }
                repoSlide.Delete(slide);
            }
            return RedirectToAction("Index");
        }
    }
}
