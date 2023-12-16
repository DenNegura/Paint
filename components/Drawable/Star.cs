using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Star : DrawableVars, IDrawable
    {
        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);

            // Определение координат точек для пятиконечной звезды
            Point[] starPoints = new Point[10];
            double angle = -Math.PI / 2; // Начальный угол

            for (int i = 0; i < 10; i++)
            {
                double factor = (i % 2 == 0) ? 1 : 0.5; // Изменение длины лучей чередуется
                starPoints[i] = new Point(
                    vars.x + (int)(vars.width / 2 + factor * vars.width / 2 * Math.Cos(angle)),
                    vars.y + (int)(vars.height / 2 + factor * vars.height / 2 * Math.Sin(angle))
                );

                angle += Math.PI / 5; // Угол между лучами звезды
            }

            graphics.DrawPolygon(pen, starPoints);
        }

        public void Draw(Graphics graphics, Brush brush, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);

            // Определение координат точек для пятиконечной звезды
            Point[] starPoints = new Point[10];
            double angle = -Math.PI / 2; // Начальный угол

            for (int i = 0; i < 10; i++)
            {
                double factor = (i % 2 == 0) ? 1 : 0.5; // Изменение длины лучей чередуется
                starPoints[i] = new Point(
                    vars.x + (int)(vars.width / 2 + factor * vars.width / 2 * Math.Cos(angle)),
                    vars.y + (int)(vars.height / 2 + factor * vars.height / 2 * Math.Sin(angle))
                );

                angle += Math.PI / 5; // Угол между лучами звезды
            }

            graphics.FillPolygon(brush, starPoints);
            graphics.DrawPolygon(pen, starPoints);
        }

        public IDrawable GetInstance()
        {
            return new Star();
        }
    }



}
