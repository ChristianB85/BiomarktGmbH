using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiomarktGmbH
{
    public partial class Products : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source =.\SQLEXPRESS; AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BioMarktDB.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductID;

        public Products()
        {
            InitializeComponent();
            ShowProducts();  
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if(textBoxProductName.Text == "" || 
                textBoxBrand.Text == "" || 
                comboBoxCategory.Text == "" || 
                textBoxPrice.Text == "")
            {
                MessageBox.Show("Bitte fülle alle Felder aus");
                return;
            }

            string productName = textBoxProductName.Text;
            string productBrand = textBoxBrand.Text;
            string productCategorie = comboBoxCategory.Text;
            string productPrice = textBoxPrice.Text;

            ExecuteQuery(string.Format("INSERT INTO Products values('{0}','{1}','{2}','{3}')", productName, productBrand, productCategorie, productPrice));

            ClearProductFields();
            ShowProducts();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductID == 0)
            {
                MessageBox.Show("Bitte wähle ein Produkt aus!");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxBrand.Text;
            string productCategory = comboBoxCategory.Text;
            string productPrice = textBoxPrice.Text;

            string query = string.Format("update Products set Name = '{0}', Brand = '{1}', Category = '{2}', Price = '{3}' where id = {4}",
                productName, 
                productBrand, 
                productCategory, 
                productPrice,
                lastSelectedProductID);

            ExecuteQuery(query);
            ShowProducts();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearProductFields();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if(lastSelectedProductID == 0)
            {
                MessageBox.Show("Bitte wähle ein Produkt aus!");
                return;
            }
            ExecuteQuery(string.Format("Delete from Products where ID = {0};", lastSelectedProductID));

            ClearProductFields();
            ShowProducts();
        }

        private void ShowProducts()
        {
            //Establish database connection
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);
            
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            ProductsDGV.DataSource = dataSet.Tables[0];
            ProductsDGV.Columns[0].Visible = false;

            databaseConnection.Close();
        }

        private void ClearProductFields()
        {
            textBoxProductName.Text = "";
            textBoxBrand.Text = "";
            comboBoxCategory.Text = "";
            textBoxPrice.Text = "";
            comboBoxCategory.SelectedItem = null;
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductName.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxBrand.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxCategory.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            lastSelectedProductID = (int)ProductsDGV.SelectedRows[0].Cells[0].Value;
        }

        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }
    }
}
