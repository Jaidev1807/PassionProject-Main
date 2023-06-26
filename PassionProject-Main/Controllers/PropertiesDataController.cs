using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_Main.Models;

namespace PassionProject_Main.Controllers
{
    public class PropertiesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PropertiesData
        public IEnumerable<PropertyDto> GetProperties()
        {
            List<Property> properties = db.Properties.ToList();
            List<PropertyDto> propertyDtos = new List<PropertyDto>();

            properties.ForEach(p => propertyDtos.Add(new PropertyDto()
            {
                PropertyId = p.PropertyId,
                Address = p.Address,
                NoOfRooms = p.NoOfRooms,
                Price = p.Price,
                Size = p.Size,
                Type = p.Type,
            }));

            return propertyDtos;
            
        }

        // GET: api/PropertiesData/5
        [ResponseType(typeof(PropertyDto))]
        public IHttpActionResult GetProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            PropertyDto propertyDto = new PropertyDto()
            {
                PropertyId = property.PropertyId,
                Address = property.Address,
                NoOfRooms = property.NoOfRooms,
                Price = property.Price,
                Size = property.Size,
                Type = property.Type,
            };

            return Ok(propertyDto);
        }

        // PUT: api/PropertiesData/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PropertiesData
        [HttpPost]
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        // DELETE: api/PropertiesData/5
        [HttpPost]
        [ResponseType(typeof(Property))]
        public IHttpActionResult DeleteProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(property);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}