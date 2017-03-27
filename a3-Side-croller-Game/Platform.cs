using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Platform
    {
        private readonly int platformHeight = 100;
        public Rectangle platformDisplayArea;
        private Rectangle gameplayArea;



        public Platform(Rectangle gameplayArea)
        {
            platformDisplayArea.Height = platformHeight;
            platformDisplayArea.Width = gameplayArea.Width;
            this.gameplayArea = gameplayArea;

            platformDisplayArea.Y = gameplayArea.Bottom - 100;
            platformDisplayArea.X = 0;

        }

        internal void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.LawnGreen);
            graphics.FillRectangle(brush, platformDisplayArea);
        }
    }
}
