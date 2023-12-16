using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Heart : DrawableVars, IDrawable
    {
        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);

            // Пропорциональное изменение координат и размера
            float heartWidth = vars.width;
            float heartHeight = vars.height;
            float centerX = vars.x + vars.width / 2f;
            float centerY = vars.y + vars.height / 2f;

            graphics.DrawBezier(pen,
                centerX, centerY - heartHeight / 3.5f,
                centerX + heartWidth * 0.2f, vars.y,
                vars.x + heartWidth, centerY - heartHeight * 0.25f,
                centerX, vars.y + heartHeight);
            graphics.DrawBezier(pen,
                centerX, centerY - heartHeight / 3.5f,
                centerX - heartWidth * 0.2f, vars.y,
                vars.x, centerY - heartHeight * 0.25f,
                centerX, vars.y + heartHeight);
        }

        public void Draw(Graphics graphics, Brush brush, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);

            // Пропорциональное изменение координат и размера
            float heartWidth = vars.width;
            float heartHeight = vars.height;
            float centerX = vars.x + vars.width / 2f;
            float centerY = vars.y + vars.height / 2f;

            GraphicsPath graphicsPath = new GraphicsPath();

            PointF point1 = new PointF(centerX, centerY - heartHeight / 3.5f);
            PointF point2 = new PointF(centerX + heartWidth * 0.2f, vars.y);
            PointF point3 = new PointF(vars.x + heartWidth, centerY - heartHeight * 0.25f);
            PointF point4 = new PointF(centerX, vars.y + heartHeight);

            PointF point5 = new PointF(centerX, centerY - heartHeight / 3.5f); 
            PointF point6 = new PointF(centerX - heartWidth * 0.2f, vars.y); 
            PointF point7 = new PointF(vars.x, centerY - heartHeight * 0.25f); 
            PointF point8 = new PointF(centerX, vars.y + heartHeight); 

            graphicsPath.AddBezier(point1, point2, point3, point4);
            graphicsPath.AddBezier(point5, point6, point7, point8);

            graphics.FillPath(brush, graphicsPath);

            graphics.DrawBezier(pen, point1, point2, point3, point4);
            graphics.DrawBezier(pen, point5, point6, point7, point8);
        }


        public IDrawable GetInstance()
        {
            return new Heart();
        }
    }



}
