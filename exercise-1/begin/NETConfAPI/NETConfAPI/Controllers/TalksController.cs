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
    public class TalksController : Controller
    {
        private readonly NETConfContext context;

        public TalksController(NETConfContext context)
        {
            this.context = context;

            if (this.context.Conferences.Count() == 0) 
            {
                Utils.InitContext(context);
            }
        }

        // GET api/talks
        [HttpGet]
        public IEnumerable<Talk> Get()
        {
            var items = this.context.Talks
                            .Include(c => c.Speaker);

            return items.ToList();
        }

        // GET api/talks/1
        [HttpGet("{id}", Name = "GetTalkById")]
        public IActionResult GetById(int id)
        {
            var item = this.context.Talks
                           .Include(c => c.Speaker)
                           .FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/talks
        [HttpPost]
        public IActionResult Post([FromBody]Talk item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            this.context.Talks.Add(item);
            this.context.SaveChanges();

            return CreatedAtRoute("GetTalkById", new { id = item.Id }, item);
        }

        // PUT api/talks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Talk updatedItem)
        {
            if (updatedItem == null || updatedItem.Id != id)
            {
                return BadRequest();
            }

            var item = this.context.Talks.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Title = updatedItem.Title;
            item.Speaker = updatedItem.Speaker;
            item.SpeakerId = updatedItem.SpeakerId;

            this.context.Talks.Update(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/talks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.context.Talks.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            this.context.Talks.Remove(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }
    }
}
