using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    internal class Snake
    {
       public int Length { get; private set; }
       public Point[] Location { get; private set; }

        public Snake()
        {
            Location = new Point[28 * 28];
            Reset();
        }

        public void Reset()
        {
            Length = 5;
            for(int i = 0;i < Length; i++)
            {
                Location[i].X = 12;
                Location[i].Y = 12;
            }
        }

        public void Follow()
        {
            for(int i = Length -1;i > 0; i--)
            {
                Location[i] = Location[i - 1];
            }
        }


        public void Up()
        {
            Follow();
            Location[0].Y--;
            if (Location[0].Y < 0)
            {
                Location[0].Y += 24;
            }
        }
        public void Down()
        {
            Follow();
            Location[0].Y++;
            if (Location[0].Y > 24)
            {
                Location[0].Y -= 27;
            }
        }
        public void Left()
        {
            Follow();
            Location[0].X--;
            if (Location[0].X < 0)
            {
                Location[0].X += 20;
            }
        }
        public void Right()
        {
            Follow();
            Location[0].X++;
            if (Location[0].X > 20)
            {
                Location[0].X -= 20;
            }
        }

        public void Eat()
        {
            playEatSound();
            Length++;
        }

        private void playEatSound()
        {
            SoundPlayer playSimpleSound = new SoundPlayer(Properties.Resources.comer);
            playSimpleSound.Play();
        }
    }
}
