using SubwayModel.Model;
using System;
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
            if (subway == null)
                throw new ArgumentNullException(nameof(subway));

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
