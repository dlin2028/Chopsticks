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

        void updateUI()
        {
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
                else
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
            }
        }

        bool humanFirst;
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rng = new Random();
            humanFirst = rng.Next(0, 2) == 0;
            gameTree = new GameTree(2, humanFirst);
            picBoxes = new[] {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4
            };
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            currHand = 0;
            pictureBox1.BackColor = Color.Green;
            pictureBox2.BackColor = Color.White;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            currHand = 1;
            pictureBox2.BackColor = Color.Green;
            pictureBox1.BackColor = Color.White;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            gameTree.Attack(2, currHand);
            updateUI();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            gameTree.Attack(1, currHand);
            updateUI();
        }

        void transfer(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
            {
                if (((Button)sender).Tag as string == "l" + i.ToString())
                {
                    gameTree.Transfer(humanFirst ? 0 : 2, humanFirst ? 1 : 3);
                }
                else if(((Button)sender).Tag as string == "r" + i.ToString())
                {
                    gameTree.Transfer(humanFirst ? 1 : 3, humanFirst ? 0 : 2);
                }
            }
            updateUI();
        }
    }
}
