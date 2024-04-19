using Omega;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Omega
{
    public partial class CaptchaDialog : Form
    {
        private readonly User user;
        private readonly PictureBox[] pictureBoxes;
        private readonly Random random;

        public CaptchaDialog(User user)
        {
            InitializeComponent();
            this.user = user;
            this.pictureBoxes = new PictureBox[]  // creates an array of pictureBoxes, that I will use for making their location random
            {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4,
                pictureBox5, pictureBox6, pictureBox7, pictureBox8
            };
            this.random = new Random();

            ShufflePictureLocations();
        }
        /// <summary>
        /// method that shuffles picture locations
        /// </summary>
        private void ShufflePictureLocations() 
        {
            // Define the bounds of the area for random positions
            int minX = 100;
            int maxX = ClientSize.Width - 100;
            int minY = 100;
            int maxY = ClientSize.Height - 100;

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                int x, y;
                do
                {
                    x = random.Next(minX, maxX);
                    y = random.Next(minY, maxY);
                } while (IsOverlap(pictureBox, x, y));

                pictureBox.Location = new Point(x, y);
            }
        }

        /// <summary>
        /// this method is keeping eye on overlapping pictures 
        /// </summary>
        /// <param name="currentPictureBox"></param>
        /// <param name="x"> coordinate X </param>
        /// <param name="y"> coordinate Y </param>
        /// <returns></returns>
        private bool IsOverlap(PictureBox currentPictureBox, int x, int y) 
        {
            Rectangle currentBounds = new Rectangle(x, y, currentPictureBox.Width, currentPictureBox.Height);

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                if (pictureBox != currentPictureBox && pictureBox.Bounds.IntersectsWith(currentBounds))
                {
                    return true; // Overlapping detected
                }
            }

            return false; // No overlapping
        }


        /// <summary>
        /// this is just repetetive click method on button that returns only message box...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

        /// <summary>
        /// This is another click method on button which will send you to Captcha 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cat ^_^", "Clickable image");
            this.Hide();
            Captcha2 c2 = new Captcha2(user);
            c2.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nope, not a cat (┬┬﹏┬┬)", "Clickable image");
        }

      
    }

}

     
