﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Omega;

namespace Omega
{
    public partial class Menu : Form
    {
        private readonly User user;
        private readonly Random random;
        private readonly HashSet<string> usedColors;
        private bool Error;
        public Menu(User user)
        {
            InitializeComponent();
            this.user = user;
            this.random = new Random();
            this.usedColors = new HashSet<string>();
            this.Error = false;
            labelUser.Text = "Logged in as: " + user.username;

        }

        /// <summary>
        /// adds a user control to the panel and brings it to the front 
        /// </summary>
        /// <param name="uc"></param>
        private void addUserControl(UserControl uc)
        {
            panel1.Controls.Clear();  // clear existing controls in panel
            panel1.Controls.Add(uc);  // add specified user control panel..
            uc.BringToFront();  // brings to the front
            this.panel1.AutoScroll = true;  // autoscrolling..

        }

        /// <summary>
        /// method for getting random color using generics
        /// </summary>
        /// <returns></returns>
        private string GetUniqueColor()
        {
            List<string> availableColors = Colors.ColorList.Except(usedColors).ToList();
            if (availableColors.Count == 0)
            {
                usedColors.Clear(); // Reset used colors to allow repetition
                availableColors = Colors.ColorList; // Reassign all colors as available
            }

            int randomIndex = random.Next(availableColors.Count);
            string colorHex = availableColors[randomIndex];
            usedColors.Add(colorHex);
            return colorHex;
        }

        /// <summary>
        /// method for assigning random color to button
        /// </summary>
        /// <param name="button"></param>
        private void AssignButtonColor(Button button)
        {
            string colorHex = GetUniqueColor();
            if (colorHex != null)
            {
                Color color = ColorTranslator.FromHtml(colorHex);
                button.BackColor = color;
            }
            else if(!Error)
            {
                
                Error = true;
            }
                         
        }
       
        /// <summary>
        /// hop on send messages component
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AssignButtonColor(button1);
            send_message sendMess = new send_message(user);
            addUserControl(sendMess);
        }
       
        /// <summary>
        /// hop on sent messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            AssignButtonColor(button2);
            sent_messages sentMess = new sent_messages(user);
            addUserControl(sentMess);
        }

        /// <summary>
        ///  received messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AssignButtonColor(button3);
            received_messages reciMess = new received_messages(user);
            addUserControl(reciMess);
        }

        /// <summary>
        /// loads form 2...
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///  this is a back button that will go back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }
        /// <summary>
        /// loads user form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            
            UserProfile form4 = new UserProfile(user);
            this.Hide();
            form4.Show();
        }

        /// <summary>
        /// loads help form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button6_Click(object sender, EventArgs e)
        {
            Help form5 = new Help(user);
            this.Hide();
            form5.Show();
        }
    }
}