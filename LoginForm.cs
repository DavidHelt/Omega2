using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omega;

namespace Omega
{
    public partial class LoginForm : Form
    {
        SqlConnection con = Database.GetInstance();
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the login process by verifying the user's credentials against the database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        /// <remarks>
        /// This method attempts to open a connection to the database and execute a SELECT query
        /// to find a user with the provided username and password. If a match is found, it proceeds
        /// to the CaptchaDialog; otherwise, it displays an error message.
        /// Note: This method assumes the connection is managed globally and does not explicitly close it.
        /// </remarks>
        private void button2_Click(object sender, EventArgs e)
        {
            //read from textbox 
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Ensure the connection is open
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE username=@username AND passw=@password", con))
            {
                //login
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User(reader[0].ToString(), textBox1.Text);
                        this.Hide();

                        CaptchaDialog captcha = new CaptchaDialog(user);
                        captcha.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
        }

        /// <summary>
        /// Opens the registration form when the sign in button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        /// <remarks>
        /// This method hides the current LoginForm and displays the Register form to the user.
        /// </remarks>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Register form3 = new Register();
            form3.Show();
            this.Hide();
        }

        /// <summary>
        /// Opens the forgotten password form when the corresponding button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        /// <remarks>
        /// This method hides the current LoginForm and displays the ForgottenPswd form to the user,
        /// allowing them to recover or reset their password.
        /// </remarks>
        private void button3_Click(object sender, EventArgs e)
        {
            ForgottenPswd form6 = new ForgottenPswd();
            form6.Show();
            this.Hide();
        }
    }
}