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
    public partial class SubwayForm : Form
    {
        public Subway subway { get; set; }

        public SubwayForm()
        {
            InitializeComponent();
        }

        public SubwayForm(Subway subway)
        {
            InitializeComponent();
            textBox1.Text = subway.Name;
            numericUpDown2.Value = subway.AverageTransmittancePassengers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            var averageTransmittancePassengers = Convert.ToInt32(numericUpDown2.Value);
            subway = new Subway(name, averageTransmittancePassengers);
            Close();
        }
    }
}
