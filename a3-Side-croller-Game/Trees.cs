using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace a3_Side_croller_Game
{
    class Trees
    {
        private readonly int treeHeight = 600;
        private readonly int treeWidth = 360;
        Image image = Image.FromFile("tree.png");
        public Rectangle treeDisplayArea;
        public Rectangle gameplayArea;

        public int Pos()
        {
            return treeDisplayArea.X;
        }
        
        public Trees(Rectangle gameplayArea)
        {
            treeDisplayArea.Height = treeHeight;
            treeDisplayArea.Width = treeWidth;
            this.gameplayArea = gameplayArea;

            treeDisplayArea.Y = gameplayArea.Height - 660;
            treeDisplayArea.X = gameplayArea.Right;

        }

        internal void Draw(Graphics graphics)
        {
            
            graphics.DrawImage(image, treeDisplayArea);
        }

        public void Move()
        {
            treeDisplayArea.X -= 10;
        }


    }
}
