using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFoodApp.Models;

namespace WebFoodApp.Models
{
    public class Cart
    {
        dbFoodAppDataContext data = new dbFoodAppDataContext();
        public int IMaMA { set; get; }
        public string STenMA { set; get; }
        public string SAnhMA { set; get; }
        public double DDongia { set; get; }
        public int ISoluong { set; get; }
        public double DThanhtien
        {
            get { return ISoluong * DDongia; }
        }
        public Cart(int MaMA)
        {
            IMaMA = MaMA;
            MonAn monAn = data.MonAns.Single(n => n.MaMA == IMaMA);
            STenMA = monAn.TenMA;
            SAnhMA = monAn.AnhMA;
            DDongia = double.Parse(monAn.GiaBan.ToString());
            ISoluong = 1;
        }
    }
}