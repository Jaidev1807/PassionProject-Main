using PassionProject_Main.Migrations;
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
    public class PropertyController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PropertyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44356/api/propertiesdata/");
        }
        // GET: Property
        public ActionResult Index()
        {
            string url = "GetProperties";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PropertyDto> properties = response.Content.ReadAsAsync<IEnumerable<PropertyDto>>().Result;
            return View(properties);
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            string url = "GetProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PropertyDto selectedproperty = response.Content.ReadAsAsync<PropertyDto>().Result;

            return View(selectedproperty);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(PropertyDto propertyDto)
        {
            try
            {
                // TODO: Add insert logic here
                Property property = new Property()
                {
                    PropertyId = propertyDto.PropertyId,
                    Address = propertyDto.Address,
                    NoOfRooms = propertyDto.NoOfRooms,
                    Price = propertyDto.Price,
                    Size = propertyDto.Size,
                    Type = propertyDto.Type,
                };

                string url = "PostProperty";


                string jsonpayload = jss.Serialize(property);

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

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "GetProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PropertyDto selectedproperty = response.Content.ReadAsAsync<PropertyDto>().Result;

            return View(selectedproperty);
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PropertyDto propertyDto)
        {
            try
            {
                // TODO: Add update logic here
                Property property = new Property()
                {
                    PropertyId = id,
                    Address = propertyDto.Address,
                    NoOfRooms = propertyDto.NoOfRooms,
                    Price = propertyDto.Price,
                    Size = propertyDto.Size,
                    Type = propertyDto.Type,
                };

                string url = "PutProperty/" + id;
                string jsonpayload = jss.Serialize(property);

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

        // GET: Property/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "GetProperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PropertyDto selectedproperty = response.Content.ReadAsAsync<PropertyDto>().Result;

            return View(selectedproperty);
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PropertyDto propertyDto)
        {
            try
            {
                // TODO: Add delete logic here
                Property property = new Property()
                {
                    PropertyId = id,
                    Address = propertyDto.Address,
                    NoOfRooms = propertyDto.NoOfRooms,
                    Price = propertyDto.Price,
                    Size = propertyDto.Size,
                    Type = propertyDto.Type,
                };

                string url = "DeleteProperty/" + id;
                string jsonpayload = jss.Serialize(property);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id = id });
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
