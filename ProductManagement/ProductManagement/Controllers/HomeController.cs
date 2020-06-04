using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetProductList(string TextSearch)
        {
            var filterText = !string.IsNullOrEmpty(TextSearch) ? TextSearch.ToLower() : null;

            ProductService productRepo = new ProductService();
            List<Product> lst = new List<Product>();
            lst = productRepo.GetProductInfo(filterText);
            return Json(new
            {
                aaData = lst
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductFormPartialView(int? id)
        {
            ProductService productRepo = new ProductService();
            var model = productRepo.GetProductInfo("").Find(p => p.ProductId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveProduct(Product model)
        {
            ProductService productRepo = new ProductService();
            List<string> messages = new List<string>();
            bool responseStatus = true;

            if (!ModelState.IsValid)
            {
                responseStatus = false;
                messages = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                messages.Add("Something wrong...please contact to admin.");
            }

            try
            {
                var modeldata = productRepo.GetProductInfo("").Find(p => p.ProductId == model.ProductId);
                if (responseStatus && model.ProductId != null && modeldata != null)
                {
                    AddUpdateProduct(model);
                    responseStatus = true;
                    messages.Add("Product data updated successfully.");
                }
                else if (responseStatus)
                {
                    AddUpdateProduct(model);
                    responseStatus = true;
                    messages.Add("Product data inserted successfully.");
                }

            }
            catch (Exception ex)
            {

                responseStatus = false;
                messages.Add("something went wrong, please contact your administrator." + ex.Message);
            }


            return Json(new { messages, responseStatus }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private void AddUpdateProduct(Product model)
        {
            ProductService objservice = new ProductService();
            objservice.SaveProductInformation(model);
        }

        public ActionResult Delete(int id)
        {
            ProductService objservice = new ProductService();
            objservice.DeleteProductInfoById(id);

            return Json(new { }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}