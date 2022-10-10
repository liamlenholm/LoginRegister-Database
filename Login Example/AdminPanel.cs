using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Login_Example
{
    public partial class AdminPanel : Form
    {
        string Connectionstr = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            reloadTable();
        }

        public void reloadTable()
        {
            using (SqlConnection con = new SqlConnection(Connectionstr))
            {
                con.Open();
                string query = "SELECT * FROM users";
                SqlConnection connection = new SqlConnection(Connectionstr);
                SqlDataAdapter dataadapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                connection.Open();
                dataadapter.Fill(ds, "users");
                con.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "users";
            }
        }

        private void btnMakeAdmin_Click(object sender, EventArgs e)
        {

            int currentRow = dataGridView1.CurrentCell.RowIndex;
            string msg = "Are you sure you want to give " + dataGridView1.Rows[currentRow].Cells[1].Value.ToString() + " Admin privileges?";
            DialogResult dialogResult = MessageBox.Show(msg, "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(Connectionstr))
                {
                    con.Open();
                    string query = "UPDATE users SET UserIsAdmin=1 WHERE Username=@username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            reloadTable();
        }

        private void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentCell.RowIndex;
            string msg = "Are you sure you want to remove " + dataGridView1.Rows[currentRow].Cells[1].Value.ToString() + " Admin privileges?";
            DialogResult dialogResult = MessageBox.Show(msg, "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(Connectionstr))
                {
                    con.Open();
                    string query = "UPDATE users SET UserIsAdmin=0 WHERE Username=@username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            int currentRow = dataGridView1.CurrentCell.RowIndex;
            string msg = "Are you sure you want to remove " + dataGridView1.Rows[currentRow].Cells[1].Value.ToString() + " account?";
            DialogResult dialogResult = MessageBox.Show(msg, "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(Connectionstr))
                {
                    con.Open();
                    string query = "DELETE FROM users WHERE Username=@username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", dataGridView1.Rows[currentRow].Cells[1].Value.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
    }
}
