using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace a3_Side_croller_Game
{
    public partial class Form1 : Form
    {
        //Objects for on screen
        Platform platform;
        Player player;
        Level level;
        HashSet<Stone> stones = new HashSet<Stone>();
        HashSet<Trees> trees = new HashSet<Trees>();
        HashSet<Hole> holes = new HashSet<Hole>();
        //Rectangle of window size
        Rectangle windowSize;
        //Integer variables for score, and randomly adding holes and stones
        //As well as consistently adding trees
        int Score;
        int addTree = 0;
        int addHole = 0;
        int addStone = 0;
        private int jumpheight = 0;
        bool gameover = false;
        //Booleans for states of different movements or game states
        bool start = true;
        private bool jumping;
        private bool falling;
        private bool hitrock;
        private bool won;
        public Form1()
        {
            InitializeComponent();

        }
        /*
         * Main Timer allows the game objects to move, spawn and check for collisions.
         */ 
        private void timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (Score == 25)//End game
            {
                timer.Stop();//Stop Timer for object movement
                timer1.Stop();//Stop Timer for jump
                timer2.Start();//Start timer for player movement
                trees.Clear();//Remove trees
                holes.Clear();//Remove holes
                stones.Clear();//Remove Stones
                won = true;//Set Win bool
                Invalidate();
            }
            //How I add stones randomly
            if (addStone == rnd.Next(40, 90))
            {
                //If stop is added reset counter
                addStone = 0;
                stones.Add(new Stone(this.DisplayRectangle));
            }
            else
            {
                //Make sure counter does not go above limit, and add to counter
                if (addStone > 90)
                {
                    addStone = 0;
                }
                addStone++;
            }
            
            //If Player hits bottom via falling or tripping on rock
            if (player.playerDisplayArea.Y >= player.gameplayArea.Bottom)
            {
                gameover = true;
                Invalidate();
            }
            //If Falling
            if (falling == true)
            {
                timer.Stop();
                player.Fall();
            }
            //If hit Rock
            if (hitrock == true)
            {
                player.Fall();
            }
            //How I randomly add holes
            if (addHole == rnd.Next(58, 100))
            {
                addHole = 0;
                holes.Add(new Hole(this.DisplayRectangle));
            } else
            {
                if (addHole > 100)
                {
                    addHole = 0;
                }
                addHole++;
            }
            //Adding trees consistently
            if (addTree == 100)
            {
                trees.Add(new Trees(this.DisplayRectangle));
                addTree = 0;
                //Score++;
            }
            else
            {
                addTree++;
            }
            //Move all objects
            foreach (Hole hole in holes)
            {

                hole.Move();
            }

            foreach (Trees tree in trees)
            {
                tree.Move();
            }

            foreach (Stone stone in stones)
            {
                stone.Move();
            }
            CheckCollisions();
            Invalidate();
        }
        /*
        * Check collisions with a safe way to remove objects that are either off
        * screen or touching other objects
        */
        private void CheckCollisions()
        {
            holes.RemoveWhere(HolesOffScreen);
            stones.RemoveWhere(StonesOffScreen);
            trees.RemoveWhere(TreesOffScreen);
            stones.RemoveWhere(StonesTouchHoles);
            
            foreach (Stone stone in stones)
            {
                if (player.playerDisplayArea.IntersectsWith(stone.stoneDisplayArea))
                {
                    hitrock = true;
                }     
            }
            foreach (Hole hole in holes)
            {
                if (player.playerDisplayArea.IntersectsWith(hole.holeDisplayArea))
                {
                    falling = true;
                }
            }
        }
        /*
         * Form load, create all needed objects for start up, plus maximize window
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            //make window maxmizewd
            this.WindowState = FormWindowState.Maximized;
            player = new Player(this.DisplayRectangle);
            windowSize = player.gameplayArea;
            platform = new Platform(this.DisplayRectangle);
            //hole = new Hole(this.DisplayRectangle);
            trees.Add(new Trees(this.DisplayRectangle));
        }
        /*
         * The Paint method draws all objects, and checks for booleans on whether
         * or not to draw certain objects, such as end game and start game text
         */
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (start)
            {
                DisplayGameStart(e.Graphics);
                start = false;
            }
            platform.Draw(e.Graphics);
            foreach (Trees tree in trees)
            {
                tree.Draw(e.Graphics);
            }
            
            DisplayScoreCount(e.Graphics);
            foreach(Hole hole in holes)
            {
                hole.Draw(e.Graphics);
            }
            foreach(Stone stone in stones)
            {
                stone.Draw(e.Graphics);
            }
            if (gameover == true)
            {
                DisplayGameOver(e.Graphics);
            }
            if(won == true)
            {
                DisplayEndGame(e.Graphics);
            }
            
            player.Draw(e.Graphics);
            
        }
        /*
         * Display Score + hint on pausing. This is always drawn even after game is over
         */
        private void DisplayScoreCount(Graphics graphics)
        {
            Font font = new Font("Arial", 32);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            Point point = new Point(20, 20);
            string Message = String.Format("Score: " + Score + "\nPress 'p' to Play/Pause");
            //do the draw
            graphics.DrawString(Message, font, brush, point);
        }
        /*
         * Display game over, the text that displays you lost and how to restart
         */
        private void DisplayGameOver(Graphics grahpics)
        {
            Font font = new Font("Arial", 64);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            string Message = "Game Over! Press 'r' to restart.";
            int x = 400;
            int y = windowSize.Bottom / 2;
            Point point = new Point(x, y);
            grahpics.DrawString(Message, font, brush, point);
        }
        /*
         * Display Game start text, it tells you how to play and your objective
         */
        private void DisplayGameStart(Graphics grahpics)
        {
            Font font = new Font("Arial", 64);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            string Message = "   Press P to start, and space to jump. \nAvoid Rocks and Holes. Get to 25 to win";
            int x = windowSize.Right / 2 - 800;
            int y = windowSize.Bottom / 2;
            Point point = new Point(x, y);
            grahpics.DrawString(Message, font, brush, point);
        }
        /*
         * Display End Game text, once you win this text tells you how to play a new game.
         */
        private void DisplayEndGame(Graphics graphics)
        {
            Font font = new Font("Arial", 64);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            string Message = "You won, press 'r' to play again";
            int x = 400;
            int y = windowSize.Bottom / 2;
            Point point = new Point(x, y);
            graphics.DrawString(Message, font, brush, point);
        }
        /*
         * Check keyboard press for pausing, restarting, and jumping
         */
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'p')
            { 
                //start or stop the timer
                if (timer.Enabled)
                {
                    timer.Stop();
                    timer1.Stop();
                }
                else
                {
                    timer.Start();
                    timer1.Start();
                }
            }else if(e.KeyChar == ' '){
                if (jumping != true)
                {
                    jumping = true;
                    jumpheight = 0;
                    player.Jump();
                }
            }else if(e.KeyChar == 'r' && gameover == true || e.KeyChar == 'r' && won == true)
            {   
                //Cleanest way to restart with so many booleans and ints for game states
                Application.Restart();
            }
        }
        /*
         * Second timer, dedicated to jumping, as the player jumps as a slower speed
         * than movement happens at.
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (jumping == true)
            {
                if (jumpheight >= 0 && jumpheight <= 7)
                {
                    player.Jump();
                    jumpheight++;
                }
                else if (jumpheight > 7 && jumpheight <= 16)
                {
                    player.Fall();
                    jumpheight++;
                }
                else { jumping = false; jumpheight = 0; }
            }
        }
        /*
         * Checking if holes are off screen ,if so delete and add to score
         */
        private bool HolesOffScreen(Hole hole)
        {
            if (hole.holeDisplayArea.Right <= DisplayRectangle.Left)
            {
                Score++;
            }

            return hole.holeDisplayArea.Right <= DisplayRectangle.Left;
        }
        /*
        * Checking if Stones are off screen ,if so delete and add to score
        */
        private bool StonesOffScreen(Stone stone)
        {
            if (stone.stoneDisplayArea.Right <= DisplayRectangle.Left)
            {
                Score++;
            }

            return stone.stoneDisplayArea.Right <= DisplayRectangle.Left;
        }
        /*
        * Checking if Trees are off screen ,if so delete do not add to score
        */
        private bool TreesOffScreen(Trees tree)
        {
            if (tree.treeDisplayArea.Right <= DisplayRectangle.Left)
            {
                //Score++;
            }

            return tree.treeDisplayArea.Right <= DisplayRectangle.Left;
        }
        /*
        * Checking if Stones and hole stouch ,if so delete. This sometimes bugs and
        * deletes as they are half way across the screen. I cannot figure out why.
        */
        private bool StonesTouchHoles(Stone stone)
        {
            foreach (Hole hole in holes)
            {
                return stone.stoneDisplayArea.IntersectsWith(hole.holeDisplayArea) ;
            }
            return false;
        }
        /*
        * Third and final timer, dedicated to player movement at end game
        */
        private void timer2_Tick(object sender, EventArgs e)
        {
            won = true;
           
            if(player.playerDisplayArea.Left > windowSize.Right)
            {
                timer2.Stop();
            }
            else
            {
                player.Move();
            }
            Invalidate();
        }
    }
}

