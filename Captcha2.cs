using Omega;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Omega
{
    public partial class Captcha2 : Form
    {
        private readonly User user;
        public Captcha2(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        /// <summary>
        /// Same as in captcha 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, I want the real one", "Fake trash");

        }

        /// <summary>
        /// ... will send you to menu logged in as user you entered in form 1 :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the real one ( •_•)>⌐■-■ ", "Trash");
            Form2 form2 = new Form2(user);
            this.Hide();
            form2.Show();
        }

      
    }
}
