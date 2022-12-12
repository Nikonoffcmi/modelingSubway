﻿using SubwayModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            label4.Text = Statistics.averageWaiting.ToString();
            label5.Text = Statistics.ratioPassengers.ToString();
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
                dataGridView2.Rows.Add(subway.Name, subway.FreeSpace, subway.AverageTransmittancePassengers);
            }
        }
    }
}
