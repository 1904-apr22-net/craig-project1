using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkateShop.DataAccess.Repositories;
using SkateShop.Library.Interfaces;
using SkateShop.Library.Models;
using SkateShop.WebUI.Models;

namespace SkateShop.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        public ISkateShopRepository Repo;

        public CustomerController(ISkateShopRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException();

        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = Repo.GetCustomers();
            IEnumerable<CustomerViewModel> items = customers.Select(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                LocationId = x.LocationId,
                Address = $"{x.Address}, {x.City}, {x.State}, {x.Country}",
                FirstName = x.FirstName,
                LastName = x.LastName,
            });
            return View(items);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            var customer = Repo.GetCustomerById(id);
            var viewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                LocationId = customer.LocationId,
                Address = $"{customer.Address}, {customer.City}, {customer.State}, {customer.Country}",
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = customer.Orders.Select(y => new OrderViewModel
                {
                    OrderId = y.OrderId,
                    Time = y.Time,
                    Quantity = y.Quantity,
                    Price = y.Total
                }).ToList()
            };
            return View(viewModel);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var customers = Repo.GetCustomers();
            var locations = Repo.GetLocations();
            OrderViewModel viewModel = new OrderViewModel();
            foreach(Customer customer in customers)
            {
                viewModel.CustomerList.Add(new SelectListItem($"{customer.FirstName} {customer.LastName}", customer.CustomerId.ToString()));
            }
            foreach(Location location in locations)
            {
                viewModel.LocationList.Add(new SelectListItem($"{location.City} {location.State}", location.LocationId.ToString()));
            }

            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}