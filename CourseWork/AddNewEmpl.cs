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
    public partial class AddNewEmpl : Form
    {
        public AddNewEmpl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try
                {
                    sqlConnection1.Open();
                    SqlCommand command1 = new SqlCommand("SELECT QualificationId FROM Qualification WHERE QualificationName = @qalif", sqlConnection1);
                    command1.Parameters.AddWithValue("@qalif", comboBox1.SelectedItem.ToString());
                    SqlDataReader reader1 = command1.ExecuteReader();
                    reader1.Read();
                    var qalifId = reader1[0]; //отримаємо код кваліф
                    reader1.Dispose();


                    SqlCommand command = new SqlCommand("INSERT INTO Workers(LastName,FirstName,QualificationId) Values(@Lname,@Fname,@qalifId)", sqlConnection1);
                    command.Parameters.AddWithValue("@Lname", textBox1.Text);
                    command.Parameters.AddWithValue("@Fname", textBox2.Text);
                    command.Parameters.AddWithValue("@qalifId", qalifId);
                    command.ExecuteNonQuery();  //додаємо у таблицю
                    sqlConnection1.Close();
                    MessageBox.Show("Запис успішно доданий.");
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
                MessageBox.Show("Оберіть кваліфікацію у випадаючому списку");
            }
         }
        

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле має містити тільки букви");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) ||char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле має містити тільки букви");
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewEmpl_Load(object sender, EventArgs e)
        {
            try //заповнюємо comboBox1 кваліфікаціями
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT QualificationName FROM Qualification", sqlConnection1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }
    }
}
