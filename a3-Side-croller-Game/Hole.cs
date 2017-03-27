using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Hole
    {
        Random rnd = new Random();//Randm for sizing
        public int distance;
        private readonly int holeHeight = 100;
        public Rectangle holeDisplayArea;
        private Rectangle gameplayArea;

        public int Pos()
        {
            return holeDisplayArea.X;
        }
        //Create a new hole with a random width
        public Hole(Rectangle gameplayArea)
        {
            distance = rnd.Next(100, 250);
            holeDisplayArea.Height = holeHeight;
            holeDisplayArea.Width = rnd.Next(75,150);
            this.gameplayArea = gameplayArea;

            holeDisplayArea.Y = gameplayArea.Height - holeHeight;
            holeDisplayArea.X = gameplayArea.Right;

        }
        //Draw hole with colour
        internal void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.SaddleBrown);
            graphics.FillRectangle(brush, holeDisplayArea);
        }

        public void Move()
        {
            holeDisplayArea.X -= 10;
        }


    }
}
