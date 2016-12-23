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
    public partial class TechAdd : Form
    {
        int[] arrOfCropId;
        public TechAdd()
        {
            InitializeComponent();
        }

        private void updateGridView()
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT Crop.CropName, TechMap.MapName, TechOperation.OperationName, TechOperation.FuilCosts, TechOperation.ProcessingTime, TechOperation.ExecutionMonth " +
                    "FROM TechOperation LEFT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId " +
                    "Order by CropName", sqlConnection1);
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
        }

        private void TechAditing_Load(object sender, EventArgs e)
        {
            try //заповнюємо comboBox1
            {
                sqlConnection1.Open();
                SqlCommand command0 = new SqlCommand("SELECT COUNT(*) FROM Crop", sqlConnection1);
                SqlDataReader reader = command0.ExecuteReader();
                reader.Read();
                arrOfCropId = new int[reader.GetInt32(0)]; //КІЛЬКІСТЬ КУЛЬТУР 
                int i = 0;

                reader.Dispose();
                SqlCommand command = new SqlCommand("SELECT CropId, CropName FROM Crop", sqlConnection1);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(1));
                    arrOfCropId[i] = reader.GetInt32(0); //ЗАПИСУЄМО КОДИ КУЛЬТУР
                    i++;
                }
                sqlConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            updateGridView();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                TechMap newTech = new TechMap(arrOfCropId[comboBox1.SelectedIndex]);
                newTech.ShowDialog();
                updateGridView();
            }
            else MessageBox.Show("Оберіть культуру для якої ви хочете додати технологічну карту.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                TechDelete newTech = new TechDelete(arrOfCropId[comboBox1.SelectedIndex]);
                newTech.ShowDialog();
                updateGridView();
            }
            else MessageBox.Show("Оберіть культуру для якої ви хочете додати технологічну карту.");
        }
    }
}
