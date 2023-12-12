using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Polyline : IDrawable
    {
        List<Point> points;

        public Polyline()
        {
            points = new List<Point>();
        }

        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
        {
            points.Add(pos2);
            if (points.Count > 1) 
            {
                graphics.DrawLines(pen, points.ToArray());
            }
        }

        public IDrawable GetInstance()
        {
            return new Polyline();
        }
    }
}
