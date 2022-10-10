using System.Data.SqlClient;

namespace Login_Example
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty!");
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty!");
            }
            else
            {
                var validAccount = new dbConnection().LoginUser(txtUsername.Text, txtPassword.Text);
                var isAdmin = new dbConnection().checkAdmin(txtUsername.Text, txtPassword.Text);

                if (validAccount is true && isAdmin is true)
                {
                    AdminPanel admPanel = new AdminPanel();
                    admPanel.Show();
                    this.Hide();


                }
                else if (validAccount is true && isAdmin is false)
                {
                    UserPanel usrPanel = new UserPanel();
                    usrPanel.currentUser = txtUsername.Text;
                    usrPanel.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("The username or password you entered is incorrect");
                }
            }



        }

        public static void sqlConnect()
        {
            string sqlcon = "Server=localhost;Database=TestDB;Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(sqlcon);
        }




        private void toggleShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterPanel regPanel = new RegisterPanel();
            regPanel.Show();
            this.Hide();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}