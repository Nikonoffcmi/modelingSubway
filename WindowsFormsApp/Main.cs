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

namespace WindowsFormsApp
{
    public partial class Main : Form
    {
        City city;
        public Main()
        {
            InitializeComponent();
            DefaultSettings();
            city = new City();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            State.simulationTime = Convert.ToInt32(numericUpDown1.Value);
            State.simulationInterval = Convert.ToInt32(numericUpDown2.Value);
            State.averageTransmittanceTrains = Convert.ToInt32(numericUpDown3.Value);
            State.TrainsCapacity = Convert.ToInt32(numericUpDown4.Value);
            try
            {

                city.Simulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                DialogResult = DialogResult.None;
            }
            label4.Text = State.averageEnterWaiting.ToString();
            label5.Text = State.ratioPassengers.ToString();
        }

        private void DefaultSettings()
        {
            numericUpDown1.Value = State.simulationTime;
            numericUpDown2.Value = State.simulationInterval;
            numericUpDown3.Value = State.averageTransmittanceTrains;
            numericUpDown4.Value = State.TrainsCapacity;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sf = new SubwayForm();
            if (sf.ShowDialog() == DialogResult.OK)
                city.subways.Add(sf.subway);
            LoadData();
        }

        public void LoadData()
        {
            dataGridView2.Rows.Clear();
            foreach (var subway in city.subways)
            {
                dataGridView2.Rows.Add(subway.Name, subway.freeSpace, subway.averageTransmittancePassengers);
            }
        }
    }
}
