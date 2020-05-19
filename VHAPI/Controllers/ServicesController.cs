using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VHAPI.Models;

namespace VHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly WebContext _context;
        private readonly IWebHostEnvironment _env;

        public ServicesController(WebContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices(int limit = 0)
        {
            if (limit > 0)
            {
                return await _context.Services.Take(limit).ToListAsync();
            }
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]   
        public async Task<IActionResult> PutService(int id, [FromForm]Service service, [FromForm]IFormFile image)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            if (image != null)
            {
                // Rememer to delete old picture!
                if (System.IO.File.Exists(_env.WebRootPath + @"\images@" + service.IMG)) ;
                {
                    System.IO.File.Delete(_env.WebRootPath + @"\images@" + service.IMG);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(_env.WebRootPath + @"\images\" + fileName);


                using (var stream = System.IO.File.Create(filePath)) 
                {
                    await image.CopyToAsync(stream);
                }
                service.IMG = fileName;
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Service>> PostService([FromForm]Service service, [FromForm]IFormFile image)
        {
            if (image != null)
            {
                //// Rememer to delete old picture!
                //if (System.IO.File.Exists(_env.WebRootPath + @"\images@" + service.IMG)) ;
                //{
                //    System.IO.File.Delete(_env.WebRootPath + @"\images@" + service.IMG);
                //}

                string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(_env.WebRootPath + @"\images\" + fileName);


                using (var stream = System.IO.File.Create(filePath))
                {
                    await image.CopyToAsync(stream);
                }
                service.IMG = fileName;
            }

                _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return service;
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
