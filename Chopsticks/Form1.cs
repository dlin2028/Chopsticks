using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chopsticks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        GameTree gameTree;
        int currHand = 0;
        PictureBox[] picBoxes;
        Button[] buttons;

        void updateUI()
        {
            if (humanFirst)
            {
                for (int i = 0; i < 4; i++)
                {
                    buttons[i].Enabled = gameTree.CurrentStatus.Hands[1] > i;
                }
                for (int i = 4; i < 8; i++)
                {
                    buttons[i].Enabled = gameTree.CurrentStatus.Hands[0] > i - 4;
                }

                if(gameTree.CurrentStatus.Hands[0] == 0)
                {
                    buttons[gameTree.CurrentStatus.Hands[1] - 1].Enabled = false;
                }
                else if(gameTree.CurrentStatus.Hands[1] == 0)
                {
                    buttons[gameTree.CurrentStatus.Hands[0] + 3].Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    buttons[i].Enabled = gameTree.CurrentStatus.Hands[3] > i;
                }
                for (int i = 4; i < 8; i++)
                {
                    buttons[i].Enabled = gameTree.CurrentStatus.Hands[2] > i - 4;
                }
            } 

            for (int i = 0; i < picBoxes.Length; i++)
            {
                if(humanFirst && i < 2)
                {
                    switch (gameTree.CurrentStatus.Hands[i])
                    {
                        case 0:
                            picBoxes[i].Image = Properties.Resources.r0;
                            break;
                        case 1:
                            picBoxes[i].Image = Properties.Resources.r1;
                            break;
                        case 2:
                            picBoxes[i].Image = Properties.Resources.r2;
                            break;
                        case 3:
                            picBoxes[i].Image = Properties.Resources.r3;
                            break;
                        case 4:
                            picBoxes[i].Image = Properties.Resources.r4;
                            break;
                    }
                }
                else if(humanFirst)
                {
                    switch (gameTree.CurrentStatus.Hands[i])
                    {
                        case 0:
                            picBoxes[i].Image = Properties.Resources._0;
                            break;
                        case 1:
                            picBoxes[i].Image = Properties.Resources._1;
                            break;
                        case 2:
                            picBoxes[i].Image = Properties.Resources._2;
                            break;
                        case 3:
                            picBoxes[i].Image = Properties.Resources._3;
                            break;
                        case 4:
                            picBoxes[i].Image = Properties.Resources._4;
                            break;
                    }
                }
                else if(i < 2)
                {
                    switch (gameTree.CurrentStatus.Hands[i])
                    {
                        case 0:
                            picBoxes[i].Image = Properties.Resources.r0;
                            break;
                        case 1:
                            picBoxes[i].Image = Properties.Resources.r1;
                            break;
                        case 2:
                            picBoxes[i].Image = Properties.Resources.r2;
                            break;
                        case 3:
                            picBoxes[i].Image = Properties.Resources.r3;
                            break;
                        case 4:
                            picBoxes[i].Image = Properties.Resources.r4;
                            break;
                    }
                }
                else
                {
                    switch (gameTree.CurrentStatus.Hands[i])
                    {
                        case 0:
                            picBoxes[i].Image = Properties.Resources._0;
                            break;
                        case 1:
                            picBoxes[i].Image = Properties.Resources._1;
                            break;
                        case 2:
                            picBoxes[i].Image = Properties.Resources._2;
                            break;
                        case 3:
                            picBoxes[i].Image = Properties.Resources._3;
                            break;
                        case 4:
                            picBoxes[i].Image = Properties.Resources._4;
                            break;
                    }
                }
            }
        }

        bool humanFirst;
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rng = new Random();
            humanFirst = true;//rng.Next(0, 2) == 0;
            gameTree = new GameTree(2, humanFirst);
            picBoxes = new[] {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4
            };
            buttons = new[]
            {
                button1,
                button2,
                button3,
                button4,
                button5,
                button6,
                button7,
                button8,
                button9
            };

            label2.Text = (humanFirst ? 0 : 2).ToString();
            label3.Text = (humanFirst ? 1 : 3).ToString();
            label4.Text = (humanFirst ? 2 : 0).ToString();
            label5.Text = (humanFirst ? 3 : 1).ToString();

            updateUI();
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            currHand = humanFirst ? 0 : 2;
            pictureBox1.BackColor = Color.Green;
            pictureBox2.BackColor = Color.White;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            currHand = humanFirst ? 1 : 3;
            pictureBox2.BackColor = Color.Green;
            pictureBox1.BackColor = Color.White;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            gameTree.Attack(humanFirst ? 2 : 0, currHand);
            updateUI();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            gameTree.Attack(humanFirst ? 3 : 1, currHand);
            updateUI();
        }

        void transfer(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
            {
                if (((Button)sender).Tag as string == "l" + i.ToString())
                {
                    gameTree.Transfer(humanFirst ? 0 : 2, humanFirst ? 1 : 3, i);
                }
                else if(((Button)sender).Tag as string == "r" + i.ToString())
                {
                    gameTree.Transfer(humanFirst ? 1 : 3, humanFirst ? 0 : 2, i);
                }
            }
            updateUI();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
