using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebDiabetes.Models;

namespace WebDiabetes
{
    public class DiabetesData
    {
        private static DiabetesData instance;

        public static DiabetesData Instance
        {
            get { if (instance == null) instance = new DiabetesData(); return instance; }
            private set { instance = value; }
        }
        private DiabetesData() { }

        string connectionString = @"Data Source = sql.bsite.net\MSSQL2016; Initial Catalog = cvt2405_Diabetes; User ID = cvt2405_Diabetes; Password=cvt123456";
        public List<KNN_item> GetDiabetes()
        {
            List<KNN_item> diabetesList = new List<KNN_item>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_diabetes", connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int lenField = reader.FieldCount;
                while (reader.Read())
                {
                    KNN_item k = new KNN_item();
                    int i = 0;
                    for (; i < lenField - 1; i++)
                        k.Attributes.Add(double.Parse(reader.GetValue(i).ToString()));
                    k.Val = int.Parse(reader.GetValue(i).ToString());
                    diabetesList.Add(k);
                }
            }
            return diabetesList;
        }
    }
}