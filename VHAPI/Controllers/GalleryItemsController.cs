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
    public class GalleryItemsController : ControllerBase
    {
        private readonly WebContext _context;

        public GalleryItemsController(WebContext context)
        {
            _context = context;
        }

        // GET: api/GalleryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GalleryItem>>> GetGalleryItems()
        {
            return await _context.GalleryItems.ToListAsync();
        }

        // GET: api/GalleryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GalleryItem>> GetGalleryItem(int id)
        {
            var galleryItem = await _context.GalleryItems.FindAsync(id);

            if (galleryItem == null)
            {
                return NotFound();
            }

            return galleryItem;
        }

        // PUT: api/GalleryItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGalleryItem(int id, GalleryItem galleryItem)
        {
            if (id != galleryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(galleryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryItemExists(id))
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

        // POST: api/GalleryItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GalleryItem>> PostGalleryItem(GalleryItem galleryItem)
        {
            _context.GalleryItems.Add(galleryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGalleryItem", new { id = galleryItem.Id }, galleryItem);
        }

        // DELETE: api/GalleryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GalleryItem>> DeleteGalleryItem(int id)
        {
            var galleryItem = await _context.GalleryItems.FindAsync(id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            _context.GalleryItems.Remove(galleryItem);
            await _context.SaveChangesAsync();

            return galleryItem;
        }

        private bool GalleryItemExists(int id)
        {
            return _context.GalleryItems.Any(e => e.Id == id);
        }
    }
}
