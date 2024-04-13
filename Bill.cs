using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiomarktGmbH
{
    public partial class Bill : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source =.\SQLEXPRESS; AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BioMarktDB.mdf;Integrated Security = True; Connect Timeout = 30");
        DataSet selectionDataSet = new DataSet();

        public Bill()
        {
            InitializeComponent();
            ShowTable("select * from Products", dataGridViewProducts);
            FormClosing += Bill_FormClosing;
        }

        private void Bill_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExecuteQuery(string.Format("DELETE from Bill;"));
        }

        private void ShowTable(string query, DataGridView dataGridView)
        {
            databaseConnection.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);
            DataSet dataSet = new DataSet();

            sqlDataAdapter.Fill(dataSet);

            dataGridView.DataSource = dataSet.Tables[0];
            dataGridView.Columns[0].Visible = false;

            if (dataGridView == dataGridSelection)
            {
                selectionDataSet = dataSet;
            }

            databaseConnection.Close();
        }

        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string productName = dataGridViewProducts.SelectedRows[0].Cells[1].Value.ToString();
            string productBrand = dataGridViewProducts.SelectedRows[0].Cells[2].Value.ToString();
            string productCategorie = dataGridViewProducts.SelectedRows[0].Cells[3].Value.ToString();
            string productPrice = dataGridViewProducts.SelectedRows[0].Cells[4].Value.ToString();


            ExecuteQuery(string.Format("INSERT INTO Bill values('{0}','{1}','{2}','{3}')", productName, productBrand, productCategorie, productPrice));
            ShowTable("select * from Bill", dataGridSelection);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            float totalAmount = 0;
            float value = 0;

            for(int i=0; i< dataGridSelection.Rows.Count; i++)
            {
                string priceAsString = dataGridSelection.Rows[i].Cells[4].Value.ToString();
                if (float.TryParse(priceAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                {
                    totalAmount += value;
                }
            }

            textBoxTotalAmount.Text = totalAmount.ToString() + " €";
        }
    }
}
