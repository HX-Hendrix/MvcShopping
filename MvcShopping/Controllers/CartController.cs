using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    public class CartController : BaseController
    {
        //MvcShoppingContext db = new MvcShoppingContext();
        //List<Cart> Carts
        //{
        //    get
        //    {
        //        if (Session["Carts"] == null)
        //        {
        //            Session["Carts"] = new List<Cart>();
        //        }
        //        return (Session["Carts"] as List<Cart>);
        //    }
        //    set
        //    {
        //        Session["Carts"] = value;
        //    }
        //}
        //
        // GET: /Cart/
        // 添加产品项目到购物车，如果没有传入Amount参数则默认购买数量为1
        [HttpPost]
        public ActionResult AddToCart(int ProductId, int Amount = 1)
        {
            var product = db.Products.Find(ProductId);
            // 验证产品是否存在
            if (product == null)
                return HttpNotFound();

            var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if (existingCart != null)
            {
                existingCart.Amount += 1;
            }
            else
            {
                this.Carts.Add(new Cart() { Product = product, Amount = Amount });
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Created);
        }
        // 显示当前的购物车项目
        public ActionResult Index()
        {
            return View(this.Carts);
        }

        // 移除购物车项目
        [HttpPost]
        public ActionResult Remove(int ProductId)
        {
            var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if (existingCart != null)
            {
                this.Carts.Remove(existingCart);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        // 更新购物车中特定项目的购买数量
        [HttpPost]
        public ActionResult UpdateAmount(List<Cart> Carts)
        {
            foreach (var item in Carts)
            {
                var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == item.Product.Id);
                if (existingCart != null)
                {
                    existingCart.Amount = item.Amount;
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
