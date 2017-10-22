using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETConfAPI.Models;

namespace NETConfAPI.Controllers
{
    [Route("api/[controller]")]
    public class ConferencesController : Controller
    {
        private readonly NETConfContext context;

        public ConferencesController(NETConfContext context)
        {
            this.context = context;

            if (this.context.Conferences.Count() == 0) 
            {
                Utils.InitContext(context);
            }
        }

        // GET api/conferences
        [HttpGet]
        public IEnumerable<Conference> Get()
        {
            var items = this.context.Conferences
                            .Include(c => c.Talks)
                            .ThenInclude(d => d.Speaker);

            return items.ToList();
        }

        // GET api/conferences/1
        [HttpGet("{id}", Name = "GetConferencesById")]
        public IActionResult GetById(int id)
        {
            var item = this.context.Conferences
                           .Include(c => c.Talks)
                           .ThenInclude(d => d.Speaker)
                           .FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/conferences
        [HttpPost]
        public IActionResult Post([FromBody]Conference item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            this.context.Conferences.Add(item);
            this.context.SaveChanges();

            return CreatedAtRoute("GetConferencesById", new { id = item.Id }, item);
        }

        // PUT api/conferences/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Conference updatedItem)
        {
            if (updatedItem == null || updatedItem.Id != id)
            {
                return BadRequest();
            }

            var item = this.context.Conferences.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;
            item.Year = updatedItem.Year;

            this.context.Conferences.Update(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/conferences/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.context.Conferences
                           .Include(c => c.Talks)
                           .FirstOrDefault(t => t.Id == id);
            
            if (item == null)
            {
                return NotFound();
            }

            // Remove talks
            foreach(var talk in item.Talks)
            {
               this.context.Talks.Remove(talk);
            }

            this.context.Conferences.Remove(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }
    }
}
