using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal abstract class DrawableVars
    {

        protected struct Vars
        {
            public readonly int x;

            public readonly int y;

            public readonly int width;

            public readonly int height;

            public Vars(int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
        }

        protected Vars CalculateVars(Point pos1, Point pos2)
        {
            int x, y, width, height;
            if (pos2.X >= pos1.X)
            {
                x = pos1.X;
                width = pos2.X - pos1.X;
            }
            else
            {
                x = pos2.X;
                width = pos1.X - pos2.X;
            }
            if (pos2.Y >= pos1.Y)
            {
                y = pos1.Y;
                height = pos2.Y - pos1.Y;
            }
            else
            {
                y = pos2.Y;
                height = pos1.Y - pos2.Y;
            }
            return new Vars(x, y, width, height);
        }
    }
}
