using SubwayModel.Model;
using SubwayModel.Model.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            labelAveragePassengersWaitingTrains.ForeColor = Color.WhiteSmoke;
            generalStatisticsLabel.ForeColor = Color.WhiteSmoke;
            labelAverageTrainWaitingTime.ForeColor = Color.WhiteSmoke;
            label8.ForeColor = Color.WhiteSmoke;
            labelAverageWaitingTime.ForeColor = Color.WhiteSmoke;
            label5.ForeColor = Color.WhiteSmoke;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SetSettings();

                var sybwayModeling = new SybwayModeling(Settings.Subways, Settings.passengerFactory);
                sybwayModeling.Simulation();

                ShowStatistics();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Argument Null Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Argument Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
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
            numericUpDown4.Value = Settings.vanCapacity;
            numericUpDown2.Value = Settings.numberRuns;
            numericUpDown5.Value = Settings.numberVan;
            comboBox2.Items.Add("10%");
            comboBox2.Items.Add("33%");
            comboBox2.Items.Add("50%");
            comboBox2.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var sf = new SubwayForm();
                if (sf.ShowDialog() == DialogResult.OK)
                    Settings.Subways.Add(sf.subway);
                LoadData();
            }
            catch(ArgumentNullException ex)
            {
                MessageBox.Show($"Argument Null Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
        }

        private void LoadData()
        {
            dataGridView.Rows.Clear();
            foreach (var subway in Settings.Subways)
            {
                dataGridView.Rows.Add(subway.Name, subway.AverageTransmittancePassengers);
            }
            UpdatecomboBox();
        }

        private void SetChart(List<int> list, Chart chart)
        {
            try
            {
                if (list == null)
                    throw new ArgumentNullException("list пуст");
                if (list.Count == 0)
                    throw new ArgumentException("list пуст");

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
                        y[j - 1]++;
                    }
                }

                for (int i = 0; i < N; i++)
                    chart.Series[0].Points.AddXY(P[i], y[i]);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Argument Null Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Argument Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
        }

        private void SetSettings()
        {
            Settings.simulationTime = Convert.ToInt32(numericUpDown1.Value);
            Settings.averageTransmittanceTrains = Convert.ToInt32(numericUpDown3.Value);
            Settings.vanCapacity = Convert.ToInt32(numericUpDown4.Value);
            Settings.numberRuns = Convert.ToInt32(numericUpDown2.Value);
            Settings.numberVan = Convert.ToInt32(numericUpDown5.Value);

            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    Settings.passengerFactory = new TakeSpacePassengerFactoryLow();
                    break;

                case 1:
                    Settings.passengerFactory = new TakeSpacePassengerFactoryMiddle();
                    break;

                case 2:
                    Settings.passengerFactory = new TakeSpacePassengerFactoryHigh();
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: Нет доступных станций\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var id = dataGridView.SelectedRows[0].Cells[0].RowIndex;
                Settings.Subways.RemoveAt(id);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: Нет доступных станций\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void ShowStatistics()
        {
            labelAveragePassengersWaitingTrains.ForeColor = Color.Black;
            generalStatisticsLabel.ForeColor = Color.Black;
            labelAverageTrainWaitingTime.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
            labelAverageWaitingTime.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            labelAverageWaitingTime.Text = Statistics.averageWaitingTime.ToString();
            labelAveragePassengersWaitingTrains.Text = Statistics.averagePassengersWaitingTrains.ToString();

            SetChart(Statistics.averageSubwayWaitingTime[comboBox1.SelectedItem.ToString()], chart1);
            SetChart(Statistics.passengersWaitingTrains[comboBox1.SelectedItem.ToString()], chart2);
        }

        private void UpdatecomboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Общая");
            foreach (var s in Settings.Subways)
                comboBox1.Items.Add(s.Name);
            comboBox1.SelectedItem = "Общая";
            comboBox1.Items.RemoveAt(comboBox1.Items.Count - 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Statistics.averageSubwayWaitingTime.Keys.Count != 0)
            {
                SetChart(Statistics.averageSubwayWaitingTime[comboBox1.SelectedItem.ToString()], chart1);
                SetChart(Statistics.passengersWaitingTrains[comboBox1.SelectedItem.ToString()], chart2);
            }
        }
    }
}
