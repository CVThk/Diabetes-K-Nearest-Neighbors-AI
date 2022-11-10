using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebDiabetes.Models;

namespace WebDiabetes
{
    public class ConnectionDiabetesDatabase
    {
        private static ConnectionDiabetesDatabase instance;

        public static ConnectionDiabetesDatabase Instance
        {
            get { if (instance == null) instance = new ConnectionDiabetesDatabase(); return instance; }
            private set { instance = value; }
        }
        private ConnectionDiabetesDatabase() { }

        string connectionString = @"Data Source = sql.bsite.net\MSSQL2016; Initial Catalog = cvt2405_Diabetes; User ID = cvt2405_Diabetes; Password=cvt123456";
        public List<ClassDiabetes> GetDiabetes()
        {
            List<ClassDiabetes> diabetesList = new List<ClassDiabetes>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_diabetes", connection);

                SqlDataReader reader = cmd.ExecuteReader();
                int lenField = reader.FieldCount;
                while (reader.Read())
                {
                    ClassDiabetes k = new ClassDiabetes();
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