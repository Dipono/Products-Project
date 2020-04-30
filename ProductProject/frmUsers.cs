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
    public partial class frmUsers : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Boston\Products.accdb");

        public frmUsers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain myMain = new frmMain();
            myMain.Show();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            loadUser();
            controlLoad();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            int count = 0;
            conn.Open();
            string query = @"select *
                           from tblproduct
                           where prodName='" + txtUsername.Text + "'";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);

            OleDbDataReader MyReader = MyCommand.ExecuteReader();

            while (MyReader.Read())
            {
                count++;
            }
            conn.Close();

            if (btnCreate.Text == "Save")
            {
                controlLoad();

                if (count >= 1)
                {
                    MessageBox.Show("User does exist", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    clearTextBoxes();
                }

                else
                {
                    if (txtUsername.Text == "")
                    {
                        MessageBox.Show("UserName field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtPassword.Text == "")
                    {
                        MessageBox.Show("Password field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtFirstName.Text == "")
                    {
                        MessageBox.Show("First Name field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (txtLastName.Text == "")
                    {
                        MessageBox.Show("Last Name field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        createUser();
                        MessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clearTextBoxes();
                        loadUser();
                    }

                }

            }

            else
            {
                controlCreate();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            controlEdit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateUser();
            clearTextBoxes();
            loadUser();
            controlLoad();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteUser();
            clearTextBoxes();
            loadUser();
            controlLoad();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string sql = @"select * 
                            from tbluser
                            where username = '" + cmbUser.Text + "'";

            OleDbCommand MyCommand = new OleDbCommand(sql, conn);

            OleDbDataReader MYReader = MyCommand.ExecuteReader();

            while (MYReader.Read())
            {
                txtUsername.Text = MYReader["userName"].ToString();
                txtPassword.Text = MYReader["userpassword"].ToString();
                txtFirstName.Text = MYReader["firstName"].ToString();
                txtLastName.Text = MYReader["lastName"].ToString();
            }
            conn.Close();
        }

        private void loadUser()
        {
            cmbUser.Items.Clear();

            conn.Open();
            string query = @"select * 
                                from tbluser";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);
            OleDbDataReader MyReader = MyCommand.ExecuteReader();

            while (MyReader.Read())
            {
                string userName = MyReader["userName"].ToString();

                cmbUser.Items.Add(userName);
            }
            conn.Close();
        }

        private void controlLoad()
        {
            cmbUser.Enabled = true;

            btnCreate.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = true;
            btnUpdate.Enabled = false;
            btnReturn.Enabled = true;

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;


            btnCreate.Text = "Create";
        }

        private void controlCreate()
        {
            cmbUser.Enabled = false;
            btnCreate.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnReturn.Enabled = false;

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;

            btnCreate.Text = "Save";
        }

        private void controlEdit()
        {
            cmbUser.Enabled = false;
            btnCreate.Enabled = false;
            btnDelete.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = true;
            btnReturn.Enabled = false;

            txtUsername.Enabled = false;
            txtPassword.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;

            btnUpdate.Text = "Update";
        }

        private void clearTextBoxes()
        {
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
            cmbUser.Text = "";
        }

        private void createUser()
        {
            string userName = txtUsername.Text;
            string password = txtPassword.Text;
            string fName = txtFirstName.Text;
            string lName = txtLastName.Text;

            conn.Open();
            string query = @"insert into tbluser(userName,userPassword,firstName,lastName)
                                values('" + userName + "','" + password + "','" + fName +"','"+ lName + "')";
            OleDbCommand MyCommand = new OleDbCommand(query, conn);
            MyCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void deleteUser()
        {
            conn.Open();
            string sql = @"delete from tblUser
                           where userName ='" + txtUsername.Text + "'";
            OleDbCommand MyCommand = new OleDbCommand(sql, conn);

            MyCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void updateUser()
        {
            conn.Open();
            string sql = @"update tblUser
                           set userpassword='" + txtPassword.Text +
                           "' ,firstName='" + txtFirstName.Text +
                           "' ,lastName='" + txtLastName.Text +
                           "' where userName ='" + txtUsername.Text + "'";
            OleDbCommand MyCommand = new OleDbCommand(sql, conn);
            MyCommand.ExecuteNonQuery();
            conn.Close();
        }


    }
}
