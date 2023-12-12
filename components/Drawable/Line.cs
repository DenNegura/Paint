using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Line : IDrawable
    {
        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
        {
            graphics.DrawLine(pen, pos1, pos2);
        }

        public IDrawable GetInstance()
        {
            return new Line();
        }
    }
}
