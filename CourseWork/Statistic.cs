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
    public partial class Statistic : Form
    {
        int[] arr;
        public Statistic()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            chart1.Series["Паливо"].Points.Clear();
            chart1.Series["Час обробки"].Points.Clear();
            if (comboBox1.SelectedIndex != -1)
            {
                try
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("SELECT OperationName, FuilCosts, ProcessingTime "
                        + "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId "
                        + "WHERE Crop.CropId = @cuture "
                        + "ORDER BY ExecutionMonth", sqlConnection1);
                    command.Parameters.AddWithValue("@cuture", arr[comboBox1.SelectedIndex]);
                    SqlDataReader reader = command.ExecuteReader();
                    if (radioButton1.Checked)
                    {
                        while (reader.Read())
                        {
                            chart1.Series["Паливо"].Points.AddXY(reader[0].ToString(), Convert.ToInt32(reader[1]));
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            chart1.Series["Час обробки"].Points.AddXY(reader[0].ToString(), Convert.ToInt32(reader[2]));
                        }
                    }
                    reader.Dispose();

                    //заг к-ть палива для обробки поля з обраною культурою
                    SqlCommand command2 = new SqlCommand("SELECT SUM(FuilCosts) " +
                        "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId " +
                        "WHERE Crop.CropId = @cuture " +
                        "GROUP BY Crop.CropId", sqlConnection1);
                    command2.Parameters.AddWithValue("@cuture", arr[comboBox1.SelectedIndex]);
                    reader = command2.ExecuteReader();
                    reader.Read();
                    label4.Text = reader[0].ToString();
                    reader.Dispose();
                    //заг к-ть часу на обробку поля з обраною культурою
                    SqlCommand command3 = new SqlCommand("SELECT SUM(ProcessingTime) " +
                        "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId " +
                        "WHERE Crop.CropId = @cuture " +
                        "GROUP BY Crop.CropId", sqlConnection1);
                    command3.Parameters.AddWithValue("@cuture", arr[comboBox1.SelectedIndex]);
                    reader = command3.ExecuteReader();
                    reader.Read();
                    label5.Text = reader[0].ToString();
                    sqlConnection1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void Statistic_Load(object sender, EventArgs e)
        {
            
            try //заповнюємо comboBox1
            {
                sqlConnection1.Open();
                SqlCommand command0 = new SqlCommand("SELECT COUNT(*) FROM (SELECT CropName, CropId FROM Crop WHERE CropId IN (SELECT Crop.CropId FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId))t", sqlConnection1);
                SqlDataReader reader = command0.ExecuteReader();
                reader.Read();
                arr = new int[reader.GetInt32(0)]; //КІЛЬКІСТЬ КУЛЬТУР ДЛЯ ЯКИХ Є ТЕХ КАРТА 
                int i = 0;

                reader.Dispose();
                SqlCommand command = new SqlCommand("SELECT CropId, CropName FROM Crop WHERE CropId IN (SELECT Crop.CropId FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId)", sqlConnection1);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    arr[i] = reader.GetInt32(0);
                    comboBox1.Items.Add(reader.GetString(1));
                    i++;
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                chart1.Series["Паливо"].Points.Clear();
                chart1.Series["Час обробки"].Points.Clear();
                if (comboBox1.SelectedIndex != -1)
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("SELECT OperationName, FuilCosts "
                        + "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId "
                        + "WHERE Crop.CropId = @cuture "
                        + "ORDER BY ExecutionMonth", sqlConnection1);
                    command.Parameters.AddWithValue("@cuture", arr[comboBox1.SelectedIndex]);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chart1.Series["Паливо"].Points.AddXY(reader[0].ToString(), Convert.ToInt32(reader[1]));
                    }
                    sqlConnection1.Close();
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                chart1.Series["Паливо"].Points.Clear();
                chart1.Series["Час обробки"].Points.Clear();
                if (comboBox1.SelectedIndex != -1)
                {
                    sqlConnection1.Open();
                    SqlCommand command = new SqlCommand("SELECT OperationName, ProcessingTime "
                        + "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId "
                        + "WHERE Crop.CropId = @cuture "
                        + "ORDER BY ExecutionMonth", sqlConnection1);
                    command.Parameters.AddWithValue("@cuture", arr[comboBox1.SelectedIndex]);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chart1.Series["Час обробки"].Points.AddXY(reader[0].ToString(), Convert.ToInt32(reader[1]));
                    }
                    sqlConnection1.Close();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
