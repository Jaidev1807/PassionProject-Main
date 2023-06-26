using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Owin.BuilderProperties;
using System.Web.Util;
using System.Xml.Linq;
using PassionProject_Main.Migrations;
using PassionProject_Main.Models;
using System.Web.Http.Controllers;

namespace PassionProject_Main.Controllers
{
    public class TransactionsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TransactionsData
        [HttpGet]
        public IHttpActionResult GetTransactions()
        {
            List<Transaction> transactions = db.Transactions.ToList();
            List<TransactionDto> transactionDtos = new List<TransactionDto>();

            transactions.ForEach(t => transactionDtos.Add(new TransactionDto()
            {
                TransactionId = t.TransactionId,
                BuyerInfo = t.BuyerInfo,
                TransactionType = t.TransactionType,
                TransactionDate = t.TransactionDate,
                TransactionAmount = t.TransactionAmount,
                AgentId = t.Agent.AgentId,
                PropertyId = t.Property.PropertyId,
            }));
             

            return Ok(transactionDtos) ; 
        }

        // GET: api/TransactionsData/5
        [ResponseType(typeof(TransactionDto))]
        [HttpGet]
        public IHttpActionResult GetTransaction(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            TransactionDto transactionDto = new TransactionDto()
            {
                TransactionId = transaction.TransactionId,
                BuyerInfo = transaction.BuyerInfo,
                TransactionType = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate,
                TransactionAmount = transaction.TransactionAmount,
                AgentId = transaction.Agent.AgentId,
                PropertyId = transaction.Property.PropertyId,
            };

            return Ok(transactionDto);
        }

        // PUT: api/TransactionsData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/TransactionsData
        [HttpPost]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/TransactionsData/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteTransaction(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.TransactionId == id) > 0;
        }
    }
}