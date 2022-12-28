using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFoodApp.Models;


namespace WebFoodApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        dbFoodAppDataContext data = new dbFoodAppDataContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public List<Cart> Laygiohang()
        {
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Cart>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iMaMA, String strURL)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.Find(n => n.IMaMA == iMaMA);
            if (sanpham == null)
            {
                sanpham = new Cart(iMaMA);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.ISoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.ISoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Cart> lstGiohang = Session["Giohang"] as List<Cart>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.DThanhtien);
            }
            return iTongTien;
        }
        public ActionResult Cart()
        {
            List<Cart> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }

        public ActionResult CartPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaMonan(int iMaSp)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.IMaMA == iMaSp);
            if (sanpham!=null)
            {
                lstGiohang.RemoveAll(n => n.IMaMA == iMaSp);
                return RedirectToAction("Cart");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
    }
}