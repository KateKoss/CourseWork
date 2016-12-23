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
    public partial class TechMap : Form
    {
        int cropId;
        String mapName;
        public TechMap(int id)
        {
            InitializeComponent();
            cropId = id;
        }

        private void TechMap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT MapId FROM TechMap WHERE MapName = @Mname AND CropId = @CropId", sqlConnection1);
                command.Parameters.AddWithValue("@Mname", mapName);
                command.Parameters.AddWithValue("@CropId", cropId);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();                    
                TechOperation newOperat = new TechOperation(reader.GetInt32(0));
                sqlConnection1.Close();
                newOperat.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO TechMap(MapName,CropId) Values(@Mname,@CropId)", sqlConnection1);
                    mapName = textBox1.Text;
                    command.Parameters.AddWithValue("@Mname", mapName);
                    command.Parameters.AddWithValue("@CropId", cropId);
                    command.ExecuteNonQuery();  //додаємо у таблицю
                    sqlConnection1.Close();
                    button1.Visible = true;
                    button3.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Назви карт не можуть повторюватись! " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sqlConnection1.Close();
                }
            }
            else
            {
                MessageBox.Show("Заповніть поле 'Назва тех карти' та натисніть додати карту.");
            }
        }
    }
}
