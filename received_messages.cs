using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omega
{
    public partial class received_messages : UserControl
    {
        SqlConnection con = Database.GetInstance();
        private readonly User user;

        //get the user 
        public received_messages(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        /// <summary>
        /// this method will handle loading for received messages which I will load from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void received_messages_Load(object sender, EventArgs e)
        {
            string query = "SELECT m.subject, m.message, m.sent_at, u.username, m.receiver , m.is_received , m.id_message FROM Messages m JOIN Users u ON m.id_user = u.id_user WHERE m.receiver = @User ORDER BY m.sent_at DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@User", user.username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int isRecieved = Convert.ToInt32(reader[5]);

                        // Check if the message has been deleted
                        if (isRecieved == 0)
                        {
                            // Create a new panel for the message
                            Panel messagePanel = new Panel();
                            messagePanel.BorderStyle = BorderStyle.FixedSingle;
                            messagePanel.Margin = new Padding(5);
                            messagePanel.Padding = new Padding(5);
                            messagePanel.AutoSize = true;
                            messagePanel.Dock = DockStyle.Top;

                            // Create a label for the message subject
                            Label subjectLabel = new Label();
                            subjectLabel.Text = "Subject: " + reader[0].ToString();
                            subjectLabel.AutoSize = true;
                            subjectLabel.Dock = DockStyle.Top;
                            messagePanel.Controls.Add(subjectLabel);

                            // Create a label for the the sender
                            Label senderLabel = new Label();
                            senderLabel.Text = "From: " + reader[3].ToString();
                            senderLabel.AutoSize = true;
                            senderLabel.Dock = DockStyle.Top;
                            senderLabel.Padding = new Padding(0, 10, 0, 0);
                            messagePanel.Controls.Add(senderLabel);

                            // Create a label for the message sent at
                            Label sentLabel = new Label();
                            sentLabel.Text = "Sent: " + reader[2].ToString();
                            sentLabel.AutoSize = true;
                            sentLabel.Dock = DockStyle.Top;
                            sentLabel.Padding = new Padding(0, 10, 0, 0);
                            messagePanel.Controls.Add(sentLabel);

                            // Create a label for the message text
                            Label messageLabel = new Label();
                            messageLabel.Text = "Text:" + reader[1].ToString();
                            messageLabel.AutoSize = true;
                            messageLabel.Dock = DockStyle.Bottom;
                            messagePanel.Controls.Add(messageLabel);

                            Button deleteButton = new Button();
                            deleteButton.Tag = reader[6];
                            deleteButton.BackColor = Color.Transparent;
                            deleteButton.FlatAppearance.BorderSize = 0;
                            deleteButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
                            deleteButton.FlatAppearance.MouseOverBackColor = Color.LightGray;
                            deleteButton.FlatStyle = FlatStyle.Flat;
                            deleteButton.Image = global::Omega.Properties.Resources.trash;
                            deleteButton.ImageAlign = ContentAlignment.MiddleCenter;
                            deleteButton.Size = new Size(20, 30);
                            deleteButton.Dock = DockStyle.Right;
                            deleteButton.Cursor = Cursors.Hand;
                            deleteButton.Click += new EventHandler(DeleteButtonClick);
                            messagePanel.Controls.Add(deleteButton);


                            // Add the message panel to the FlowLayoutPanel
                            panel1.Controls.Add(messagePanel);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// This method handles delete button so it will delete message from both form and database...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            object messageId = button.Tag.ToString();
            // Update the is_recieved column for the message with the given id
            string updateQuery = "UPDATE Messages SET is_received = 1 WHERE id_message = @Id";
            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
            {
                cmd.Parameters.AddWithValue("@Id", messageId);
                cmd.ExecuteNonQuery();
            }
            // Remove the message panel from the FlowLayoutPanel
            Control messagePanel = button.Parent;
            panel1.Controls.Remove(messagePanel);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
