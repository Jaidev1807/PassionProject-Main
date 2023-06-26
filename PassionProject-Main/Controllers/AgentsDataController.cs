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
    public class AgentsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AgentsData
        public IEnumerable<AgentDto> GetAgents()
        {
            List<Agent> agents = db.Agents.ToList();
            List<AgentDto> agentDtos = new List<AgentDto>();

            agents.ForEach(a => agentDtos.Add(new AgentDto()
            {
                AgentId = a.AgentId,
                Name = a.Name,
                Sepcialization = a.Sepcialization,
                PerfomanceMetrics = a.PerfomanceMetrics,
                CommissionRate = a.CommissionRate,
                ConatactInfo = a.ConatactInfo,
            }));

            return agentDtos;
        }

        // GET: api/AgentsData/5
        [ResponseType(typeof(AgentDto))]
        public IHttpActionResult GetAgent(int id)
        {
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return NotFound();
            }

            AgentDto agentDto = new AgentDto()
            {
                AgentId = agent.AgentId,
                Name = agent.Name,
                Sepcialization = agent.Sepcialization,
                PerfomanceMetrics = agent.PerfomanceMetrics,
                CommissionRate = agent.CommissionRate,
                ConatactInfo = agent.ConatactInfo,
            };

            return Ok(agentDto);
        }

        // PUT: api/AgentsData/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgent(int id, Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agent.AgentId)
            {
                return BadRequest();
            }

            db.Entry(agent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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

        // POST: api/AgentsData
        [HttpPost]
        [ResponseType(typeof(Agent))]
        public IHttpActionResult PostAgent(Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agents.Add(agent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agent.AgentId }, agent);
        }

        // DELETE: api/AgentsData/5
        [HttpPost]
        [ResponseType(typeof(Agent))]
        public IHttpActionResult DeleteAgent(int id)
        {
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return NotFound();
            }

            db.Agents.Remove(agent);
            db.SaveChanges();

            return Ok(agent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgentExists(int id)
        {
            return db.Agents.Count(e => e.AgentId == id) > 0;
        }
    }
}