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
    public partial class AddNewField : Form
    {
        public AddNewField()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int number;
            if (Int32.TryParse(textBox1.Text, out number))
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    try
                    {
                        sqlConnection1.Open();
                        SqlCommand command1 = new SqlCommand("SELECT CropId FROM Crop WHERE CropName = @name", sqlConnection1);
                        command1.Parameters.AddWithValue("@name", comboBox1.SelectedItem.ToString());
                        SqlDataReader reader1 = command1.ExecuteReader();
                        reader1.Read();
                        var CropId = reader1[0]; //отримаємо код обраної культури
                        reader1.Dispose();


                        SqlCommand command = new SqlCommand("INSERT INTO Field(FieldNumber,CropId) Values(@number,@cropid)", sqlConnection1);
                        command.Parameters.AddWithValue("@number", number);
                        command.Parameters.AddWithValue("@cropid", CropId);
                        command.ExecuteNonQuery();  //додаємо у таблицю
                        sqlConnection1.Close();
                        MessageBox.Show("Запис успішно доданий.");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ви не можете додавати поля з вже існуючими номерами"+ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sqlConnection1.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть культуру у випадаючому списку");
                }
            }
            else
            {
                MessageBox.Show("Номер поля має бути цілим числом(номера полів не можуть повторюватись)");
            }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void AddNewField_Load_1(object sender, EventArgs e)
        {
            try //заповнюємо comboBox1
            {
               // String number;
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT CropName FROM Crop", sqlConnection1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));

                    //comboBox1.DisplayMember = reader.GetString(1);;
                    //comboBox1.ValueMember = reader.GetInt32(0).ToString();
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
