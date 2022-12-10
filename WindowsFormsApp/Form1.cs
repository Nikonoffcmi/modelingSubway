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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var subway = new Subway();
            subway.Simulation();
            label4.Text = State.averageEnterWaiting.ToString();
            label5.Text = State.ratioGuests.ToString();
        }
    }
}
