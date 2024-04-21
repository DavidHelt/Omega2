using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Omega;
using System.Security.Cryptography;
using System.Text;

namespace Omega
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create account using button, check if username is taken and if password is valid
        /// Insert username and HASHED password into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text;
            string regexPassword = @"^(?=.*\d)(?=.*[A-Z]).{7,}$";

            Regex rg = new Regex(regexPassword);
            if (username.Length < 4)
            {
                MessageBox.Show("Username is too short");
            }
            else if (!(rg.IsMatch(password)))
            {
                MessageBox.Show("Please make a stronger password -> At least 1 number, 1 uppercase letter, and 7 characters");
            }
            else if (password != confirmPassword)
            {
                MessageBox.Show("Password and confirmation do not match");
            }
            else
            {
                // Hash the password
                string hashedPassword = ComputeSha256Hash(password);

                SqlConnection con = Database.GetInstance();
                SqlCommand checkUser = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username=@username", con);
                checkUser.Parameters.AddWithValue("@username", username);
                int userExists = (int)checkUser.ExecuteScalar();

                if (userExists > 0)
                {
                    MessageBox.Show("This username is already taken. Please choose a different username.");
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users(username,passw) values(@username,@password)", con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account has been created.");
                        LoginForm form1 = new LoginForm();
                        form1.Show();
                        this.Hide();
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
        /// button that will send you back to the login (first form)
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



