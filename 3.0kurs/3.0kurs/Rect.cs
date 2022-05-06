﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({X},{Y}) {Width}x{Height}")]
    public class Rect
    {
        private int _width;
        private int _height;


        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width
        {
            get { return _width; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Width should be possitive number");

                _width = value;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Height should be possitive number");
                _height = value;
            }
        }

        public int Right
        {
            get { return X + Width - 1; }
        }

        public int Bottom
        {
            get { return Y + Height - 1; }
        }

        public void Inflate(int width, int height)
        {

            X -= width;
            Y -= height;
            Width += width * 2;
            Height += height * 2;
        }

        public void Inflate1(int width, int height)
        {

            X = (X - width) == -1 ? X : ((X + width) == 10 ? X  : X-= width);
            Y = (Y - height) == -1 ? Y : ((Y + height) == 10 ? Y : Y -= height);
            Width = (X - width) == -1 ? Width += (width * 2) - 1 : Width += width * 2;
            Height = (Y - height) == -1 ? Height += (height * 2) - 1 : Height += height * 2;
            //Height += height * 2 ;
        }

        public void InflateAndMiss(int width, int height)
        {
            X -= width;
            Y -= height;
            Width += width * 2;
            Height += height * 2;
            
        }

        public bool Contains(Rect rect)
        {
            return X <= rect.X && Y <= rect.Y
                && Right >= rect.Right && Bottom >= rect.Bottom;
        }

        public bool Contains(Point point)
        {
            return point.X >= X && point.X <= Right && point.Y >= Y && point.Y <= Bottom;
        }

        public bool IntersectsWith(Rect rect)
        {
            return !(
                X > rect.Right
                || Right < rect.X
                || Y > rect.Bottom
                || Bottom < rect.Y
            );
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IList<Point> GetPoints()
        {
            List<Point> points = new List<Point>();

            for (int x = X; x <= Right; x++)
            {
                for (int y = Y; y <= Bottom; y++)
                {
                    points.Add(new Point(x, y));
                }
            }

            return points;
        }
    }
}