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
    public class ProductController : Controller
    {
        private static ProductDAO productDAO = new ProductDAO();

        [HttpGet]
        public ActionResult getAll()
        {
            List<Product> listProduct = productDAO.getAll();
            ViewBag.listProduct = listProduct;
            return View();
        }

        [HttpPost]
        public ActionResult InsertProduct(int id, string productName, double cost)
        {
            try
            {
                Product product = new Product(id, productName, cost);
                productDAO.insert(product);
            }
            catch (Exception)
            {
                ViewBag.errorInsert = "Thất bại";
            }           
            return RedirectToAction("getAll", "Product");
        }

        [HttpPost]
        public ActionResult UpdateProduct(int id, string nameProduct, double cost)
        {
            try
            {
                productDAO.update(new Product(id, nameProduct, cost));
            }
            catch (Exception)
            {

            }
            return RedirectToAction("getAll", "Product");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int idProductDelete)
        {
            try
            {
                productDAO.delete(idProductDelete);
            }
            catch (Exception)
            {

            }
            return RedirectToAction("getAll", "Product");
        }
    }
}