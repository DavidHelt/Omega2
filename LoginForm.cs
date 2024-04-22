using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Security.Cryptography;
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
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Hash the input password
            string hashedPassword = ComputeSha256Hash(password);

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE username=@username AND passw=@password", con))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashedPassword);

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
        /// Computes the SHA-256 hash of a given string and returns the hash as a hexadecimal string.
        /// This method is used for creating a secure hash of passwords before they are stored in the database.
        /// </summary>
        /// <param name="rawData">The input string to hash.</param>
        /// <returns>The hexadecimal string representation of the SHA-256 hash.</returns>
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
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


        private void button3_Click(object sender, EventArgs e)
        {
            ForgottenPswd form6 = new ForgottenPswd();
            form6.Show();
            this.Hide();
        }
    }
}