using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CourseWork
{
    public partial class TechOperation : Form
    {
        int mapId;
        public TechOperation(int mapId)
        {
            InitializeComponent();
            this.mapId = mapId;
        }

        private void TechOperation_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != "" && textBox1.Text != "")
            {
                try
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO TechOperation(MapId,OperationName,ExecutionMonth,FuilCosts,ProcessingTime) "+
                        "Values(@MapId,@OperationName,@ExecutionMonth,@FuilCosts,@ProcessingTime)", sqlConnection1);
                    command.Parameters.AddWithValue("@MapId", mapId);
                    command.Parameters.AddWithValue("@OperationName", textBox1.Text);
                    command.Parameters.AddWithValue("@ExecutionMonth", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FuilCosts", textBox2.Text);
                    command.Parameters.AddWithValue("@ProcessingTime", textBox3.Text);
                    command.ExecuteNonQuery();  //додаємо у таблицю

                    sqlConnection1.Close();
                    MessageBox.Show("Запис додано.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sqlConnection1.Close();
                }
            }
            else
            {
                MessageBox.Show("Заповніть всі поля.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
