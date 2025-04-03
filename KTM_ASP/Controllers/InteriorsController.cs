using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KTM_ASP.Data;
using KTM_ASP.Models;

namespace KTM_ASP.Controllers
{
    public class InteriorsController : Controller
    {
        private readonly KTM_ASPContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public InteriorsController(KTM_ASPContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Interiors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Interiors.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interiors interiors)
        {
            if (ModelState.IsValid)
            {
                if (interiors.AnhFile != null)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(interiors.AnhFile.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await interiors.AnhFile.CopyToAsync(stream);
                    }

                    interiors.HinhAnh = "/images/" + fileName;
                }

                _context.Add(interiors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interiors);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var interiors = await _context.Interiors.FindAsync(id);
            if (interiors == null) return NotFound();

            return View(interiors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Interiors interiors)
        {
            if (id != interiors.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingInterior = await _context.Interiors.FindAsync(id);
                    if (existingInterior == null) return NotFound();

                    if (interiors.AnhFile != null)
                    {
                        // Tạo thư mục lưu ảnh nếu chưa có
                        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        // Tạo tên file ngẫu nhiên để tránh trùng
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(interiors.AnhFile.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);

                        // Lưu file ảnh mới
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await interiors.AnhFile.CopyToAsync(stream);
                        }

                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(existingInterior.HinhAnh))
                        {
                            string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingInterior.HinhAnh.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Lưu đường dẫn ảnh mới vào DB
                        existingInterior.HinhAnh = "/images/" + fileName;
                    }

                    // Cập nhật các trường khác
                    existingInterior.TenSanPham = interiors.TenSanPham;
                    existingInterior.MieuTa = interiors.MieuTa;
                    existingInterior.SoLuong = interiors.SoLuong;
                    existingInterior.ChatLieu = interiors.ChatLieu;
                    existingInterior.MauSac = interiors.MauSac;
                    existingInterior.Gia = interiors.Gia;

                    _context.Update(existingInterior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteriorsExists(interiors.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(interiors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interiors = await _context.Interiors.FirstOrDefaultAsync(m => m.Id == id);
            if (interiors == null)
            {
                return NotFound();
            }

            return View(interiors);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var interiors = await _context.Interiors.FirstOrDefaultAsync(m => m.Id == id);
            if (interiors == null) return NotFound();

            return View(interiors);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interiors = await _context.Interiors.FindAsync(id);
            if (interiors != null)
            {
                if (!string.IsNullOrEmpty(interiors.HinhAnh))
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, interiors.HinhAnh.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Interiors.Remove(interiors);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleFavorite(int id)
        {
            var product = _context.Interiors.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.IsFavorite = !product.IsFavorite;
            _context.SaveChanges();

            return Json(new { success = true, isFavorite = product.IsFavorite });
        }

        private bool InteriorsExists(int id)
        {
            return _context.Interiors.Any(e => e.Id == id);
        }
    }
}



