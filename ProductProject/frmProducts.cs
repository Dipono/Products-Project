using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ProductProject
{
    public partial class frmProducts : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Boston\Products.accdb");

        
        public frmProducts()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain myMain = new frmMain();
            myMain.Show();
        }


        private void frmProducts_Load(object sender, EventArgs e)
        {
            loadProduct();
            controlLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (btnCreated.Text == "Save")
            {
                controlLoad();

                int count = 0;
                conn.Open();
                string query = @"select *
                           from tblproduct
                           where prodName='" + txtProdName.Text +
                            "'and username ='" + frmLogin.username + "'";
                OleDbCommand MyCommand = new OleDbCommand(query, conn);

                OleDbDataReader MyReader = MyCommand.ExecuteReader();

                while (MyReader.Read())
                {
                    count++;
                }
                conn.Close();


                if (count >= 1)
                {
                    MessageBox.Show("Product does exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    clearTextBoxes();
                }

                else
                {
                    if (txtProdName.Text == "")
                    {
                        MessageBox.Show("Product Name field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if(txtProdDesc.Text == "")
                    {
                        MessageBox.Show("Product Description field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtProdPrice.Text == "")
                    {
                        MessageBox.Show("Product Price field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        createProduct();
                        MessageBox.Show("Saved Successfully","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        clearTextBoxes();
                        loadProduct();
                    }
                }  
            }

            else
            {
                controlCreate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controlEdit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateProduct();
            loadProduct();
            clearTextBoxes();
            controlLoad();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            deleteProduct();
            clearTextBoxes();
            loadProduct();
            controlLoad();

        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = @"select * 
                            from tblProduct 
                            where prodName = '" + cmbProducts.Text + "'";

                OleDbCommand MyCommand = new OleDbCommand(query, conn);

                OleDbDataReader MyReader = MyCommand.ExecuteReader();

                while (MyReader.Read())
                {
                    txtProdName.Text = MyReader["prodName"].ToString();
                    txtProdDesc.Text = MyReader["prodDesc"].ToString();
                    txtProdPrice.Text = MyReader["price"].ToString();
                }
                conn.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void loadProduct()
        {
            frmLogin myLog = new frmLogin();

            cmbProducts.Items.Clear();
            conn.Open();
            string query = @"select * 
                           from tblProduct 
                            where username ='"+frmLogin.username+"'";
            OleDbCommand MyCommand = new OleDbCommand(query,conn);
            OleDbDataReader MyReader = MyCommand.ExecuteReader();

            while (MyReader.Read())
            {
                string proName = MyReader["prodName"].ToString();

                cmbProducts.Items.Add(proName);
                
            }

              conn.Close();

        }

        private void controlLoad()
        {
            cmbProducts.Enabled = true;

            btnCreated.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = true;
            btnUpdate.Enabled = false;
            btnReturn.Enabled = true;

            txtProdName.Enabled = false;
            txtProdDesc.Enabled = false;
            txtProdPrice.Enabled = false;

            btnCreated.Text = "Create";
        }

        private void controlCreate()
        {
            cmbProducts.Enabled = false;
            btnCreated.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnReturn.Enabled = false;

            txtProdName.Enabled = true;
            txtProdDesc.Enabled = true;
            txtProdPrice.Enabled = true;

            btnCreated.Text = "Save";
        }

        private void controlEdit()
        {
            cmbProducts.Enabled = false;
            btnCreated.Enabled = false;
            btnDelete.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = true;
            btnReturn.Enabled = false;

            txtProdName.Enabled = true;
            txtProdDesc.Enabled = true;
            txtProdPrice.Enabled = true;

            btnUpdate.Text = "Update";
        }

        private void clearTextBoxes()
        {
            txtProdDesc.Text = "";
            txtProdName.Text = "";
            txtProdPrice.Clear();
            cmbProducts.Text = "";
        }


        private void createProduct()
        {
            string name =  txtProdName.Text;
            string description = txtProdDesc.Text;
            string price = txtProdPrice.Text;
           
        
            conn.Open();
            string query = @"insert into tblProduct(prodName,prodDesc,price,username)
                                values('" + name + "','" + description + "','" + price + "','" + frmLogin.username + "')";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);
            MyCommand.ExecuteNonQuery();
            conn.Close();

        }

        private void deleteProduct()
        {
            conn.Open();
            string query = @"delete from tblProduct
                           where prodName ='" + txtProdName.Text +
                           "'and username ='" + frmLogin.username + "'";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);

            MyCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void updateProduct()
        {
            conn.Open();
            string query = @"update tblProduct
                           set prodName='" + txtProdName.Text +
                           "' ,prodDesc='" + txtProdDesc.Text +
                           "' ,price='" + txtProdPrice.Text +
                           "' where prodName ='" + cmbProducts.Text + 
                           "'and username ='"+frmLogin.username+"'";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);
            MyCommand.ExecuteNonQuery();
            conn.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
           
    }
}
