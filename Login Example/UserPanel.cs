using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Example
{


    public partial class UserPanel : Form
    {
        public string currentUser { get; set; }

        public UserPanel()
        {
            InitializeComponent();
        }

        private void UserPanel_Load(object sender, EventArgs e)
        {
           
            label1.Text = ("Hello " + currentUser + " lala");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
