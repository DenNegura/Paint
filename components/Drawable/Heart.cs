using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Heart : IDrawable
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

            // Пропорциональное изменение координат и размера
            float heartWidth = width;
            float heartHeight = height;
            float centerX = x + width / 2f;
            float centerY = y + height / 2f;

            graphics.DrawBezier(pen,
                centerX, centerY - heartHeight / 3.5f,
                centerX + heartWidth * 0.2f, y,
                x + heartWidth, centerY - heartHeight * 0.25f,
                centerX, y + heartHeight);
            graphics.DrawBezier(pen,
                centerX, centerY - heartHeight / 3.5f,
                centerX - heartWidth * 0.2f, y,
                x, centerY - heartHeight * 0.25f,
                centerX, y + heartHeight);
        }

        public IDrawable GetInstance()
        {
            return new Heart();
        }
    }



}
