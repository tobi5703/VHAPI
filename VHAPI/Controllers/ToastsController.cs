using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VHAPI.Models;

namespace VHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToastsController : ControllerBase
    {
        private readonly WebContext _context;

        public ToastsController(WebContext context)
        {
            _context = context;
        }

        // GET: api/Toasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Toast>>> GetToast()
        {
            return await _context.Toasts.ToListAsync();
        }

        // GET: api/Toasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Toast>> GetToast(int id)
        {
            var toast = await _context.Toasts.FindAsync(id);

            if (toast == null)
            {
                return NotFound();
            }

            return toast;
        }

        // PUT: api/Toasts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToast(int id, Toast toast)
        {
            if (id != toast.Id)
            {
                return BadRequest();
            }

            _context.Entry(toast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Toasts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Toast>> PostToast(Toast toast)
        {
            _context.Toasts.Add(toast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToast", new { id = toast.Id }, toast);
        }

        // DELETE: api/Toasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Toast>> DeleteToast(int id)
        {
            var toast = await _context.Toasts.FindAsync(id);
            if (toast == null)
            {
                return NotFound();
            }

            _context.Toasts.Remove(toast);
            await _context.SaveChangesAsync();

            return toast;
        }

        private bool ToastExists(int id)
        {
            return _context.Toasts.Any(e => e.Id == id);
        }
    }
}
