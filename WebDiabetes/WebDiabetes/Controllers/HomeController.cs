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
        static List<KNN_item> _dataDiabetes = DiabetesData.Instance.GetDiabetes();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dataset()
        {
            string str = "";
            foreach (var k in _dataDiabetes)
            {
                str += @"<tr>";
                foreach (var item in k.Attributes)
                {
                    str += @"<td>" + item.ToString() + @"</td>";
                }
                str += @"<td>" + k.Val.ToString() + @"</td></tr>";
            }
            ViewBag.Diabetes = str;
            ViewBag.CountPos = "[" + _dataDiabetes.Count(x => x.Val == 0).ToString() + "]";
            ViewBag.CountNe = "[" + _dataDiabetes.Count(x => x.Val == 1).ToString() + "]";
            double Precision, Recall;
            KNN _knn = new KNN();
            _knn.Fit(_dataDiabetes);
            //_knn.K();
            _knn.Accuracy(out Precision, out Recall, 11);

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Result(FormCollection fc)
        {
            double Pregnancies, Glucose, BloodPressure, Age, SkinThickness, DiabetesPedigreeFunction, Insulin, Weight, Height;
            if (!Utilities.Instance.ConvertDouble(fc["Pregnancies"].ToString(), out Pregnancies) || !Utilities.Instance.ConvertDouble(fc["Glucose"].ToString(), out Glucose)
                || !Utilities.Instance.ConvertDouble(fc["BloodPressure"].ToString(), out BloodPressure) || !Utilities.Instance.ConvertDouble(fc["Age"].ToString(), out Age)
                || !Utilities.Instance.ConvertDouble(fc["SkinThickness"].ToString(), out SkinThickness) || !Utilities.Instance.ConvertDouble(fc["DiabetesPedigreeFunction"].ToString(), out DiabetesPedigreeFunction)
                || !Utilities.Instance.ConvertDouble(fc["Insulin"].ToString(), out Insulin) || !Utilities.Instance.ConvertDouble(fc["Weight"].ToString(), out Weight)
                || !Utilities.Instance.ConvertDouble(fc["Height"].ToString(), out Height))
            {
                Session["Err"] = "true";
                return RedirectToAction("Test");
            }    


            KNN _knn = new KNN();
            _knn.Fit(_dataDiabetes);

            string avgResult, maxResult;
            _knn.GraphString(out avgResult, out maxResult);

            ViewBag.avgResult = avgResult;
            ViewBag.maxResult = maxResult;

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