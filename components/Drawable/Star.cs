using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Star : IDrawable
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

            // Определение координат точек для пятиконечной звезды
            Point[] starPoints = new Point[10];
            double angle = -Math.PI / 2; // Начальный угол

            for (int i = 0; i < 10; i++)
            {
                double factor = (i % 2 == 0) ? 1 : 0.5; // Изменение длины лучей чередуется
                starPoints[i] = new Point(
                    x + (int)(width / 2 + factor * width / 2 * Math.Cos(angle)),
                    y + (int)(height / 2 + factor * height / 2 * Math.Sin(angle))
                );

                angle += Math.PI / 5; // Угол между лучами звезды
            }

            graphics.DrawPolygon(pen, starPoints);
        }

        public IDrawable GetInstance()
        {
            return new Star();
        }
    }



}
