﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FinkiManEscape
{
    abstract public class Figura
    {
        public int FiguraId { get; set; }

        public Rectangle rec;
        public Brush brush;
        public int Length { get; set; }//kolko kocke da zafati

        public int PositionX { get; set; }//pozicija kude pocnue figura(kolona)
        public int PositionY { set; get; }//pozicija kude pocnue figura(red)

        public int X { get; set; }
        public int Y { get; set; }

        public int Orinetation { get; set; }//ovoj e jasno :P
        public static readonly int PORTRAIT = 0;
        public static readonly int LANDSCAPE = 1;
        
        public int[] Bounds { set; get; }//Slobodni kocke bounds[0]:UP bounds[1]:DOWN...
        public static readonly int BOUNDUP = 0;
        public static readonly int BOUNDDOWN = 1;
        public static readonly int BOUNDLEFT = 0;
        public static readonly int BOUNDRIGHT = 1;

        public static int gap { set; get; }
        public static readonly int paddingX = 10;
        public static readonly int paddingY = 35;

        /// <summary>
        /// Konstruktor za Figura
        /// </summary>
        /// <param name="length">Dolzina na figurata (vo kocki)</param>
        /// <param name="positionX">X pozicija na figurata(vo odnos na kocki t.e. matricata 6x6)</param>
        /// <param name="positionY">Y pozicija na figurata(vo odnos na kocki t.e. matricata 6x6)</param>
        /// <param name="orientation">Horizontalna postava(Landscape) i Vertikalna(Portrait)</param>
        public Figura(int length, int positionX, int positionY, int orientation)
        {
            X = positionX * Game.squareDimension;
            Y = positionY * Game.squareDimension;
            int width, heigth;
            if (orientation == PORTRAIT)
            {
                heigth = length * Game.squareDimension;
                width = Game.squareDimension;
            }
            else
            {
                width = length * Game.squareDimension;
                heigth = Game.squareDimension;
            }
            gap = 4;
            rec = new Rectangle(X + gap + paddingX, Y + gap + paddingY, width - gap, heigth - gap);
            brush = new SolidBrush(Color.Aqua);
            Length = length;
            PositionX = positionX;
            PositionY = positionY;
            Orinetation = orientation;
            Bounds = new int[2];
        }

        /// <summary>
        /// Metoda za pomestuvanje na figura
        /// </summary>
        /// <param name="X">dX rastojanie za koe treba da se pomesti</param>
        /// <param name="Y">dY rastojanie za koe treba da se pomesti</param>
        /// <returns>true za validno pomestuvanje, false za nevalidno</returns>
        public bool move(int X, int Y)
        {
            if (Orinetation == PORTRAIT)
            {
                if (PositionY + Y < Bounds[BOUNDUP] || PositionY + Y + Length * Game.squareDimension > Bounds[BOUNDDOWN])
                {
                    return false;
                }
                PositionY += Y;//razlikaod poslednjo
                rec = new Rectangle(PositionX + gap + paddingX, PositionY + gap + paddingY, Game.squareDimension - gap, Length * Game.squareDimension - gap);
            }
            else
            {
                if (PositionX + X < Bounds[BOUNDLEFT] || PositionX + X + Length * Game.squareDimension > Bounds[BOUNDRIGHT])
                {
                    return false;
                }
                PositionX += X;
                rec = new Rectangle(PositionX + gap + paddingX, PositionY + gap + paddingY, Length * Game.squareDimension - gap, Game.squareDimension - gap);
            }
            return true;
        }
        /// <summary>
        /// Se povikuva pri smena goleminata na ekranot za da se inicijaliziraat novite vrednosti na figurite
        /// </summary>
        public void resize()
        {
            X = PositionX * Game.squareDimension;
            Y = PositionY * Game.squareDimension;
            int width, heigth;
            if (Orinetation == PORTRAIT)
            {
                heigth = Length * Game.squareDimension;
                width = Game.squareDimension;
            }
            else
            {
                width = Length * Game.squareDimension;
                heigth = Game.squareDimension;
            }
            rec = new Rectangle(X + gap + paddingX, Y + gap + paddingY, width - gap, heigth - gap);
            brush = new SolidBrush(Color.Aqua);
            Length = Length;
            PositionX = PositionX;
            PositionY = PositionY;
            Orinetation = Orinetation;
            Bounds = new int[2];
        }
        
        abstract public void draw(Graphics g);
        abstract public bool endGame();
       
    }
}
