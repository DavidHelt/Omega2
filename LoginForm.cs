﻿using System;
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
        /// login button that uses simple sql commands to add username and password to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //read from textbox 
            string username = textBox1.Text;
            string password = textBox2.Text;
     
            using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE username=@username AND passw=@password", con))
            {
                //login
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User(reader[0].ToString(),textBox1.Text);           
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
        /// sign in button which will hop on form 3 which is simple register form :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Register form3 = new Register();
            form3.Show();
            this.Hide();
        }

      
    }
}