using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WFP_Demo01
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        Random random;
        int playerSpeed = 14;
        int carImage;
        int roadSpeed;
        int trafficSpeed;
        int score;
        int star = 0;
        int[] carLocation;

        public Form1()
        {
            InitializeComponent();
            
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void ResetGame()
        {
            explosion.Visible = false;
            award.Visible = false;
            goLeft = false;
            goRight = false;

            award.Image = Properties.Resources.bronze;

            score = 0;
            roadSpeed = 12;
            trafficSpeed = 8;

            random = new Random();
            carLocation = new int[] { 75, 80, 85, 195, 200, 205, 315, 320, 325, 425, 430, 435 };

            A1.Top = random.Next(100, 500) * -1;
            A1.Left = carLocation[random.Next(0, 6)];

            A2.Top = random.Next(100, 500) * -1;
            A2.Left = carLocation[random.Next(6, 12)];

            StartGame();
        }
        private void StartGame()
        {
            game_timer.Start();
        }
        private void game_timer_Tick(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            score++;

            if (goLeft == true && player.Left > 60)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 450)
            {
                player.Left += playerSpeed;
            }

            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;

            if (roadTrack2.Top > 700)
                roadTrack2.Top = -600;
            if (roadTrack1.Top > 700)
                roadTrack1.Top = -600;

            A1.Top += trafficSpeed;
            A2.Top += trafficSpeed;

            
            if (A1.Top > 700)
            {
                ChangeCars(A1);
            }
            if (A2.Top > 700)
            {
                ChangeCars(A2);
            }

            star1.Top += roadSpeed;
            star2.Top += roadSpeed;
            star3.Top += roadSpeed;
            star4.Top += roadSpeed;

            if (star1.Top > 700)
            {
                GetCoin(star1);
            }
            if (star2.Top > 700)
            {
                GetCoin(star2);
            }
            if (star3.Top > 700)
            {
                GetCoin(star3);
            }
            if (star4.Top > 700)
            {
                GetCoin(star4);
            }

            if (player.Bounds.IntersectsWith(star1.Bounds))
            {
                this.star1.Visible = false;
            }
            if (player.Bounds.IntersectsWith(star2.Bounds))
            {
                this.star2.Visible = false;
            }
            if (player.Bounds.IntersectsWith(star3.Bounds))
            {
                this.star3.Visible = false;
            }
            if (player.Bounds.IntersectsWith(star4.Bounds))
            {
                this.star4.Visible = false;
            }

            this.lblStar.Text = star.ToString();
            if (player.Bounds.IntersectsWith(A1.Bounds) || player.Bounds.IntersectsWith(A2.Bounds))
            {
                GameOver();
            }

            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.bronze;
            }


            if (score > 500 && score < 2000)
            {
                award.Image = Properties.Resources.silver;
                roadSpeed = 20;
                trafficSpeed = 18;
            }

            if (score > 2000)
            {
                award.Image = Properties.Resources.gold;
                trafficSpeed = 24;
                roadSpeed = 27;
            }

            pavRight.Top += roadSpeed;
            pavRight2.Top += roadSpeed;
            pavLeft.Top += roadSpeed;
            pavLeft2.Top += roadSpeed;

            if (pavRight.Top > 700)
                pavRight.Top = -650;
            if (pavRight2.Top > 700)
                pavRight2.Top = -650;
            if (pavLeft.Top > 700)
                pavLeft.Top = -650;
            if (pavLeft2.Top > 700)
                pavLeft2.Top = -650;

        }
        private void ChangeCars(PictureBox tempCar)
        {
            carImage = random.Next(1, 9);

            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
            }

            tempCar.Top = random.Next(50, 400) * -1;

            if (tempCar.Tag.Equals("carLeft"))
                tempCar.Left = carLocation[random.Next(0, 6)];
            if (tempCar.Tag.Equals("carRight"))
                tempCar.Left = carLocation[random.Next(6, 12)];
        }
        private void GetCoin(PictureBox state)
        {
            star = state.Visible == false ? ++star : star;
            state.Visible = true;
            state.Top = random.Next(100, 1000) * -1;
            lblStar.Text = star.ToString();
        }
        void GameOver()
        {
            playSound();
            game_timer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();
            
            btnReload.Visible = true;
            btnExit.Visible = true;
            btnReload.Enabled = true;
            btnExit.Enabled = true;
            btnReload.BringToFront();
            btnExit.BringToFront();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void playSound()
        {
            System.Media.SoundPlayer playCrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            playCrash.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGame();
        }
        private void LoadGame()
        {
            this.panel1.Visible = false;
            this.btnPlay.Enabled = true;
            this.btnClose.Enabled = true;
            this.pnCarModel.Visible = false;
            this.pnMain.Visible = true;

            this.btnReload.Enabled = false;
            this.btnExit.Enabled = false;
            this.btnReload.Visible = false;
            this.btnExit.Visible = false;

            A1.BringToFront();
            A2.BringToFront();
            player.BringToFront();
            panel4.BringToFront();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.btnPlay.Enabled = false;
            this.btnClose.Enabled= false;
            this.pnCarModel.Visible = true;
            this.pnMain.Visible = false;
            this.panel1.Visible = false;

            this.btnNext.Enabled = true;
            this.btnBack.Enabled = true;

            this.btnCarRed.Enabled = true;
            this.btnCarPink.Enabled = true;
            this.btnCarTruckBlue.Enabled = true;
            this.btnCarOrange.Enabled = true;
            this.btnCarGrey.Enabled = true;

            this.lblSumStar.Text = star.ToString();
        }


        private void CarModel_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            switch (p.Name)
            {
                case "carYellow":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.carYellow;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.carYellow;
                    break;
                case "carRed":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.CarRed;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.CarRed;
                    break;
                case "carPink":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.carPink;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.carPink;
                    break;
                case "carOrange":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.carOrange;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.carOrange;
                    break;
                case "carGrey":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.carGrey;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.carGrey;
                    break;
                case "carTruckBlue":
                    this.carPlayer.Image = global::WFP_Demo01.Properties.Resources.TruckBlue;
                    this.player.Image = global::WFP_Demo01.Properties.Resources.TruckBlue;
                    break;
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.pnCarModel.Visible = false;
            this.btnNext.Enabled = false;
            this.btnBack.Enabled = false;
            this.pnMain.Visible = false;
            this.panel1.Visible = true;

            this.btnCarRed.Enabled = false;
            this.btnCarPink.Enabled = false;
            this.btnCarTruckBlue.Enabled = false;
            this.btnCarOrange.Enabled = false;
            this.btnCarGrey.Enabled = false;

            ResetGame();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            btnReload.Enabled = false;
            btnExit.Enabled = false;
            btnReload.Visible = false;
            btnExit.Visible = false;
            ResetGame();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnReload.Enabled = false;
            btnExit.Enabled = false;
            btnReload.Visible = false;
            btnExit.Visible = false;
            LoadGame();
        }

        private void btnCarRed_Click(object sender, EventArgs e)
        {
            if(star >= 20)
            {
                carRed.Enabled = true;
                star -= 20;
                lblSumStar.Text = star.ToString();
                pcbLock1.Visible = false;
                btnCarRed.Enabled = false;
                btnCarRed.Visible = false;
            }
        }

        private void btnCarPink_Click(object sender, EventArgs e)
        {
            if(star >= 30)
            {
                carPink.Enabled = true;
                star -= 30;
                lblSumStar.Text = star.ToString();
                pcbLock2.Visible = false;
                btnCarPink.Enabled = false;
                btnCarPink.Visible = false;
            }
        }

        private void btnCarOrange_Click(object sender, EventArgs e)
        {
            if (star >= 50)
            {
                carOrange.Enabled = true;
                star -= 50;
                lblSumStar.Text = star.ToString();
                pcbLock3.Visible = false;
                btnCarOrange.Enabled = false;
                btnCarOrange.Visible = false;
            }
        }

        private void btnCarGrey_Click(object sender, EventArgs e)
        {
            if (star >= 80)
            {
                carGrey.Enabled = true;
                star -= 80;
                lblSumStar.Text = star.ToString();
                pcbLock4.Visible = false;
                btnCarGrey.Enabled = false;
                btnCarGrey.Visible = false;
            }
        }

        private void btnCarTruckBlue_Click(object sender, EventArgs e)
        {
            if (star >= 120)
            {
                carTruckBlue.Enabled = true;
                star -= 120;
                lblSumStar.Text = star.ToString();
                pcbLock5.Visible = false;
                btnCarTruckBlue.Enabled = false;
                btnCarGrey.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
