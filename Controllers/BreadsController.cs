using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotnetBakery.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreadsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BreadsController(ApplicationContext context) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Bread> GetAll(){
            return _context.Breads.Include(Baker => Baker.bakedBy);
        }

        [HttpGet("{id}")]
        public ActionResult<Bread> GetById(int id){
            Bread bread = _context.Breads
            .Include(Baker => Baker.bakedBy)
            .SingleOrDefault(bread => bread.id ==id );

            if (bread is null)
            {
                return NotFound();
            }

            return bread;
        }

        [HttpPost]
        public IActionResult Post(Bread bread){
            _context.Add(bread);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Post), new {id = bread.id}, bread);
        }

        //PUT /api/bread/:id
        //returns NoContent()
        //Bread must contain all fields that are NOT NULL
        // nullables will be filled with NULL if They are missing from the request body JSON
        [HttpPut("{id}")]
        public IActionResult Put(int id, Bread bread) {
            Console.WriteLine("in PUT");
            if (id != bread.id) {
                return BadRequest();
            }
            // update in DB
            _context.Update(bread);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Bread bread = _context.Breads.SingleOrDefault(b => b.id == id);

            if(bread is null) {
                return NotFound();
            }

            _context.Breads.Remove(bread);
            _context.SaveChanges(); // really make the change            

            // 204
            return NoContent();
        }
    }
}
