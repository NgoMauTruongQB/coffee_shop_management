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
    public class UserAccountController : Controller
    {
        private static UserAccountDAO userAccountDAO = new UserAccountDAO();

        [HttpGet]
        public ActionResult getAll()
        {
            List<UserAccount> listUser = userAccountDAO.getAll();
            ViewBag.listUser = listUser;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string userName, string password)
        {
            UserAccount user = new UserAccount(userName, password, 0);
            int check = userAccountDAO.isValid(user);
            if(check == 0)
            {
                return RedirectToAction("Index", "Home");
            } 
            else if (check == 1)
            {
                return RedirectToAction("getAll", "UserAccount");
            } 
            else
            {
                return RedirectToAction("GetOrderProduct", "Orders");
            }
        }

        [HttpPost]
        public ActionResult InsertAccount(string iusername, string ipassword, byte irole)
        {
            ViewBag.errorInsert = "";
            try
            {
                UserAccount user = new UserAccount(iusername, ipassword, irole);
                userAccountDAO.insert(user);
            }
            catch (Exception)
            {
                ViewBag.errorInsert = "Tài khoản đã tồn tại";
            }
            return RedirectToAction("getAll", "UserAccount");
        }

        [HttpGet]
        public ActionResult DeleteAccount(string userName)
        {
            try
            {
                userAccountDAO.delete(userName);
            }
            catch(Exception)
            {
                
            }
            return RedirectToAction("getAll", "UserAccount");
        }

        [HttpPost]
        public ActionResult UpdateUserAccount(string userName, string password)
        {
            try
            {
                userAccountDAO.update(new UserAccount(userName, password));
            }
            catch (Exception)
            {

            }
            return RedirectToAction("getAll", "UserAccount");
        }
    }
}