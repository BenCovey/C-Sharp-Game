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
        Platform platform;
        Player player;
        Level level;
        //Hole hole;
        int Score;
        int addTree = 0;
        int addHole = 0;
        int addStone = 0;
        bool gameover = false;
        bool start = true;
        HashSet<Stone> stones = new HashSet<Stone>();
        HashSet<Trees> trees = new HashSet<Trees>();
        HashSet<Hole> holes = new HashSet<Hole>();
        private bool jumping;
        Rectangle windowSize;
        private int jumpheight = 0;
        private bool falling;
        private bool hitrock;
        private bool won;
        public Form1()
        {
            InitializeComponent();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (Score == 1)
            {
                timer.Stop();
                timer1.Stop();
                timer2.Start();
                trees.Clear();
                holes.Clear();
                stones.Clear();
                won = true;
                Invalidate();

            }
            if (Score <= 10)
            {
                if (addStone == rnd.Next(40, 90))
                {
                    addStone = 0;
                    stones.Add(new Stone(this.DisplayRectangle));
                }
                else
                {
                    if (addStone > 90)
                    {
                        addStone = 0;
                    }
                    addStone++;
                }
            }

            if (player.playerDisplayArea.Y >= player.gameplayArea.Bottom)
            {
                timer.Stop();
                timer1.Stop();
                trees.Clear();
                holes.Clear();
                stones.Clear();
                gameover = true;
                Invalidate();
                }
                if (falling == true)
                {
                    player.Fall();
                }
                if (hitrock == true)
                {
                    //player.Fall();
                }
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

        private void DisplayScoreCount(Graphics graphics)
        {
            Font font = new Font("Arial", 32);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            Point point = new Point(20, 20);
            string Message = String.Format("Score: " + Score + "\nPress 'p' to Play/Pause");
            //do the draw
            graphics.DrawString(Message, font, brush, point);
        }

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
                Application.Restart();
            }
        }

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

        private bool HolesOffScreen(Hole hole)
        {
            if (hole.holeDisplayArea.Right <= DisplayRectangle.Left)
            {
                Score++;
            }

            return hole.holeDisplayArea.Right <= DisplayRectangle.Left;
        }
        private bool StonesOffScreen(Stone stone)
        {
            if (stone.stoneDisplayArea.Right <= DisplayRectangle.Left)
            {
                Score++;
            }

            return stone.stoneDisplayArea.Right <= DisplayRectangle.Left;
        }
        private bool TreesOffScreen(Trees tree)
        {
            if (tree.treeDisplayArea.Right <= DisplayRectangle.Left)
            {
                //Score++;
            }

            return tree.treeDisplayArea.Right <= DisplayRectangle.Left;
        }
        private bool StonesTouchHoles(Stone stone)
        {
            foreach (Hole hole in holes)
            {
                return stone.stoneDisplayArea.IntersectsWith(hole.holeDisplayArea) ;
            }
            return false;
        }

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

