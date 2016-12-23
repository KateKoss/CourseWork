using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fields newF = new Fields();
            newF.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employees newEmp = new Employees();
            newEmp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Statistic stat = new Statistic();
            stat.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TechAdd newTech = new TechAdd();
            newTech.ShowDialog();
        }
    }
}
