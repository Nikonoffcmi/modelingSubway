using SubwayModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DefaultSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //chart1.Series[0].Points.Clear();
            //chart2.Series[0].Points.Clear();
            Settings.simulationTime = Convert.ToInt32(numericUpDown1.Value);
            Settings.averageTransmittanceTrains = Convert.ToInt32(numericUpDown3.Value);
            Settings.TrainsCapacity = Convert.ToInt32(numericUpDown4.Value);
            try
            {
                var city = new City(Settings.Subways);
                city.Simulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            label4.Text = Statistics.averageWaitingTime.ToString();
            label5.Text = Statistics.averagePassengersWaitingTrains.ToString();
            //for (int i = 0; i < Settings.Subways.Count; i++)
            //{
            //    chart1.Series[0].Points.AddXY(i, Statistics.ratioSubwayPassengers[i]);
            //    chart2.Series[0].Points.AddXY(i, Statistics.averageSubwayWaiting[i]);
            //}
        }

        private void DefaultSettings()
        {
            numericUpDown1.Value = Settings.simulationTime;
            numericUpDown3.Value = Settings.averageTransmittanceTrains;
            numericUpDown4.Value = Settings.TrainsCapacity;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sf = new SubwayForm();
            if (sf.ShowDialog() == DialogResult.OK)
                Settings.Subways.Add(sf.subway);
            LoadData();
        }

        public void LoadData()
        {
            dataGridView2.Rows.Clear();
            foreach (var subway in Settings.Subways)
            {
                dataGridView2.Rows.Add(subway.Name, subway.AverageTransmittancePassengers);
            }
        }
        //public void gra(List<int> list)
        //{
        //    int N = 10; 
        //    double[] x = new double[N];
        //    double[] y = new double[N];
        //    double ysumm = 0;
        //    double[] P = new double[N + 1];
        //    int j = 0;
        //    for (double i = 1; i < 2; i += 0.2)
        //    {
        //        x[j] = i + 0.1;
        //        ysumm += w * Math.Exp(-w * x[j]);
        //        j++;
        //    }
        //    j = 1;
        //    for (double i = 1; i < 2; i += 0.2)
        //    {
        //        x[j - 1] = i - 0.1;
        //        P[j] = (w * Math.Exp(-w * x[j - 1])) / ysumm;
        //        j++;
        //    }
        //    for (int k = 0; k <= 1000; k++) //кол-во элементов в выборке
        //    {
        //        j = 1;
        //        double r1 = b.NextDouble(); //вероятность выпадения которая сравнивается с вероятностью попадения в интревал
        //        while (r1 > P[j])          //выбирает интервал и сравнивает
        //        {
        //            r1 = r1 - P[j];
        //            j = j + 1;
        //            if (j == 10)
        //            {
        //                r1 = r1 - P[j];
        //                break;
        //            }
        //        }
        //        double r2 = b.NextDouble();

        //        if (r1 < P[j])    //выбирает интервал и генерирует значение на интервале
        //        {

        //            int count = 0;
        //            x[j] = x[j - 1] + r2 * (x[j] - x[j - 1]); //элемент на интервале
        //            chart1.Series[0].Points.AddXY(x[j], 10);
        //            //y[j] = w * Math.Exp(-w * x[j]);
        //        }
        //        //chart1.Series[0].Points.AddXY(x[j],y[j]);
        //    }
        //}
    }
}
