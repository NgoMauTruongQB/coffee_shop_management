using CoffeeManagement.Models.DAL;
using CoffeeManagement.Models.DAL.Implement;
using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeManagement.Controllers
{
    public class OrdersController : Controller
    {
        private static OrdersDAO ordersDAO = new OrdersDAO();
        private static Order_detailDAO order_DetailDAO = new Order_detailDAO();
        private static ProductDAO productDAO = new ProductDAO();

        public ActionResult Order()
        {
            return View();
        }

        // GET: Orders
        public string Index()
        {
            return "hello";
        }

        public ActionResult GetAll()
        {
            List<Orders> list = ordersDAO.getAll();
            return View(list);

        }


        public int GetTableNameById()
        {
            Orders orders =  ordersDAO.getById(1);
            return orders.TableNumber;
        }

        [HttpPost]
        public ActionResult OrderProduct(int numberOrder, int productId, int quantity)
        {
            try
            {
                if (ordersDAO.getById(numberOrder) == null)
                {
                    ordersDAO.insert(new Orders(numberOrder, numberOrder, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), 0));
                }

                order_DetailDAO.insert(new Order_detail(numberOrder, productId, quantity));
            }
            catch (Exception)
            {
            }
            return RedirectToAction("GetOrderProduct", "Orders");
        }

        [HttpGet]
        public ActionResult GetOrderProduct(int numberOrder = 1)
        {
            ViewBag.totalCost = -1;
            List<Product> listProduct = productDAO.getAll();
            ViewBag.listProduct = listProduct;
            ViewBag.table = numberOrder;
            Orders check = ordersDAO.getById(numberOrder);
            if (check != null && check.State != 1)
            {
                Orders db = ordersDAO.getOrderProduct(numberOrder);
                if (db != null)
                {
                    ViewBag.listOrderQuantity = db.Quantity;
                    ViewBag.listOrderProduct = db.ListProduct;
                    ViewBag.totalCost = ordersDAO.getTotalCostOfOrder(numberOrder);
                    ViewBag.numberTable = numberOrder;
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult payBill(int numberOrder)
        {
            try
            {
                ordersDAO.updateOrderState(numberOrder, 1);
            }
            catch(Exception)
            {
            }
            return RedirectToAction("GetOrderProduct", "Orders");

        }

        [HttpGet]
        public ActionResult Sales()
        {
            ViewBag.checkNull = -1;
            List<DateTime> listDate = ordersDAO.getAllDate();
            List<double> listCost = ordersDAO.getTotalSales();
            if(listDate != null && listCost != null)
            {
                ViewBag.listDate = listDate;
                ViewBag.listCost = listCost;
                ViewBag.checkNull = 0;
            }
            return View();
        }
    }
}