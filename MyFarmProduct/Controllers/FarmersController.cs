using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFarmProduct.Data;
using MyFarmProduct.Models;
using MyFarmProduct.Models.ViewModels;

namespace MyFarmProduct.Controllers
{
    public class FarmersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Farmers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Farmers.Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Farmers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Farmers == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>(_context);
                if (!userStore.Users.Any(u => u.UserName == model.Email))
                {
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        NormalizedUserName = model.Email.ToUpper(),
                        Email = model.Email,
                        NormalizedEmail = model.Email.ToUpper(),
                        PhoneNumber = model.Contact,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user, model.Password);
                    user.PasswordHash = hashed;

                    var result = await userStore.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                    await userStore.AddToRoleAsync(user, "FARMER");
                    Farmer farmer = new Farmer
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Address = model.Address,
                        City = model.City,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        Contact = model.Contact,
                        Email = model.Email,
                        FarmSize = model.FarmSize,
                        AdditionalInfo = model.AdditionalInfo,
                        UserId = user.Id,
                    };
                    farmer.Id = Guid.NewGuid();

                    _context.Add(farmer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }
                return View(model);
            }
            return View(model);
        }

        // GET: Farmers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Farmers == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }
            return View(farmer);
        }

        // POST: Farmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,City,State,ZipCode,Contact,Email,FarmSize,AdditionalInfo,UserId")] Farmer farmer)
        {
            if (id != farmer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }

        // GET: Farmers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Farmers == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Farmers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Farmers'  is null.");
            }
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmerExists(Guid id)
        {
            return (_context.Farmers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
