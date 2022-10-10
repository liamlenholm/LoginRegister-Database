using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Example
{
    internal class validateData
    {
        public static bool checkIfBlank(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                //MessageBox.Show(type + " cannot be blank");
                return false;
            }
            else 
            {
                return true;
            }
        }

        public static bool PasswordMatch(string input1, string input2)
        {
            if (input1 == input2)
            {
                return true;
            } else
            {
                return false;
            }
        }

        
    }
}
