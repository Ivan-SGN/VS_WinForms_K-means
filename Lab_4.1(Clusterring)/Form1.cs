using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Clustering;

namespace Lab_4._1_Clusterring_
{
    public partial class Form1 : Form
    {
        private string _fileName;
        private AppLogic Logic = new AppLogic();

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Users\\Ivan\\Desktop\\Lab_4.1(Clusterring)";
            ofd.Filter = "Comma separeted files (*.csv)|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo info = new FileInfo(ofd.FileName);
                if (info.Exists && info.Length > 0)
                {
                    FilePathTxt.Text = ofd.FileName;
                    _fileName = ofd.FileName;
                }
                else
                    MessageBox.Show("Wrong File", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (FilePathTxt.Text != "")
            {
                try
                {
                    Logic.CalculateData(_fileName);
                    FillChartCluster(chartCluster);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Open File", "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void FillChartCluster(Chart chartCluster)
        {
            chartCluster.Series.Clear();
            chartCluster.Titles.Clear();
            chartCluster.Series.Add("Clusterring");
            chartCluster.Series[0].ChartType = SeriesChartType.Point;
            for (int i = 0; i < Logic.dataPoints.Count; i++)
            {
                Logic.dataPoints[i].Color = Color.Black;
                Logic.dataPoints[i].MarkerSize = 1;
                chartCluster.Series[0].Points.Add(Logic.dataPoints[i]);
            }
            for (int i = 0; i < Logic.clusterPoints.Count; i++)
            {
                Logic.clusterPoints[i].MarkerSize = 7;
                Logic.clusterPoints[i].Color = Color.Red;
                chartCluster.Series[0].Points.Add(Logic.clusterPoints[i]);
            }
        }
    }
}
