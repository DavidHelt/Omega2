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
        public ForgottenPswd()
        {
            InitializeComponent();
            newConnection = Database.GetInstance();
        }


        private void VerifyIdentityAndAllowPasswordChange()
        {
            string username = textBox1.Text;
            string hobby = textBox2.Text;
            // Assuming Database.GetInstance() returns a valid SqlConnection object
            SqlConnection con = newConnection;
            try
            {

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                    // Check if the username exists in the Users table
                using (SqlCommand cmd = new SqlCommand("SELECT id_user FROM Users WHERE username=@username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var userId = cmd.ExecuteScalar(); // Get the user ID if exists

                    if (userId != null)
                    {
                        // Now check if the hobby matches for the same user in User_Info table
                        using (SqlCommand cmdHobby = new SqlCommand("SELECT COUNT(*) FROM User_Info WHERE id_user=@UserId AND CAST(hobby AS NVARCHAR(MAX))=@Hobby", con))
                        {
                            cmdHobby.Parameters.AddWithValue("@UserId", userId);
                            cmdHobby.Parameters.AddWithValue("@Hobby", hobby);
                            int match = (int)cmdHobby.ExecuteScalar();

                            if (match > 0)
                            {
                                // If hobby matches, allow user to change password
                                MessageBox.Show("Identity verified. You can change your password.");
                                // Enable or show password change fields here
                            }
                            else
                            {
                                MessageBox.Show("Hobby does not match my records. Please try again.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            string regexPassword = @"^(?=.*\d)(?=.*[A-Z]).{7,}$";
            Regex rg = new Regex(regexPassword);

            // Check if the new password and confirm password match
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

            // Assuming Database.GetInstance() returns a valid SqlConnection object
            SqlConnection con = newConnection;
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                // Update the user's password in the database
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

