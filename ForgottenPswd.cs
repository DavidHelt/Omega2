using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omega
{
    public partial class ForgottenPswd : Form
    {
        private SqlConnection newConnection;
        private bool canChangePassword = false; // Step 1: Define a boolean variable

        public ForgottenPswd()
        {
            InitializeComponent();
            newConnection = Database.GetInstance();
        }

        /// <summary>
        /// Verifies the identity of a user by matching the provided username and hobby with the database records.
        /// If the user's identity is verified, it allows the user to proceed with password change by setting a flag.
        /// This method checks for empty input fields, opens a database connection, and executes SQL commands to verify the user's identity.
        /// Appropriate messages are displayed to the user based on the outcome of the verification process.
        /// </summary>
        private void VerifyIdentityAndAllowPasswordChange()
        {
            string username = textBox1.Text;
            string hobby = textBox2.Text;
            SqlConnection con = newConnection;
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter a username.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(hobby))
                {
                    MessageBox.Show("Please enter your hobby.");
                    return;
                }

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT id_user FROM Users WHERE username=@username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var userId = cmd.ExecuteScalar();

                    if (userId != null)
                    {
                        using (SqlCommand cmdHobby = new SqlCommand("SELECT COUNT(*) FROM User_Info WHERE id_user=@UserId AND CAST(hobby AS NVARCHAR(MAX))=@Hobby", con))
                        {
                            cmdHobby.Parameters.AddWithValue("@UserId", userId);
                            cmdHobby.Parameters.AddWithValue("@Hobby", hobby);
                            int match = (int)cmdHobby.ExecuteScalar();

                            if (match > 0)
                            {
                                MessageBox.Show("Identity verified. You can change your password.");
                                canChangePassword = true; // Step 2: Set the variable to true if hobby check passes
                            }
                            else
                            {
                                MessageBox.Show("Hobby does not match our records. Please try again.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerifyIdentityAndAllowPasswordChange();
        }


        /// <summary>
        /// Handles the password update process for a user who has forgotten their password.
        /// This method first checks if the user has successfully verified their identity.
        /// If verified, it then validates the new password against a set of criteria (presence of digits, uppercase letters, and minimum length).
        /// Upon successful validation, it updates the user's password in the database.
        /// If the password update is successful, it redirects the user to the login form.
        /// Otherwise, it displays an appropriate error message.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!canChangePassword) // Step 3: Check the variable before allowing password change
            {
                MessageBox.Show("You must verify your identity before changing your password.");
                return;
            }

            string regexPassword = @"^(?=.*\d)(?=.*[A-Z]).{7,}$";
            Regex rg = new Regex(regexPassword);

            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("The passwords do not match. Please try again.");
                return;
            }
            else if (!rg.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Please make a stronger password -> At least 1 number, 1 uppercase letter, and 7 characters");
                return;
            }

            SqlConnection con = newConnection;
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Users SET passw=@NewPassword WHERE username=@Username", con))
                {
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@NewPassword", textBox3.Text);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Your password has been updated successfully.");
                        LoginForm form1 = new LoginForm();
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error updating password. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.Show();
            this.Hide();
        }
    }
    }

