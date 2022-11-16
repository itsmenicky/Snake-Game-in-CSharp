using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    internal class Game
    {

        public Keys Direction { get; set; }
        public Keys Arrow { get; set; }

        private Timer Frame { get; set; }

        private Label lbpontos { get; set; }

        private Panel pntela { get; set; }

        private int pontos = 0;

        private Food Food;

        private Snake Snake;

        private Bitmap offScreenBitmap;

        private Graphics bitmapGraph;

        private Graphics screenGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            pntela = panel;
            Frame = timer;
            lbpontos = label;
            offScreenBitmap = new Bitmap(428, 428);
            Snake = new Snake();
            Food = new Food();
            Direction = Keys.Left;
            Arrow = Direction;
        }

        public void StartGame()
        {
            Snake.Reset();
            Food.CreateFood();
            Direction = Keys.Left;
            bitmapGraph = Graphics.FromImage(offScreenBitmap);
            screenGraph = pntela.CreateGraphics();
            Frame.Enabled = true;
            lbpontos.Text = "PONTOS: 0";
        }

        public void Tick()
        {
            if(((Arrow == Keys.Left) && (Direction != Keys.Right)||
                (Arrow == Keys.Right) && (Direction != Keys.Left))||
                (Arrow == Keys.Up) && (Direction != Keys.Down)||
                (Arrow == Keys.Down) && (Direction != Keys.Up)){
                Direction = Arrow;
            }

            switch (Direction)
            {
                case Keys.Left:
                    Snake.Left();
                    break;
                case Keys.Right:
                    Snake.Right();
                    break;
                case Keys.Up:
                    Snake.Up();
                    break;
                case Keys.Down:
                    Snake.Down();
                    break;
            }

            bitmapGraph.Clear(Color.White);
            bitmapGraph.DrawImage(Properties.Resources.apple_icon,(Food.Location.X*15), (Food.Location.Y * 15), 15, 15);

            bool gameOver = false;

            for(int i=0;i < Snake.Length; i++)
            {
                if(i == 0)
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }
                else
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#008000")), (Snake.Location[i].X * 15), (Snake.Location[i].Y * 15), 15, 15);
                }

                if ((Snake.Location[i] == Snake.Location[0]) && (i > 0))
                {
                    gameOver = true;
                }
            }

            screenGraph.DrawImage(offScreenBitmap, 0, 0);
            CheckCollision();
            if (gameOver)
            {
                GameOver();
            }
        }

        public void CheckCollision()
        {
            if (Snake.Location[0] == Food.Location)
            {
                Snake.Eat();
                Food.CreateFood();
                pontos += 1;
                lbpontos.Text = "PONTOS: " + pontos;
            }
        }

        public void GameOver()
        {
            SoundPlayer playSimpleSound = new SoundPlayer(Properties.Resources.GAMEOVER);
            playSimpleSound.Play();
            Frame.Enabled = false;
            bitmapGraph.Dispose();
            screenGraph.Dispose();
            lbpontos.Text = "";
            pontos = 0;
            MessageBox.Show("GAME OVER!");
        }

        public void playStartSound()
        {
            SoundPlayer playSimpleSound = new SoundPlayer(Properties.Resources.STARTSOUND);
            playSimpleSound.Play();
        }
    }
}
