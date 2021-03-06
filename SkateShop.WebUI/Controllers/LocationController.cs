﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkateShop.Library.Interfaces;
using SkateShop.Library.Models;
using SkateShop.WebUI.Models;

namespace SkateShop.WebUI.Controllers
{
    public class LocationController : Controller
    {
        public ISkateShopRepository Repo { get; }

        public LocationController(ISkateShopRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<Location> locations = Repo.GetLocations();
            IEnumerable<LocationViewModel> viewModels = locations.Select(x => new LocationViewModel
            {
                LocationId = x.LocationId,
                Address = $"{x.Address}, {x.City}, {x.State}"
            });
            return View(viewModels);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            Location location = Repo.GetLocationById(id);
            var viewModel = new LocationViewModel
            {
                LocationId = location.LocationId,
                Address = location.Address,
                City = location.City,
                State = location.State,
                Country = location.Country,
                Orders = location.Orders.Select(y => new OrderViewModel
                {
                    OrderId = y.OrderId,
                    Time = y.Time,
                    Quantity = y.Quantity,
                    Price = y.Total
                }).ToList()
            };
            return View(viewModel);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
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

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Location/Edit/5
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

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Location/Delete/5
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