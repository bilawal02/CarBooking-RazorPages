﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarBookingModels.Models;
using CarBookingWeb.DataContext;
using CarBookingRepository.Contract;

namespace CarBookingWeb.Pages.CarMakerPages
{
    //[Authorize]
    public class EditModel : PageModel
    {
        //private readonly ApplicationDbContext _context;
        //public EditModel(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IGenericRepository<CarMaker> _repository;
        public EditModel(IGenericRepository<CarMaker> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public CarMaker CarMaker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var carmaker =  await _context.CarMakers.FirstOrDefaultAsync(m => m.Id == id);
            var carmaker = await _repository.GetSingleAsync(id.Value);
            if (carmaker == null)
            {
                return NotFound();
            }
            CarMaker = carmaker;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CarMaker.CreatedDate = DateTime.Now;
            //_context.Attach(CarMaker).State = EntityState.Modified;
            await _repository.EditAsync(CarMaker);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CarMakerExists(CarMaker.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private async Task<bool> CarMakerExists(int id)
        {
            //return _context.CarMakers.Any(e => e.Id == id);
            return await _repository.ExistsAsync(id);
        }
    }
}
