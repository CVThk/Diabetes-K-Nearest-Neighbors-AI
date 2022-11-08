using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDiabetes.Models
{
    public class KNN
    {
        private List<KNN_item> DataList { get; set; }
        private List<double> Label { get; set; }

        private double Min_In_Col(List<KNN_item> dataList, int col)
        {
            double min = dataList[0].Attributes[col];
            foreach (var item in dataList)
            {
                if (item.Attributes[col] < min)
                    min = item.Attributes[col];
            }
            return min;
        }
        private double Max_In_Col(List<KNN_item> dataList, int col)
        {
            double max = dataList[0].Attributes[col];
            foreach (var item in dataList)
            {
                if (item.Attributes[col] > max)
                    max = item.Attributes[col];

            }
            return max;
        }
        // Chuẩn hóa dữ liệu
        public void Scale_Fit_Transform()
        {
            int row = DataList.Count;
            int col = DataList.First().Attributes.Count;
            for (int j = 0; j < col; j++)
            {
                double min = Min_In_Col(DataList, j);
                double max = Max_In_Col(DataList, j);
                foreach (var item in DataList)
                {
                    item.Attributes[j] = (double)(item.Attributes[j] - min) / (max - min);
                }
                //data.Attributes[j] = (double)(data.Attributes[j] - min) / (max - min);
            }
        }

        public void Fit(List<KNN_item> dataList)
        {
            DataList = dataList;
            Label = new List<double>();
            foreach (var item in DataList)
            {
                if (!Label.Contains(item.Val))
                    Label.Add(item.Val);
            }
        }

        private double distance(KNN_item a, KNN_item b, int col_start, int col_end)
        {
            double d = 0;
            for (int j = col_start; j <= col_end; j++)
                d += Math.Pow(a.Attributes[j] - b.Attributes[j], 2);
            return Math.Sqrt(d);
        }

        public double Predict(KNN_item data, int col_start, int col_end, int k)
        {
            foreach (var item in DataList)
            {
                item.Distance = distance(item, data, col_start, col_end);
            }
            DataList.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            
            int[] count = new int[Label.Count];
            for (int i = 0; i < k; i++)
            {
                int index_Count = 0;
                foreach (var item in Label)
                {
                    if (DataList[i].Val == item)
                    {
                        count[index_Count]++;
                    }
                    index_Count++;
                }
            }
            int max = count[0];
            double label_max = Label[0];
            for (int i = 1; i < Label.Count; i++)
            {
                if (max < count[i])
                {
                    max = count[i];
                    label_max = Label[i];
                }
            }
            return label_max;
        }

        public KNN_item Average()
        {
            int row = DataList.Count;
            int col = DataList.First().Attributes.Count;
            KNN_item item = new KNN_item();
            for (int j = 0; j < col; j++)
            {
                double avg = 0;
                for (int i = 0; i < row; i++)
                {
                    if(DataList[i].Val == 0)
                        avg += DataList[i].Attributes[j];
                }
                avg /= (double)row;
                item.Attributes.Add(avg);
            }
            return item;
        }
        public KNN_item Min()
        {
            KNN_item test = DataList.Find(x => x.Val == 1);
            if (test == null) return null;
            KNN_item min = new KNN_item();
            min.Attributes = test.Attributes;
            int n = min.Attributes.Count;
            for(int j = 0; j < n; j++)
            {
                foreach (var item in DataList)
                {
                    if (item.Attributes[j] < min.Attributes[j] && item.Val == 0)
                        min.Attributes[j] = item.Attributes[j];
                }
            }    
            return min;
        }
        public KNN_item Max()
        {
            KNN_item test = DataList.Find(x => x.Val == 0);
            if (test == null) return null;
            KNN_item max = new KNN_item();
            max.Attributes = test.Attributes;
            int n = max.Attributes.Count;
            for (int j = 0; j < n; j++)
            {
                foreach (var item in DataList)
                {
                    if (item.Attributes[j] > max.Attributes[j] && item.Val == 0)
                        max.Attributes[j] = item.Attributes[j];
                }
            }
            return max;
        }
    }
}