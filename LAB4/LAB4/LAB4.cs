using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB4
{
    public partial class LAB4 : Form
    {
        public LAB4()
        {
            InitializeComponent();
        }

        private void btnTask1_Click(object sender, EventArgs e)
        {
            Task1From task1From = new Task1From();
            task1From.ShowDialog();
        }

        private void btnTask3_Click(object sender, EventArgs e)
        {
            Task3Form task3Form = new Task3Form();
            task3Form.ShowDialog();
        }
    }
}
