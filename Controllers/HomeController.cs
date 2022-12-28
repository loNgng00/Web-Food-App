using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFoodApp.Models;

namespace WebFoodApp.Controllers
{
    public class HomeController : Controller
    {
        dbFoodAppDataContext data = new dbFoodAppDataContext();
        private List<MonAn> Laymonmoi(int count)
        {
            return data.MonAns.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var monmoi = Laymonmoi(10);
            return View(monmoi);
        }
        public ActionResult Details(int id)
        {
            var monan = from s in data.MonAns
                        where s.MaMA == id
                        select s;
            return View(monan.Single());
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
        public ActionResult CartPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
    }
}