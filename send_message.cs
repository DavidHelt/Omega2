using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using static Omega.Form1;

namespace Omega
{
    public partial class send_message : UserControl
    {
        private readonly User user;
        public send_message(User user)
        {
            InitializeComponent();
            this.user = user;
        }


        /// <summary>
        /// this method will handle button click to send message and is used with simple sql
        /// commands to insert message into database...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SqlConnection con = Database.GetInstance();
            // Get the subject, receiver, and message from the form
            string receiver = textBox1.Text;
            string[] multiple_receivers = receiver.Split(',');
            string subject = textBox2.Text;
            string message = richTextBox1.Text;

            bool isErrorOccurred = false; 

            try
            {
                foreach (var rec in multiple_receivers)
                {
                    SqlDataReader reader = null;
                    try
                    {
                        using (SqlCommand checkUserCommand = new SqlCommand("SELECT username FROM Users WHERE username = @receiver", con))
                        {
                            checkUserCommand.Parameters.AddWithValue("@receiver", rec);
                            reader = checkUserCommand.ExecuteReader();

                            if (reader.Read())
                            {
                                // Insert the message into the database
                                using (SqlCommand insertMessage = new SqlCommand("INSERT INTO Messages (subject, message, sent_at, id_user, receiver) VALUES (@subject, @message, @sent_at, @id_user, @receiver)", con))
                                {
                                    insertMessage.Parameters.AddWithValue("@subject", subject);
                                    insertMessage.Parameters.AddWithValue("@message", message);
                                    insertMessage.Parameters.AddWithValue("@sent_at", formattedDateTime);
                                    insertMessage.Parameters.AddWithValue("@id_user", user.id);
                                    insertMessage.Parameters.AddWithValue("@receiver", rec);
                                    reader.Close();
                                    insertMessage.ExecuteNonQuery();
                                }

                                // Get the ID of the message
                                using (SqlCommand getMessageId = new SqlCommand("SELECT @@IDENTITY", con))
                                {
                                    object messageId = getMessageId.ExecuteScalar();

                                    // Insert the message into the User_Messages table
                                    using (SqlCommand insertUserMessage = new SqlCommand("INSERT INTO User_Messages (message_id, user_id) VALUES (@message_id, @user_id)", con))
                                    {
                                        insertUserMessage.Parameters.AddWithValue("@message_id", messageId);
                                        insertUserMessage.Parameters.AddWithValue("@user_id", user.id);
                                        insertUserMessage.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"User '{rec}' does not exist");
                                isErrorOccurred = true; // isErrorOccurred is true in case that the user does not exist
                            }
                        }
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }
                }

                if (!isErrorOccurred) // Notification pops only when no error occurred
                {
                    MessageBox.Show("Message sent successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sending the message: {ex.Message}");
            }
        }




    }

}
