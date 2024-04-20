using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Omega
{
    public partial class UserProfile : Form
    {
        private readonly User user;
        private SqlConnection newConnection;

        public UserProfile(User user)
        {
            InitializeComponent();
            this.user = user;
            label1.Text = user.username;



            // Update the database connection
            newConnection = Database.GetInstance();

            // Load user's information from the database
            LoadUserInfo();
        }

        /// <summary>
        /// Loads the user's information from the database and displays it on the form.
        /// This method attempts to open a database connection if it's not already open,
        /// then queries the User_Info table for the current user's hobby, gender, and student status.
        /// The results are displayed on the form's controls.
        /// </summary>
       
        private void LoadUserInfo()
        {
            try
            {
                // Directly use the singleton connection without explicitly opening it
                SqlConnection connection = newConnection; // This is obtained from Database.GetInstance()

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open(); // Attempt to open the connection if it's not already open
                }
                string query = "SELECT hobby, gender, is_student FROM User_Info WHERE id_user = @UserId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", user.id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    textBox1.Text = reader["hobby"].ToString();
                    label6.Text = "Hobby: " + reader["hobby"].ToString(); // Display hobby on label6

                    string gender = reader["gender"].ToString();
                    label7.Text = "Gender: " + gender; // Display gender on label7
                    if (gender == "Male")
                        radioButton1.Checked = true;
                    else if (gender == "Female")
                        radioButton2.Checked = true;
                    else
                        radioButton3.Checked = true;

                    bool isStudent = Convert.ToBoolean(reader["is_student"]);
                    checkBox1.Checked = isStudent;
                    label8.Text = "Student: " + (isStudent ? "Yes" : "No"); // Display student status on label8
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu form2 = new Menu(user);
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save user's updated information to the database
            SaveUserInfo();

            // Display a notification message
            MessageBox.Show("Your profile has been updated", "Profile Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Saves the user's information to the database.
        /// This method checks if the user's information already exists in the database.
        /// If it does, the information is updated; if not, a new record is inserted.
        /// The method handles opening the database connection if it's not already open.
        /// </summary>
        private void SaveUserInfo()
        {
            try
            {
                // Use the singleton connection instance
                SqlConnection connection = newConnection; // This is obtained from Database.GetInstance()
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open(); // Attempt to open the connection if it's not already open
                }

                // Check if user info already exists
                string checkQuery = "SELECT COUNT(*) FROM User_Info WHERE id_user = @UserId";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@UserId", user.id);
                int exists = (int)checkCommand.ExecuteScalar();

                string query;
                if (exists > 0)
                {
                    // Update existing user info
                    query = "UPDATE User_Info SET hobby = @Hobby, gender = @Gender, is_student = @IsStudent WHERE id_user = @UserId";
                }
                else
                {
                    // Insert new user info
                    query = "INSERT INTO User_Info (id_user, hobby, gender, is_student) VALUES (@UserId, @Hobby, @Gender, @IsStudent)";
                }

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", user.id);
                command.Parameters.AddWithValue("@Hobby", textBox1.Text);
                command.Parameters.AddWithValue("@Gender", radioButton1.Checked ? "Male" : (radioButton2.Checked ? "Female" : "Other"));
                command.Parameters.AddWithValue("@IsStudent", checkBox1.Checked);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the current form
            var newForm = new UserProfile(user); // Create a new instance of Form4 with the current user
            newForm.Show(); // Show the new form
            this.Close(); // Close the current form
        }
    }
}