using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDiabetes.Models;

namespace WebDiabetes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dataset()
        {
            List<KNN_item> list = DiabetesData.Instance.GetDiabetes();
            string str = "";
            foreach (var k in list)
            {
                str += @"<tr>";
                foreach (var item in k.Attributes)
                {
                    str += @"<td>" + item.ToString() + @"</td>";
                }
                str += @"<td>" + k.Val.ToString() + @"</td></tr>";
            }
            ViewBag.Diabetes = str;
            return View();
        }

        public ActionResult Test()
        {
            KNN _knn = new KNN();
            List<KNN_item> list = DiabetesData.Instance.GetDiabetes();
            _knn.Fit(list);
            KNN_item _Item_AVG = _knn.Average();
            KNN_item _Item_Max = _knn.Max();
            KNN_item _Item_Min = _knn.Min();

            string avgResult = @"[";
            string maxResult = @"[";
            string minResult = @"[";

            int n = _Item_AVG.Attributes.Count;
            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                {
                    avgResult += ',';
                    maxResult += ',';
                    minResult += ',';
                }
                avgResult += _Item_AVG.Attributes[i].ToString().Replace(',', '.');
                maxResult += _Item_Max.Attributes[i].ToString().Replace(',', '.');
                minResult += _Item_Min.Attributes[i].ToString().Replace(',', '.');
            }
            avgResult += "]";
            maxResult += "]";
            minResult += "]";

            ViewBag.avgResult = avgResult;
            ViewBag.maxResult = maxResult;
            ViewBag.minResult = minResult;

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}