﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StreetFighter
{
    public partial class Form1 : Form
    {
        bool kenOutOfBounds = false;
        bool ryuOutOfBounds = false;
        bool kenCollision = false;
        bool ryuCollision = false;
        Point pointRight;
        Point pointLeft;

        private void MicroTimerTest()
        {
            // Instantiate new MicroTimer and add event handler
            MicroLibrary.MicroTimer microTimer = new MicroLibrary.MicroTimer();
            microTimer.MicroTimerElapsed +=
                new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(OnTimedEvent);

            microTimer.Interval = 1000; // Call micro timer every 1000µs (1ms)

            // Can choose to ignore event if late by Xµs (by default will try to catch up)
            // microTimer.IgnoreEventIfLateBy = 500; // 500µs (0.5ms)

            microTimer.Enabled = true; // Start timer

            // Do something whilst events happening, for demo sleep 2000ms (2sec)
            //System.Threading.Thread.Sleep(2000);

            //microTimer.Enabled = false; // Stop timer (executes asynchronously)

            // Alternatively can choose stop here until current timer event has finished
            // microTimer.StopAndWait(); // Stop timer (waits for timer thread to terminate)

            // Wait for user input
        }



        private void OnTimedEvent(object sender, MicroLibrary.MicroTimerEventArgs timerEventArgs)
        {
            //<>
            if (KEN.Right >= RYU.Left + 10)
            {
                kenCollision = true;

                if (KEN.InvokeRequired)
                {

                    KEN.Invoke(new Action(() => KEN.Left = RYU.Left - RYU.Width + 10));
                }
                else
                {

                    KEN.Left = RYU.Left - RYU.Width + 10;
                }


            }
            else
            {
                kenCollision = false;
            }

            if (RYU.Left < KEN.Right - 10)
            {
                ryuCollision = true;

                if (RYU.InvokeRequired)
                {

                    RYU.Invoke(new Action(() => RYU.Left = KEN.Right - 10));
                }
                else
                {

                    RYU.Left = KEN.Right - 10;
                }


            }
            else
            {
                ryuCollision = false;
            }




            #region outOfBounds
            if (KEN.Right > screen.Right)
            {
                kenOutOfBounds = true;
                if (KEN.InvokeRequired)
                {
                    KEN.Invoke(new Action(() => KEN.Left = pointRight.X));
                }
                else
                {
                    KEN.Left = pointRight.X;
                }
            }
            else
            {
                kenOutOfBounds = false;
            }

            if (KEN.Left < screen.Left)
            {
                kenOutOfBounds = true;
                if (KEN.InvokeRequired)
                {
                    KEN.Invoke(new Action(() => KEN.Left = pointLeft.X));
                }
                else
                {
                    KEN.Left = pointLeft.X;
                }
            }
            else
            {
                kenOutOfBounds = false;
            }

            if (RYU.Left < screen.Left)
            {
                ryuOutOfBounds = true;
                if (RYU.InvokeRequired)
                {
                    RYU.Invoke(new Action(() => RYU.Left = pointLeft.X));
                }
                else
                {
                    RYU.Left = pointLeft.X;
                }
            }
            else
            {
                ryuOutOfBounds = false;
            }

            if (RYU.Right > screen.Right)
            {
                ryuOutOfBounds = true;
                if (RYU.InvokeRequired)
                {
                    RYU.Invoke(new Action(() => RYU.Left = pointRight.X));
                }
                else
                {
                    RYU.Left = pointRight.X;
                }
            }
            else
            {
                ryuOutOfBounds = false;
            }
            #endregion
        }

        bool CrossMovement;

        #region Player1Initialization
        bool P1Left;
        bool P1Right;
        bool P1jump;
        bool P1Punch;
        bool P1Kick;
        int P1JumpCounter = 0;
        #endregion
        #region Player2Initialization
        bool P2Left;
        bool P2Right;
        bool P2jump;
        bool P2Punch;
        bool P2Kick;
        int P2JumpCounter = 0;
        #endregion
        public Form1()
        {
            InitializeComponent();
            MicroTimerTest();
            pointRight = new Point(screen.Right - KEN.Width, KEN.Top);
            pointLeft = new Point(screen.Left, KEN.Top);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            screen.Image = StreetFighter.Properties.Resources.SfBG;

            RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
            KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;

            RYU.Parent = screen;
            KEN.Parent = screen;
            #region
            #region Player1ProgressBar
            PBplayer1.Minimum = 1;
            PBplayer1.Maximum = 100;
            PBplayer1.Value = 100;
            #endregion
            #region Player2ProgressBar
            PBplayer2.Minimum = 1;
            PBplayer2.Maximum = 100;
            PBplayer2.Value = 100;
            #endregion
            #endregion
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            #region
            #region CRMF
            if (CrossMovement == false)
            {
                #region Player1
                #region Right
                if (e.KeyCode == Keys.Right)
                {
                    if (!kenOutOfBounds)
                    {

                        if (!kenCollision)
                        {
                            P1Right = true;
                        }
                    }


                    if (P1jump != true)
                    {
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.Left)
                {
                    P1Left = true;

                    if (P1jump != true)
                    {
                        // KEN.BackColor = Color.Blue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.Up)
                {
                    P1jump = true;
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.A)
                {
                    P1Punch = true;

                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.golpe_corto);
                    golpe.Play();
                }
                #endregion
                #region Kick
                if (e.KeyCode == Keys.S)
                {
                    P1Kick = true;
                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.patada_1);
                    golpe.Play();
                }
                #endregion
                #endregion
                #region Player2
                #region Right
                if (e.KeyCode == Keys.L)
                {

                    P2Right = true;
                    if (P2jump != true)
                    {
                        //  RYU.BackColor = Color.Yellow;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.J)
                {
                    if (!ryuOutOfBounds)
                    {
                        if (!ryuCollision)
                        {
                            P2Left = true;
                        }
                    }

                    if (P2jump != true)
                    {
                        //  RYU.BackColor = Color.Blue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;


                    }

                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.I)
                {
                    P2jump = true;
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.B)
                {
                    P2Punch = true;
                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.golpe_corto);
                    golpe.Play();
                }
                #endregion
                #region Kick
                if (e.KeyCode == Keys.N)
                {
                    P2Kick = true;
                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.patada_1);
                    golpe.Play();
                }
                #endregion
                #endregion

            }
            #endregion
            #region CRMT
            else if (CrossMovement == true)
            {
                #region Player1
                #region Right
                if (e.KeyCode == Keys.Right)
                {
                    if (!kenOutOfBounds)
                    {
                        P1Right = true;
                    }


                    if (P1jump != true)
                    {
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.Left)
                {

                    P1Left = true;
                    if (P1jump != true)
                    {
                        // KEN.BackColor = Color.DarkBlue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.Up)
                {
                    P1jump = true;
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.A)
                {
                    P1Punch = true;
                }
                #endregion
                #region Kick
                if (e.KeyCode == Keys.S)
                {
                    P1Kick = true;
                }
                #endregion
                #endregion
                #region Player2
                #region Right
                if (e.KeyCode == Keys.L)
                {
                    P2Right = true;
                    if (P2jump != true)
                    {
                        //  RYU.BackColor = Color.DarkMagenta;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.J)
                {

                    P2Left = true;
                    if (P2jump != true)
                    {
                        // RYU.BackColor = Color.DarkBlue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                    }

                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.I)
                {
                    P2jump = true;
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.B)
                {
                    P2Punch = true;
                }
                #endregion
                #region Kick
                if (e.KeyCode == Keys.N)
                {
                    P2Kick = true;
                }
                #endregion
                #endregion



            }
            #endregion
            #endregion
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            #region
            #region CRMF
            if (CrossMovement == false)
            {
                #region Player1
                #region Right
                if (e.KeyCode == Keys.Right)
                {
                    P1Right = false;
                    //  KEN.BackColor = Color.Red;
                    KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;
                    KEN.BackgroundImageLayout = ImageLayout.Stretch;

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.Left)
                {
                    P1Left = false;
                    //  KEN.BackColor = Color.Red;
                    KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;
                    KEN.BackgroundImageLayout = ImageLayout.Stretch;

                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.Up)
                {
                    if (P1Right == true)
                    {
                        //     KEN.BackColor = Color.Yellow;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    else if (P1Left == true)
                    {
                        //  KEN.BackColor = Color.Blue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.A)
                {
                    if (P1Left != true && P1Right != true)
                    {
                        P1Punch = false;
                        //   KEN.BackColor = Color.Red;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P1Left == true)
                    {
                        P1Punch = false;
                        //    KEN.BackColor = Color.Blue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P1Right == true)
                    {
                        P1Punch = false;
                        //  KEN.BackColor = Color.Yellow;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                }
                #endregion
                #region Kick

                if (e.KeyCode == Keys.S)
                {
                    if (P1Left != true && P1Right != true)
                    {
                        P1Kick = false;
                        // KEN.BackColor = Color.Red;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P1Left == true)
                    {
                        P1Kick = false;
                        //    KEN.BackColor = Color.Blue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P1Right == true)
                    {
                        P1Kick = false;
                        // KEN.BackColor = Color.Yellow;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                }
                #endregion
                #endregion
                #region Player2
                #region Right
                if (e.KeyCode == Keys.L)
                {
                    P2Right = false;
                    // RYU.BackColor = Color.Red;
                    RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
                    RYU.BackgroundImageLayout = ImageLayout.Stretch;

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.J)
                {
                    P2Left = false;
                    //   RYU.BackColor = Color.Red;
                    RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
                    RYU.BackgroundImageLayout = ImageLayout.Stretch;
                }
                #endregion
                #region Up
                if (e.KeyCode == Keys.I)
                {
                    if (P2Right == true)
                    {
                        //  RYU.BackColor = Color.Yellow;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (P2Left == true)
                    {
                        // RYU.BackColor = Color.Blue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.B)
                {
                    if (P2Left != true && P2Right != true)
                    {
                        P2Punch = false;
                        // RYU.BackColor = Color.Red;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Left == true)
                    {
                        P2Punch = false;
                        //   RYU.BackColor = Color.Blue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Right == true)
                    {
                        P2Punch = false;
                        //  RYU.BackColor = Color.Yellow;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #region Kick

                if (e.KeyCode == Keys.N)
                {
                    if (P2Left != true && P2Right != true)
                    {
                        P2Kick = false;
                        //   RYU.BackColor = Color.Red;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Left == true)
                    {
                        P2Kick = false;
                        //  RYU.BackColor = Color.Blue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Right == true)
                    {
                        P2Kick = false;
                        // RYU.BackColor = Color.Yellow;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #endregion

            }
            #endregion
            #region CRMT
            else if (CrossMovement == true)
            {
                #region Player1
                #region Right
                if (e.KeyCode == Keys.Right)
                {
                    P1Right = false;
                    //KEN.BackColor = Color.DarkRed;
                    KEN.Image = StreetFighter.Properties.Resources.GifKenLeftIdle;
                    KEN.BackgroundImageLayout = ImageLayout.Stretch;
                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.Left)
                {
                    P1Left = false;
                    // KEN.BackColor = Color.DarkRed;
                    KEN.Image = StreetFighter.Properties.Resources.GifKenLeftIdle;
                    KEN.BackgroundImageLayout = ImageLayout.Stretch;
                }
                #endregion
                #region UP
                if (e.KeyCode == Keys.Up)
                {
                    if (P1Right == true)
                    {
                        // KEN.BackColor = Color.DarkMagenta;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (P1Left == true)
                    {
                        // KEN.BackColor = Color.DarkBlue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.A)
                {
                    if (P1Left != true && P1Right != true)
                    {
                        P1Punch = false;
                        // KEN.BackColor = Color.DarkRed;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftIdle;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P1Left == true)
                    {
                        P1Punch = false;
                        //  KEN.BackColor = Color.DarkBlue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P1Right == true)
                    {
                        P1Punch = false;
                        // KEN.BackColor = Color.DarkMagenta;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #region Kick

                if (e.KeyCode == Keys.S)
                {
                    if (P1Left != true && P1Right != true)
                    {
                        P1Kick = false;
                        // KEN.BackColor = Color.DarkRed;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftIdle;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P1Left == true)
                    {
                        P1Kick = false;
                        //KEN.BackColor = Color.DarkBlue;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P1Right == true)
                    {
                        P1Kick = false;
                        // KEN.BackColor = Color.DarkMagenta;
                        KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #endregion
                #region Player2
                #region Right
                if (e.KeyCode == Keys.L)
                {
                    P2Right = false;
                    // RYU.BackColor = Color.DarkRed;
                    RYU.Image = StreetFighter.Properties.Resources.GifRyuRightIdle;
                    RYU.BackgroundImageLayout = ImageLayout.Stretch;

                }
                #endregion
                #region Left
                if (e.KeyCode == Keys.J)
                {
                    P2Left = false;
                    //  RYU.BackColor = Color.DarkRed;
                    RYU.Image = StreetFighter.Properties.Resources.GifRyuRightIdle;
                    RYU.BackgroundImageLayout = ImageLayout.Stretch;
                }
                #endregion
                #region UP
                if (e.KeyCode == Keys.I)
                {
                    if (P2Right == true)
                    {
                        //RYU.BackColor = Color.DarkMagenta;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (P2Left == true)
                    {
                        // RYU.BackColor = Color.DarkBlue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #region Punch
                if (e.KeyCode == Keys.B)
                {
                    if (P2Left != true && P2Right != true)
                    {
                        P2Punch = false;
                        //  RYU.BackColor = Color.DarkRed;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightIdle;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Left == true)
                    {
                        P2Punch = false;
                        // RYU.BackColor = Color.DarkBlue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    if (P2Right == true)
                    {
                        P2Punch = false;
                        //  RYU.BackColor = Color.DarkMagenta;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                }
                #endregion
                #region Kick

                if (e.KeyCode == Keys.N)
                {
                    if (P2Left != true && P2Right != true)
                    {
                        P2Kick = false;
                        // RYU.BackColor = Color.DarkRed;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightIdle;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P2Left == true)
                    {
                        P2Kick = false;
                        // RYU.BackColor = Color.DarkBlue;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                    }
                    if (P2Right == true)
                    {
                        P2Kick = false;
                        // RYU.BackColor = Color.DarkMagenta;
                        RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                #endregion
                #endregion
            }
            #endregion
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region
            #region CrossMovement
            if (KEN.Location.X > RYU.Location.X && RYU.Location.X < KEN.Location.X)
            {
                CrossMovement = true;
            }
            else
            {
                CrossMovement = false;
            }
            #endregion

            #region CRMF
            if (CrossMovement == false)
            {
                if (!(PBplayer2.Value < 5))
                {
                    #region Player1
                    #region Left
                    if (P1Left == true)
                    {
                        KEN.Left -= 5;
                    }
                    #endregion
                    #region Right
                    if (P1Right == true)
                    {
                        KEN.Left += 5;
                    }
                    #endregion
                    #region Punch
                    if (P1Punch == true)
                    {

                        //KEN.BackColor = Color.Black;
                        KEN.Image = StreetFighter.Properties.Resources.KenRightPunch;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        if (KEN.Bounds.IntersectsWith(RYU.Bounds))
                        {
                            try
                            {
                                PBplayer2.Value -= 5;
                                P1Punch = false;

                            }
                            catch
                            {
                                timer1.Stop();
                                System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.player_win);
                                golpe.Play();
                                MessageBox.Show("Player 1 Win");
                                PBplayer2.Value = 1;
                                Application.Restart();
                            }
                        }
                    }
                    #endregion
                    #region Kick
                    if (P1Kick == true)
                    {
                        //  KEN.BackColor = Color.White;
                        KEN.Image = StreetFighter.Properties.Resources.KenRightKick;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        if (KEN.Bounds.IntersectsWith(RYU.Bounds))
                        {
                            try
                            {
                                PBplayer2.Value -= 5;
                                P1Kick = false;
                            }
                            catch
                            {
                                timer1.Stop();
                                System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.player_win);
                                golpe.Play();
                                MessageBox.Show("Player 1 Win");
                                PBplayer2.Value = 1;
                                Application.Restart();
                            }
                        }
                    }
                    #endregion
                    #region Jump
                    if (P1jump == true)
                    {
                        Player1JumpCMF(KEN);
                        if (P1Left == true)
                            if (P1Left != true && P1Right != true && P1jump != true)
                            {
                                KEN.Image = StreetFighter.Properties.Resources.GifKenRightIdle;
                                KEN.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        if (P1jump == false && P1Right == true)
                        {
                            KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                            KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P1jump == false && P1Left == true)
                        {
                            KEN.Image = StreetFighter.Properties.Resources.GifKenRightWalkin;
                            KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    timer1.Stop();
                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.player_win);
                    golpe.Play();
                    MessageBox.Show("Player 1 Win");
                    PBplayer2.Value = 1;
                    Application.Restart();
                }
                if (!(PBplayer1.Value < 5))
                {
                    #region Player2
                    #region Left
                    if (P2Left == true)
                    {
                        RYU.Left -= 5;
                    }
                    #endregion
                    #region Right
                    if (P2Right == true)
                    {
                        RYU.Left += 5;
                    }
                    #endregion
                    #region Punch
                    if (P2Punch == true)
                    {

                        // RYU.BackColor = Color.Black;
                        RYU.Image = StreetFighter.Properties.Resources.RyuLeftPunch;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        if (RYU.Bounds.IntersectsWith(KEN.Bounds))
                        {
                            PBplayer1.Value -= 4;
                            P2Punch = false;
                        }
                    }
                    #endregion
                    #region Kick
                    if (P2Kick == true)
                    {
                        // RYU.BackColor = Color.White;
                        RYU.Image = StreetFighter.Properties.Resources.RyuLeftKick;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        if (RYU.Bounds.IntersectsWith(KEN.Bounds))
                        {
                            PBplayer1.Value -= 4;
                            P2Kick = false;
                        }
                    }
                    #endregion
                    #region Jump
                    if (P2jump == true)
                    {
                        Player2JumpCMF(RYU);
                        if (P2Left != true && P2Right != true && P2jump != true)
                        {
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftIdle;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P2jump == false && P2Right == true)
                        {
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P2jump == false && P2Left == true)
                        {
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuLeftWalkin;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Player2 Win");
                    PBplayer1.Value = 1;
                    System.Media.SoundPlayer golpe = new System.Media.SoundPlayer(StreetFighter.Properties.Resources.player_win);
                    golpe.Play();
                    Application.Restart();
                }

            }
            #endregion
            #region CRMT
            else if (CrossMovement == true)
            {
                if (!(PBplayer2.Value < 5))
                {
                    #region Player1
                    #region Left
                    if (P1Left == true)
                    {
                        KEN.Left -= 5;
                    }
                    #endregion
                    #region Right
                    if (P1Right == true)
                    {
                        KEN.Left += 5;
                    }
                    #endregion
                    #region Punch
                    if (P1Punch == true)
                    {

                        // KEN.BackColor = Color.Black;
                        KEN.Image = StreetFighter.Properties.Resources.KenLeftPunch;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        if (KEN.Bounds.IntersectsWith(RYU.Bounds))
                        {
                            PBplayer2.Value -= 4;
                            P1Punch = false;
                        }
                    }
                    #endregion
                    #region Kick
                    if (P1Kick == true)
                    {
                        //KEN.BackColor = Color.White;
                        KEN.Image = StreetFighter.Properties.Resources.KenLeftKick;
                        KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        if (KEN.Bounds.IntersectsWith(RYU.Bounds))
                        {
                            PBplayer2.Value -= 4;
                            P1Kick = false;
                        }
                    }
                    #endregion
                    #region Jump
                    if (P1jump == true)
                    {
                        Player1JumpCMT(KEN);
                        if (P1Left == true)
                        {
                            //  KEN.BackColor = Color.DarkGreen;
                        }
                        if (P1Right == true)
                        {
                            // KEN.BackColor = Color.DarkGreen;
                        }
                        if (P1Left != true && P1Right != true && P1jump != true)
                        {
                            //  KEN.BackColor = Color.DarkRed;
                            KEN.Image = StreetFighter.Properties.Resources.GifKenLeftIdle;
                            KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P1jump == false && P1Right == true)
                        {
                            //KEN.BackColor = Color.DarkMagenta;
                            KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                            KEN.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P1jump == false && P1Left == true)
                        {
                            //KEN.BackColor = Color.DarkBlue;
                            KEN.Image = StreetFighter.Properties.Resources.GifKenLeftWalkin;
                            KEN.BackgroundImageLayout = ImageLayout.Stretch;

                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Player1 Win");
                    PBplayer2.Value = 1;
                    Application.Restart();
                }
                if (!(PBplayer2.Value < 5))
                {
                    #region Player2
                    #region Left
                    if (P2Left == true)
                    {
                        RYU.Left -= 5;
                    }
                    #endregion
                    #region Right
                    if (P2Right == true)
                    {
                        RYU.Left += 5;
                    }
                    #endregion
                    #region Punch
                    if (P2Punch == true)
                    {

                        //RYU.BackColor = Color.Black;
                        RYU.Image = StreetFighter.Properties.Resources.RyuRightPunch;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        if (RYU.Bounds.IntersectsWith(KEN.Bounds))
                        {
                            try
                            {
                                PBplayer1.Value -= 6;
                                P2Punch = false;
                            }
                            catch
                            {
                                timer1.Stop();
                                MessageBox.Show("Player2 Win");
                                PBplayer1.Value = 1;
                                Application.Restart();
                            }
                        }
                    }
                    #endregion
                    #region Kick
                    if (P2Kick == true)
                    {
                        //  RYU.BackColor = Color.White;
                        RYU.Image = StreetFighter.Properties.Resources.RyuRightKick;
                        RYU.BackgroundImageLayout = ImageLayout.Stretch;

                        if (RYU.Bounds.IntersectsWith(RYU.Bounds))
                        {

                            PBplayer1.Value -= 5;
                            P2Kick = false;

                        }
                    }
                    #endregion
                    #region Jump
                    if (P2jump == true)
                    {
                        Player2JumpCMT(RYU);

                        if (P2Left != true && P2Right != true && P2jump != true)
                        {
                            //  RYU.BackColor = Color.DarkRed;
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuRightIdle;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P2jump == false && P2Right == true)
                        {
                            //  RYU.BackColor = Color.DarkMagenta;
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        if (P2jump == false && P2Left == true)
                        {
                            // RYU.BackColor = Color.DarkBlue;
                            RYU.Image = StreetFighter.Properties.Resources.GifRyuRightWalkin;
                            RYU.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Player2 Win");
                    PBplayer1.Value = 1;
                    Application.Restart();
                }

            }
            #endregion
            #endregion
        }
        #region PlayerJump1CMFalse
        void Player1JumpCMF(PictureBox Player)
        {
            // Player.BackColor = Color.Green;
            P1JumpCounter++;
            if (P1JumpCounter <= 10)
            {
                Player.Location = new Point(Player.Location.X, KEN.Location.Y - 6);
                Player.Image = StreetFighter.Properties.Resources.KenRightJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P1JumpCounter > 10 && P1JumpCounter <= 15)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 4);
                Player.Image = StreetFighter.Properties.Resources.KenRightJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P1JumpCounter > 15 && P1JumpCounter <= 20)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 4);
                Player.Image = StreetFighter.Properties.Resources.KenRightJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P1JumpCounter > 20 && P1JumpCounter <= 30)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 6);
                Player.Image = StreetFighter.Properties.Resources.KenRightJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                P1jump = false;
                P1JumpCounter = 0;
                //  Player.BackColor = Color.Red;
            }
        }
        #endregion
        #region PlayerJump2CMFalse
        void Player2JumpCMF(PictureBox Player)
        {
            //  Player.BackColor = Color.Green;
            P2JumpCounter++;
            if (P2JumpCounter <= 10)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 6);
                Player.Image = StreetFighter.Properties.Resources.RyuLeftJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P2JumpCounter > 10 && P2JumpCounter <= 15)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 4);
                Player.Image = StreetFighter.Properties.Resources.RyuLeftJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;

            }
            else if (P2JumpCounter > 15 && P2JumpCounter <= 20)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 4);
                Player.Image = StreetFighter.Properties.Resources.RyuLeftJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P2JumpCounter > 20 && P2JumpCounter <= 30)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 6);
                Player.Image = StreetFighter.Properties.Resources.RyuLeftJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                P2jump = false;
                P2JumpCounter = 0;
                //    Player.BackColor = Color.Red;
            }
        }
        #endregion

        #region PlayerJump1CMTrue
        void Player1JumpCMT(PictureBox Player)
        {
            // Player.BackColor = Color.DarkGreen;
            P1JumpCounter++;
            if (P1JumpCounter <= 10)
            {
                Player.Location = new Point(Player.Location.X, KEN.Location.Y - 6);
                Player.Image = StreetFighter.Properties.Resources.KenLeftJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P1JumpCounter > 10 && P1JumpCounter <= 15)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 4);
                Player.Image = StreetFighter.Properties.Resources.KenLeftJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }

            else if (P1JumpCounter > 15 && P1JumpCounter <= 20)

            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 4);
                Player.Image = StreetFighter.Properties.Resources.KenLeftJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;

            }
            else if (P1JumpCounter > 20 && P1JumpCounter <= 30)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 6);
                Player.Image = StreetFighter.Properties.Resources.KenLeftJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                P1jump = false;
                P1JumpCounter = 0;
                // Player.BackColor = Color.DarkRed;
            }
        }
        #endregion
        #region PlayerJump2CMTrue
        void Player2JumpCMT(PictureBox Player)
        {
            // Player.BackColor = Color.DarkGreen;
            P2JumpCounter++;
            if (P2JumpCounter <= 10)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 6);
                Player.Image = StreetFighter.Properties.Resources.RyuRightJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P2JumpCounter > 10 && P2JumpCounter <= 15)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y - 4);
                Player.Image = StreetFighter.Properties.Resources.RyuRightJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P2JumpCounter > 15 && P2JumpCounter <= 20)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 4);
                Player.Image = StreetFighter.Properties.Resources.RyuRightJump2;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (P2JumpCounter > 20 && P2JumpCounter <= 30)
            {
                Player.Location = new Point(Player.Location.X, Player.Location.Y + 6);
                Player.Image = StreetFighter.Properties.Resources.RyuRightJump1;
                Player.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                P2jump = false;
                P2JumpCounter = 0;
                // Player.BackColor = Color.DarkRed;
            }
            #endregion
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Environment.Exit(1);
            //this.Dispose();
            //CerrarAplicacion();
        }

        private void CerrarAplicacion()
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // Use this since we are a WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Use this since we are a console app
                System.Environment.Exit(1);
            }
        }
    }
}

namespace MicroLibrary
{
    /// <summary>
    /// MicroStopwatch class
    /// </summary>
    public class MicroStopwatch : System.Diagnostics.Stopwatch
    {
        readonly double _microSecPerTick =
            1000000D / System.Diagnostics.Stopwatch.Frequency;

        public MicroStopwatch()
        {
            if (!System.Diagnostics.Stopwatch.IsHighResolution)
            {
                throw new Exception("On this system the high-resolution " +
                                    "performance counter is not available");
            }
        }

        public long ElapsedMicroseconds
        {
            get
            {
                return (long)(ElapsedTicks * _microSecPerTick);
            }
        }
    }

    /// <summary>
    /// MicroTimer class
    /// </summary>
    public class MicroTimer
    {
        public delegate void MicroTimerElapsedEventHandler(
                             object sender,
                             MicroTimerEventArgs timerEventArgs);
        public event MicroTimerElapsedEventHandler MicroTimerElapsed;

        System.Threading.Thread _threadTimer = null;
        long _ignoreEventIfLateBy = long.MaxValue;
        long _timerIntervalInMicroSec = 0;
        bool _stopTimer = true;

        public MicroTimer()
        {
        }

        public MicroTimer(long timerIntervalInMicroseconds)
        {
            Interval = timerIntervalInMicroseconds;
        }

        public long Interval
        {
            get
            {
                return System.Threading.Interlocked.Read(
                    ref _timerIntervalInMicroSec);
            }
            set
            {
                System.Threading.Interlocked.Exchange(
                    ref _timerIntervalInMicroSec, value);
            }
        }

        public long IgnoreEventIfLateBy
        {
            get
            {
                return System.Threading.Interlocked.Read(
                    ref _ignoreEventIfLateBy);
            }
            set
            {
                System.Threading.Interlocked.Exchange(
                    ref _ignoreEventIfLateBy, value <= 0 ? long.MaxValue : value);
            }
        }

        public bool Enabled
        {
            set
            {
                if (value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
            get
            {
                return (_threadTimer != null && _threadTimer.IsAlive);
            }
        }

        public void Start()
        {
            if (Enabled || Interval <= 0)
            {
                return;
            }

            _stopTimer = false;

            System.Threading.ThreadStart threadStart = delegate ()
            {
                NotificationTimer(ref _timerIntervalInMicroSec,
                                  ref _ignoreEventIfLateBy,
                                  ref _stopTimer);
            };

            _threadTimer = new System.Threading.Thread(threadStart);
            _threadTimer.Priority = System.Threading.ThreadPriority.Highest;
            _threadTimer.Start();
        }

        public void Stop()
        {
            _stopTimer = true;
        }

        public void StopAndWait()
        {
            StopAndWait(System.Threading.Timeout.Infinite);
        }

        public bool StopAndWait(int timeoutInMilliSec)
        {
            _stopTimer = true;

            if (!Enabled || _threadTimer.ManagedThreadId ==
                System.Threading.Thread.CurrentThread.ManagedThreadId)
            {
                return true;
            }

            return _threadTimer.Join(timeoutInMilliSec);
        }

        public void Abort()
        {
            _stopTimer = true;

            if (Enabled)
            {
                _threadTimer.Abort();
            }
        }

        void NotificationTimer(ref long timerIntervalInMicroSec,
                               ref long ignoreEventIfLateBy,
                               ref bool stopTimer)
        {
            int timerCount = 0;
            long nextNotification = 0;

            MicroStopwatch microStopwatch = new MicroStopwatch();
            microStopwatch.Start();

            while (!stopTimer)
            {
                long callbackFunctionExecutionTime =
                    microStopwatch.ElapsedMicroseconds - nextNotification;

                long timerIntervalInMicroSecCurrent =
                    System.Threading.Interlocked.Read(ref timerIntervalInMicroSec);
                long ignoreEventIfLateByCurrent =
                    System.Threading.Interlocked.Read(ref ignoreEventIfLateBy);

                nextNotification += timerIntervalInMicroSecCurrent;
                timerCount++;
                long elapsedMicroseconds = 0;

                while ((elapsedMicroseconds = microStopwatch.ElapsedMicroseconds)
                        < nextNotification)
                {
                    System.Threading.Thread.SpinWait(10);
                }

                long timerLateBy = elapsedMicroseconds - nextNotification;

                if (timerLateBy >= ignoreEventIfLateByCurrent)
                {
                    continue;
                }

                MicroTimerEventArgs microTimerEventArgs =
                     new MicroTimerEventArgs(timerCount,
                                             elapsedMicroseconds,
                                             timerLateBy,
                                             callbackFunctionExecutionTime);
                MicroTimerElapsed(this, microTimerEventArgs);
            }

            microStopwatch.Stop();
        }
    }

    /// <summary>
    /// MicroTimer Event Argument class
    /// </summary>
    public class MicroTimerEventArgs : EventArgs
    {
        // Simple counter, number times timed event (callback function) executed
        public int TimerCount { get; private set; }

        // Time when timed event was called since timer started
        public long ElapsedMicroseconds { get; private set; }

        // How late the timer was compared to when it should have been called
        public long TimerLateBy { get; private set; }

        // Time it took to execute previous call to callback function (OnTimedEvent)
        public long CallbackFunctionExecutionTime { get; private set; }

        public MicroTimerEventArgs(int timerCount,
                                   long elapsedMicroseconds,
                                   long timerLateBy,
                                   long callbackFunctionExecutionTime)
        {
            TimerCount = timerCount;
            ElapsedMicroseconds = elapsedMicroseconds;
            TimerLateBy = timerLateBy;
            CallbackFunctionExecutionTime = callbackFunctionExecutionTime;
        }

    }
}
