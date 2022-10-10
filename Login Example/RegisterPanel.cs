using System.Configuration;
using System.Text;

namespace Login_Example
{
    public partial class RegisterPanel : Form

    {

        string Connectionstr = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public RegisterPanel()
        {
            InitializeComponent();
            txtRegPassword.UseSystemPasswordChar = true;
            txtConfPassword.UseSystemPasswordChar = true;

        }

        private void RegisterPanel_Load(object sender, EventArgs e)
        {

        }
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }


        public String CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);

        }


        public String CreateSHA256Hash(String input, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }


        private void btnReg_Click(object sender, EventArgs e)
        {
            validateData.checkIfBlank(txtRegUsername.Text);
            validateData.checkIfBlank(txtRegPassword.Text);
            validateData.checkIfBlank(txtConfPassword.Text);

            if (validateData.PasswordMatch(txtRegPassword.Text, txtConfPassword.Text) is true && validateData.checkIfBlank(txtRegUsername.Text) is true && validateData.checkIfBlank(txtRegPassword.Text) is true && validateData.checkIfBlank(txtConfPassword.Text) is true)
            {

                bool usernameTaken = new dbConnection().checkUsername(txtRegUsername.Text);


                if (usernameTaken is false)
                {

                    //Adding encrpytion
                    String salt = CreateSalt(12);
                    String hashedpw = CreateSHA256Hash(txtRegPassword.Text, salt);

                    var createUser = new dbConnection().AddUser(txtRegUsername.Text, txtRegPassword.Text);

                    if (createUser is true)
                    {
                        MessageBox.Show("Acc created");
                    }
                    else
                    {
                        MessageBox.Show("Failed to create account");
                    }
                }
                else if (usernameTaken is true)
                {
                    MessageBox.Show("Username not available");
                }
            }
            else if (validateData.PasswordMatch(txtRegPassword.Text, txtConfPassword.Text) is false)
            {

                MessageBox.Show("Passwords does not match");

            }
            else if (validateData.checkIfBlank(txtRegUsername.Text) is false || validateData.checkIfBlank(txtRegPassword.Text) is false || validateData.checkIfBlank(txtConfPassword.Text) is false)
            {
                MessageBox.Show("Fields cannot be empty");
            }

        }
    }
}
