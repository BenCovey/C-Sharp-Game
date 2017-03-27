using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Level
    {
        Random rnd = new Random();
        public int distance;
        private readonly int levelHeight = 30;
        public Rectangle levelDisplayArea;
        private Rectangle gameplayArea;

        public int Pos()
        {
            return levelDisplayArea.X;
        }

        public Level(Rectangle gameplayArea)
        {
            levelDisplayArea.Height = levelHeight;
            levelDisplayArea.Width = rnd.Next(200, 450);
            this.gameplayArea = gameplayArea;

            levelDisplayArea.Y = gameplayArea.Height - 200;
            levelDisplayArea.X = gameplayArea.Right;

        }

        internal void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.LawnGreen);
            graphics.FillRectangle(brush, levelDisplayArea);
        }

        public void Move()
        {
            levelDisplayArea.X -= 10;
        }

    }
}
