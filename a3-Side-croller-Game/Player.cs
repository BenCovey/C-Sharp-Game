using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Player
        { 
        private readonly int playerHeight = 200;
        private readonly int playerWidth = 120;
        Image image;
        public Rectangle playerDisplayArea;
        public Rectangle gameplayArea;


        public Player(Rectangle gameplayArea)
        {
            playerDisplayArea.Height = playerHeight;
            playerDisplayArea.Width = playerWidth;
            this.gameplayArea = gameplayArea;

            playerDisplayArea.Y = gameplayArea.Height - 290;
            playerDisplayArea.X = 250;

        }

        internal void Draw(Graphics graphics)
        {
            image = Image.FromFile("stick-figure.png");
            graphics.DrawImage(image, playerDisplayArea);
        }

        public void Jump()
        {
            playerDisplayArea.Y -= 20;
        }

        public void Fall()
        {
            playerDisplayArea.Y += 20;
        }

        public void Move()
        {
            playerDisplayArea.X += 10;
        }
            
    }
}
