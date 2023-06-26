using PassionProject_Main.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject_Main.Controllers
{
    public class AgentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AgentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44356/api/agentsdata/");
        }
        // GET: Agent
        public ActionResult Index()
        {
            string url = "GetAgents";
            HttpResponseMessage response = client.GetAsync(url).Result;


            IEnumerable<AgentDto> agents = response.Content.ReadAsAsync<IEnumerable<AgentDto>>().Result;
          
            return View(agents);
        }

        // GET: Agent/Details/5
        public ActionResult Details(int id)
        {
            string url = "GetAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AgentDto selectedagent = response.Content.ReadAsAsync<AgentDto>().Result;

            return View(selectedagent);
        }

        // GET: Agent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        [HttpPost]
        public ActionResult Create(AgentDto agentDto)
        {
            try
            {
                // TODO: Add insert logic here
                Agent agent = new Agent()
                {
                    AgentId = agentDto.AgentId,
                    Name = agentDto.Name,
                    Sepcialization = agentDto.Sepcialization,
                    ConatactInfo = agentDto.ConatactInfo,
                    CommissionRate = agentDto.CommissionRate,
                    PerfomanceMetrics  = agentDto.PerfomanceMetrics
                };

                string url = "PostAgent";


                string jsonpayload = jss.Serialize(agent);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Agent/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "GetAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AgentDto selectedagent = response.Content.ReadAsAsync<AgentDto>().Result;

            return View(selectedagent);
        }

        // POST: Agent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AgentDto agentDto)
        {
            try
            {
                // TODO: Add update logic here
                Agent agent = new Agent()
                {
                    AgentId = id,
                    Name = agentDto.Name,
                    Sepcialization = agentDto.Sepcialization,
                    ConatactInfo = agentDto.ConatactInfo,
                    CommissionRate = agentDto.CommissionRate,
                    PerfomanceMetrics = agentDto.PerfomanceMetrics
                };

                string url = "PutAgent/" + id;


                string jsonpayload = jss.Serialize(agent);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Agent/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "GetAgent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AgentDto selectedagent = response.Content.ReadAsAsync<AgentDto>().Result;

            return View(selectedagent);
        }

        // POST: Agent/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,  AgentDto agentDto)
        {
            try
            {
                // TODO: Add delete logic here
                // TODO: Add update logic here
                Agent agent = new Agent()
                {
                    AgentId = id,
                    Name = agentDto.Name,
                    Sepcialization = agentDto.Sepcialization,
                    ConatactInfo = agentDto.ConatactInfo,
                    CommissionRate = agentDto.CommissionRate,
                    PerfomanceMetrics = agentDto.PerfomanceMetrics
                };

                string url = "DeleteAgent/" + id;


                string jsonpayload = jss.Serialize(agent);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
