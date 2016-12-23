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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        private void updateGridViewAndCombobox()
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT WorkerId AS [Код робітника], LastName AS [Прізвище], FirstName AS [Ім'я], QualificationName AS [Кваліфікація], Salary AS [Ставка] FROM Workers LEFT JOIN Qualification ON Workers.QualificationId = Qualification.QualificationId ORDER BY WorkerId", sqlConnection1);
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
                SqlCommand command = new SqlCommand("SELECT WorkerId FROM Workers", sqlConnection1);
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewEmpl newEmp = new AddNewEmpl();
            newEmp.ShowDialog();
            updateGridViewAndCombobox();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            updateGridViewAndCombobox();
            try //заповнюємо comboBox1
            {
                comboBox1.Items.Clear();
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT QualificationName FROM Qualification", sqlConnection1);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try 
                {
                    sqlConnection1.Open();//спочатку знімаємо робітника з тех операції, а потім "звільняємо"
                    SqlCommand command = new SqlCommand("DELETE FROM EmployeesList WHERE WorkerId = @deletNum DELETE FROM Workers WHERE WorkerId = @deletNum", sqlConnection1);
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
            else MessageBox.Show("Оберіть код робітника у випадаючому списку.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                try
                {
                    sqlConnection1.Open();//спочатку знімаємо робітника з тех операції, а потім "звільняємо"
                    SqlCommand command = new SqlCommand("UPDATE Qualification SET Salary=@salary WHERE QualificationName = @qName", sqlConnection1);
                    command.Parameters.AddWithValue("@salary", textBox1.Text);
                    command.Parameters.AddWithValue("@qName", comboBox2.SelectedItem.ToString());
                    command.ExecuteNonQuery();
                    sqlConnection1.Close();
                    updateGridViewAndCombobox();//заповнюємо comboBox1
                    MessageBox.Show("Ставка оновлена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sqlConnection1.Close();
                }
            }
            else MessageBox.Show("Оберіть кваліфікацію у випадаючому списку.");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле має містити тільки цифри!");
            }   
        }

    }
}
