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
    public class TransactionController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TransactionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44356/api/transactionsdata/");
        }
        // GET: Transaction
        public ActionResult Index()
        {
            string url = "GetTransactions";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<TransactionDto> transactions = response.Content.ReadAsAsync<IEnumerable<TransactionDto>>().Result;
            return View(transactions);
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id)
        {
            string url = "GetTransaction/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TransactionDto selectedtransactions = response.Content.ReadAsAsync<TransactionDto>().Result;
            return View(selectedtransactions);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        public ActionResult Create(TransactionDto transactionDto)
        {
            try
            {
                // TODO: Add insert logic here
                Transaction transaction = new Transaction()
                {
                    TransactionId = transactionDto.TransactionId,
                    BuyerInfo = transactionDto.BuyerInfo,
                    TransactionType = transactionDto.TransactionType,
                    TransactionDate = transactionDto.TransactionDate,
                    TransactionAmount = transactionDto.TransactionAmount,
                    AgentId = transactionDto.AgentId,
                    PropertyId = transactionDto.PropertyId,                    
                };

                string url = "PostTransaction";


                string jsonpayload = jss.Serialize(transaction);

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

        // GET: Transaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
