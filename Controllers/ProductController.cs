using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFoodApp.Models;

namespace WebFoodApp.Controllers
{
    public class ProductController : Controller
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
        public ActionResult Category()
        {
            var cgr = from cd in data.ChuDes select cd;
            return PartialView(cgr);
        }
        public ActionResult CuaHang()
        {
            var cuahang = from ch in data.CuaHangs select ch;
            return PartialView(cuahang);
        }
        public ActionResult SPTheocate(int id) {
            var ma = from m in data.MonAns where m.MaCD == id select m;
            return View(ma);
        }
        public ActionResult SPTheocuahang(int id)
        {
            var ma = from m in data.MonAns where m.MaCH == id select m;
            return View(ma);
        }
    }
}