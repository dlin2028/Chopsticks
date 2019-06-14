using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chopsticks
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var gameScreen = new GameScreen(true, (int)Math.Pow(trackBar1.Value, 2));
            gameScreen.Closed += (s, args) => this.Close();
            gameScreen.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var gameScreen = new GameScreen(false, (int)Math.Pow(trackBar1.Value, 2));
            gameScreen.Closed += (s, args) => this.Close();
            gameScreen.Show();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            label7.Text = ((int)Math.Pow(trackBar1.Value, 2)).ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Both players start with one finger on each hand. You can attack by clicking your hand, then your opponent's hand. Attacks will increase the attacked hand by however many fingers you had on the attacking hand. You can also transfer fingers between your hands, but not all transfers are legal. The illegal transfers will be greyed out. If a finger has more than 5 fingers, it resets to 0. Any player with 0 fingers total on their hands loses."
                , "How To Play", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if(res == DialogResult.No)
            {
                MessageBox.Show("ok then dont play");
                Application.Exit();
            }
        }
    }
}
