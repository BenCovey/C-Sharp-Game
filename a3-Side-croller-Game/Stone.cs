using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Stone

    {
        Random rnd = new Random();//Random for width of stone
        Image image = Image.FromFile("stone.png");
        private readonly int stoneHeight = 40;
        public Rectangle stoneDisplayArea;
        private Rectangle gameplayArea;

        public int Pos()
        {
            return stoneDisplayArea.X;
        }

        public Stone(Rectangle gameplayArea)
        {
            stoneDisplayArea.Height = stoneHeight;
            stoneDisplayArea.Width = rnd.Next(50, 60);//Random for width of rock
            this.gameplayArea = gameplayArea;

            stoneDisplayArea.Y = gameplayArea.Bottom - stoneHeight - 80;
            stoneDisplayArea.X = gameplayArea.Right;

        }
        //Draw stone with image
        internal void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, stoneDisplayArea);
        }

        public void Move()
        {
            stoneDisplayArea.X -= 10;
        }
    }
}
