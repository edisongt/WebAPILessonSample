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
using WebAPILessonOne.Models;

namespace WebAPILessonOne.Areas.HelpPage.Controllers
{
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Clients
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("clients/{id:int}",Name = "GetClientByID")]
        public Client GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            //if (client == null)
            //{
            //    return NotFound();
            //}

            return client;
        }

        [Route("clients/{id:int}/orders")]
        public IHttpActionResult GetOrderByClientId(int id)
        {
            var orders = db.Orders.Where(x => x.ClientId == id);
            if (!orders.Any())
            {
                return RedirectToRoute("GetClientById", new { id = id });
            }

            return Ok(orders);
        }



        [Route("clients/{id:int}/orders/{oid:int}")]
        public IHttpActionResult GetOrderByOrderId(int id, int oid)
        {
            var orders = db.Orders.Where(x => x.ClientId == id
                                            && x.OrderId == oid);
            return Ok(orders);
        }


        [Route("clients/{id:int}/orders/{oid:int}")]
        public IHttpActionResult GetOrdersByClientIdOrderId(int id, int oid)
        {
            var orders = db.Orders
                .Where(p => p.ClientId == id && p.OrderId == oid);
            return Ok(orders);
        }

        [Route("clients/{id:int}/orders/{status:alpha:length(1)}")]
        public IHttpActionResult GetOrdersByClientIdOrderStatus(int id, string status)
        {
            var orders = db.Orders
                .Where(p => p.ClientId == id && p.OrderStatus == status);
            return Ok(orders);
        }

        [Route("clients/{id:int}/orders/{*odate:datetime}")]
        public IHttpActionResult GetOrdersByClientIdOrderDate(int id, DateTime odate)
        {
            var orders = db.Orders
                .Where(p => p.ClientId == id && p.OrderDate > odate);
            return Ok(orders);
        }




        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
        }
    }
}