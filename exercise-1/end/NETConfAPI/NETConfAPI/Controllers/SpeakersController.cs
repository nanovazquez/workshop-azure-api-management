using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETConfAPI.Models;

namespace NETConfAPI.Controllers
{
    [Route("api/[controller]")]
    public class SpeakersController : Controller
    {
        private readonly NETConfContext context;

        public SpeakersController(NETConfContext context)
        {
            this.context = context;

            if (this.context.Conferences.Count() == 0)
            {
                Utils.InitContext(context);
            }
        }

        // GET api/speakers
        [HttpGet]
        public IEnumerable<Speaker> Get()
        {
            var items = this.context.Speakers;

            return items.ToList();
        }

        // GET api/speakers/1
        [HttpGet("{id}", Name = "GetSpeakersById")]
        public IActionResult GetById(int id)
        {
            var item = this.context.Speakers.FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/speakers
        [HttpPost]
        public IActionResult Post([FromBody]Speaker item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            this.context.Speakers.Add(item);
            this.context.SaveChanges();

            return CreatedAtRoute("GetSpeakersById", new { id = item.Id }, item);
        }

        // PUT api/speakers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Speaker updatedItem)
        {
            if (updatedItem == null || updatedItem.Id != id)
            {
                return BadRequest();
            }

            var item = this.context.Speakers.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;

            this.context.Speakers.Update(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/speakers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.context.Speakers.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var hasTalks = this.context.Talks.Count(c => c.Speaker != null && c.Speaker.Id == id) != 0;

            if (hasTalks)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            this.context.Speakers.Remove(item);
            this.context.SaveChanges();
            return new NoContentResult();
        }
    }
}
