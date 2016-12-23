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
    public partial class TechDelete : Form
    {
        int cropId;
        public TechDelete(int id)
        {
            InitializeComponent();
            cropId = id;
        }

        private void updateGridView()
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand command = new SqlCommand("SELECT TechMap.MapName, TechOperation.OperationName, TechOperation.FuilCosts, TechOperation.ProcessingTime, TechOperation.ExecutionMonth " +
                    "FROM TechOperation RIGHT JOIN (Crop RIGHT JOIN TechMap ON Crop.CropId = TechMap.CropId) ON TechOperation.MapId = TechMap.MapId "+
                    "WHERE Crop.CropId = @id", sqlConnection1);
                command.Parameters.AddWithValue("@id", cropId);
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

        private void TechDelete_Load(object sender, EventArgs e)
        {
            //try //заповнюємо comboBox1
            //{
            //    sqlConnection1.Open();
            //    SqlCommand command0 = new SqlCommand("SELECT COUNT(*) FROM Crop", sqlConnection1);
            //    SqlDataReader reader = command0.ExecuteReader();
            //    reader.Read();
            //    arrOfCropId = new int[reader.GetInt32(0)]; //КІЛЬКІСТЬ КУЛЬТУР 
            //    int i = 0;

            //    reader.Dispose();
            //    SqlCommand command = new SqlCommand("SELECT CropId, CropName FROM Crop", sqlConnection1);
            //    reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        comboBox1.Items.Add(reader.GetString(1));
            //        arrOfCropId[i] = reader.GetInt32(0); //ЗАПИСУЄМО КОДИ КУЛЬТУР
            //        i++;
            //    }
            //    sqlConnection1.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
            updateGridView();
        }
    }
}
