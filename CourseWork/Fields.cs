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
    public partial class Fields : Form
    {
        public Fields()
        {
            InitializeComponent();
        }
        
        private void updateGridViewAndCombobox()
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT FieldNumber AS [Номер поля], CropName AS [Назва культури] FROM Crop RIGHT JOIN Field ON Crop.CropId = Field.CropId ORDER BY FieldNumber", sqlConnection1);
                SqlDataAdapter dAdapter = new SqlDataAdapter(command);
                DataTable dTable = new DataTable();
                dAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
            try //заповнюємо comboBox1
            {
                comboBox1.Items.Clear();
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT FieldNumber FROM Field", sqlConnection1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetInt32(0));
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }
         

        private void Form1_Load(object sender, EventArgs e)
        {
            updateGridViewAndCombobox();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddNewField newField = new AddNewField();
            newField.ShowDialog();

            updateGridViewAndCombobox();
            
        }

        
        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try 
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Field WHERE FieldNumber = @deletNum", sqlConnection1);
                    command.Parameters.AddWithValue("@deletNum", comboBox1.SelectedItem.ToString());
                    command.ExecuteNonQuery();
                    sqlConnection1.Close();
                    updateGridViewAndCombobox();//заповнюємо comboBox1
                    MessageBox.Show("Запис видалено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sqlConnection1.Close();
                }
            }
            else MessageBox.Show("Оберіть номер поля у випадаючому списку.");
        }
    }
}
