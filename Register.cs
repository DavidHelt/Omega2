using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Omega;

namespace Omega
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// create account using button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text; // New textbox for password confirmation
            string regexPassword = @"^(?=.*\d)(?=.*[A-Z]).{7,}$";

            Regex rg = new Regex(regexPassword);
            if (username.Length < 5)
            {
                MessageBox.Show("Username is too short");
            }
            else if (!(rg.IsMatch(password)))
            {
                MessageBox.Show("Please make a stronger password -> At least 1 number, 1 uppercase letter, and 7 characters");
            }
            else if (password != confirmPassword) // Check if password and confirmation match
            {
                MessageBox.Show("Password and confirmation do not match");
            }
            else
            {
                // Connect to database and insert the new user
                SqlConnection con = Database.GetInstance();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users(username,passw) values(@username,@password)", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account has been created.");
                    LoginForm form1 = new LoginForm();
                    form1.Show();
                    this.Hide();
                }
            }

        }
        /// <summary>
        /// button that will send you back to the login (form1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();

        }

    
    }
}



