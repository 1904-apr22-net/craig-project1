using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkateShop.Library.Interfaces;
using SkateShop.Library.Models;
using SkateShop.WebUI.Models;

namespace SkateShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        public ISkateShopRepository Repo { get; }

        public OrderController(ISkateShopRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        // GET: Order
        public ActionResult Index()
        {
            IEnumerable<Order> orders = Repo.GetOrders();
            IEnumerable<OrderViewModel> items = orders.Select(x => new OrderViewModel
            {
                OrderId = x.OrderId,
                Time = x.Time,
                Quantity = x.Quantity,
                Price = x.Total
            });
            return View(items);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Order order = Repo.GetOrderById(id);
            Customer customer = Repo.GetCustomerById(order.CustomerId);
            OrderViewModel viewModel = new OrderViewModel
            {
                OrderId = order.OrderId,
                Price = order.Total,
                Quantity = order.Quantity,
                Time = order.Time,
                Products = order.Products.Select(x => new ProductViewModel
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Price = (decimal)x.Price
                }).ToList(),
                Customer = new CustomerViewModel
                {
                    CustomerId = customer.CustomerId,
                    Address = $"{customer.Address}, {customer.City}, {customer.State}",
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                }
            };
            return View(viewModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var customers = Repo.GetCustomers();
            var locations = Repo.GetLocations();
            var products = Repo.GetProducts();
            OrderViewModel viewModel = new OrderViewModel();
            foreach (Customer customer in customers)
            {
                viewModel.CustomerList.Add(new SelectListItem($"{customer.FirstName} {customer.LastName}", customer.CustomerId.ToString()));
            }
            foreach (Location location in locations)
            {
                viewModel.LocationList.Add(new SelectListItem($"{location.City}, {location.State}", location.LocationId.ToString()));
            }
            foreach(Product product in products)
            {
                viewModel.Products.Add(new ProductViewModel { Name = product.Name, Price = (decimal)product.Price, ProductId = product.ProductId });
                viewModel.Cart.Add(product.Name, 0);
            }
            for(int i=0; i<= 10; i++)
            {
                viewModel.QuantityList.Add(new SelectListItem(i.ToString(), i.ToString()));
            }

            return View(viewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Product> cart = new List<Product>();
                    int quantity = 0;
                    decimal total = 0;
                    foreach(KeyValuePair<string, int> item in viewModel.Cart)
                    {
                        if(item.Value != 0)
                        {
                            for(int i=0; i<item.Value; i++)
                            {
                                cart.Add(Repo.GetProducts().Where(x => x.Name == item.Key).FirstOrDefault());
                                quantity++;
                                total += (decimal)cart[i].Price;
                            }
                        }
                    }
                    int.TryParse(viewModel.LocationId, out var locationId);
                    int.TryParse(viewModel.CustomerId, out var customerId);
                    var order = new Order()
                    {
                        LocationId = locationId,
                        CustomerId = customerId,
                        Time = DateTime.Now,
                        Quantity = quantity,
                        Total = total
                    };
                    foreach(Product product in cart)
                    {
                        order.Products.Add(product);
                    }
                    Repo.PlaceOrder(order, cart);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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