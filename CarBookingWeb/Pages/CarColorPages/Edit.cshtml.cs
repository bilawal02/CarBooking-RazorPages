using CarBookingModels.Models;
using CarBookingRepository.Contract;
using CarBookingWeb.DataContext;
using CarBookingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarBookingWeb.Pages.CarColorPages
{
    //[Authorize]
    public class EditModel : PageModel
    {
        //private readonly ApplicationDbContext _context;
        //public EditModel(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IGenericRepository<CarColor> _repository;
        public EditModel(IGenericRepository<CarColor> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public CarColor CarColor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var carcolor = await _context.CarColors.FirstOrDefaultAsync(m => m.Id == id);
            var carcolor = await _repository.GetSingleAsync(id.Value);
            if (carcolor == null)
            {
                return NotFound();
            }
            CarColor = carcolor;
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
            CarColor.CreatedDate = DateTime.Now;
            //_context.Attach(CarColor).State = EntityState.Modified;

            await _repository.EditAsync(CarColor);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CarColorExists(CarColor.Id))
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

        //private bool CarColorExists(int id)
        //{
        //    return _context.CarMakers.Any(e => e.Id == id);
        //}

        private async Task<bool> CarColorExists(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
