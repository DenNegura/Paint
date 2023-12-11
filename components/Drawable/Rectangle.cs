using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Rectangle : IDrawable
    {
        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
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
            graphics.DrawRectangle(pen, x, y, width, height);
        }

        public IDrawable getInstance()
        {
            return new Rectangle();
        }
    }
}
