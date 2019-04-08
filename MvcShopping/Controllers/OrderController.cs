using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Controllers
{
    [Authorize]  // 必须登陆会员才能使用订单结账功能
    public class OrderController : BaseController
    {
        //MvcShoppingContext db = new MvcShoppingContext();

        List<Cart> Carts
        {
            get
            {
                if (Session["Carts"] == null)
                {
                    Session["Carts"] = new List<Cart>();
                }
                return (Session["Carts"] as List<Cart>);
            }
            set { Session["Carts"] = value; }
        }
        //
        // GET: /Order/

        // 显示完成订单的窗体页面
        public ActionResult Complete()
        {
            return View();
        }

        // 将订单信息与购物车信息写入数据库
        [HttpPost]
        public ActionResult Complete(OrderHeader form)
        {
            var member = db.Members.Where(p => p.Email == User.Identity.Name).FirstOrDefault();
            if (member == null) return RedirectToAction("Index", "Home");

            if (this.Carts.Count == 0) return RedirectToAction("Index", "Cart");

            // TODO: 将订单信息与购物车信息写入数据库
            OrderHeader oh = new OrderHeader()
            {
                Member = member,
                ContactName = form.ContactName,
                ContactAddress = form.ContactAddress,
                ContactPhoneNo = form.ContactPhoneNo,
                BuyOn = DateTime.Now,
                Memo = form.Memo,
                OrderDetailItems = new List<OrderDetail>()
            };

            int total_price = 0;
            foreach (var item in this.Carts)
            {
                var product = db.Products.Find(item.Product.Id);
                if (product == null) return RedirectToAction("Index", "Cart");

                total_price += item.Product.Price * item.Amount;
                oh.OrderDetailItems.Add(new OrderDetail() { Product = product, Price = product.Price, Amount = item.Amount });
            }
            oh.TotalPrice = total_price;

            db.Orders.Add(oh);
            db.SaveChanges();
            // TODO: 订单完成后必须清空现有购物车信息
            this.Carts.Clear();
            // 订单完成后回到网站首页
            return RedirectToAction("Index", "Home");
        }
    }
}
