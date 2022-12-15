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
            labelAveragePassengersWaitingTrains.ForeColor = Color.White;
            generalStatisticsLabel.ForeColor = Color.White;
            labelAverageTrainWaitingTime.ForeColor = Color.White;
            label8.ForeColor = Color.White;
            labelAverageWaitingTime.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SetSettings();
                var city = new City(Settings.Subways);
                for (int i = 0; i < 1000; i++)
                    city.Simulation();


                UpdateStattistics();
                SetChart(Statistics.averageSubwayWaitingTime, chart1);
                SetChart(Statistics.passengersWaitingTrains, chart2);
                ShowLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
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

        private void LoadData()
        {
            dataGridView.Rows.Clear();
            foreach (var subway in Settings.Subways)
            {
                dataGridView.Rows.Add(subway.Name, subway.AverageTransmittancePassengers);
            }
        }

        private void SetChart(List<int> list, Chart chart)
        {
            chart.Series[0].Points.Clear();
            int N = 10;
            double[] x = new double[N];
            double[] y = new double[N];
            double[] P = new double[N + 1];
            int j = 0;
            j = 1;
            for (int i = 0; i <= N; i++)
            {
                P[i] = (double)list.Max() / N * i;
            }
            for (int k = 0; k < list.Count; k++) 
            {
                j = 1;
                while (list[k] > P[j])    
                {
                    j = j + 1;
                    if (j == N)
                        break;
                }

                if (list[k] <= P[j])
                {
                    y[j-1]++;
                }
            }

            for (int i = 0; i < N; i++)
                chart.Series[0].Points.AddXY(P[i], y[i]);
        }

        private void SetSettings()
        {
            Settings.simulationTime = Convert.ToInt32(numericUpDown1.Value);
            Settings.averageTransmittanceTrains = Convert.ToInt32(numericUpDown3.Value);
            Settings.TrainsCapacity = Convert.ToInt32(numericUpDown4.Value);
            Statistics.averageSubwayWaitingTime.Clear();
            Statistics.passengersWaitingTrains.Clear();
        }

        private void UpdateStattistics()
        {
            Statistics.averageSubwayWaitingTime.RemoveAt(Settings.Subways.Count - 1);
            if (Statistics.averageSubwayWaitingTime.Count > 0)
                Statistics.averageWaitingTime = (int)Math.Round(Statistics.averageSubwayWaitingTime.Average());
            else
                Statistics.averageWaitingTime = 0;

            if (Statistics.passengersWaitingTrains.Count > 0)
                Statistics.averagePassengersWaitingTrains = (int)Math.Round(Statistics.passengersWaitingTrains.Average());
            else
                Statistics.averagePassengersWaitingTrains = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].RowIndex;
            var sf = new SubwayForm(Settings.Subways[id]);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Settings.Subways.RemoveAt(id);
                Settings.Subways.Insert(id, sf.subway);
            }
            LoadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].RowIndex;
            Settings.Subways.RemoveAt(id);
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void ShowLabel()
        {
            labelAveragePassengersWaitingTrains.ForeColor = Color.Black;
            generalStatisticsLabel.ForeColor = Color.Black;
            labelAverageTrainWaitingTime.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
            labelAverageWaitingTime.ForeColor = Color.Black;
            labelAverageWaitingTime.Text = Statistics.averageWaitingTime.ToString();
            labelAveragePassengersWaitingTrains.Text = Statistics.averagePassengersWaitingTrains.ToString();
        }
    }
}
