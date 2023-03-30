using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using LinearAalgebra;
using Clustering;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Lab_4._1_Clusterring_
{
    internal class AppLogic
    {
        public int MaxSizeOfData { get => 100000; set => value = 100000; }

        public List<DataPoint> dataPoints = new List<DataPoint>();
        public List<DataPoint> clusterPoints = new List<DataPoint>();
        public List<IMathVector> vectors = new List<IMathVector>();
        private int _clustersCnt = 15;
                
        public void CalculateData(string fileName)
        {
            string[] rowData = File.ReadAllLines(fileName);
            for (int i = 0; i < rowData.Length; i++)
            {
                IMathVector vec = new MathVector(2);
                string preparedStr = rowData[i].Trim().Replace("    ", ",");
                string[] subs = preparedStr.Split(',');
                for (int j = 0; j < 2; j++)
                {
                    vec[j] = Convert.ToDouble(subs[j]);
                }
                vectors.Add(vec);
                dataPoints.Add(new DataPoint(vec[0], vec[1])); 
            }
            dataPoints.Add(new DataPoint(1, 1));

            IClusterer clustering = new KmeansClusterer(_clustersCnt);
            clustering.ClaculateData(vectors);
            IEnumerable<Cluster> clusters = new List<Cluster>(_clustersCnt);
            clusters = clustering.Clusters;
            foreach (var cluster in clusters)
            {
                clusterPoints.Add(new DataPoint(cluster.GetCenter[0], cluster.GetCenter[1]));
            }
        }
    }
}
