﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Ellipse : DrawableVars, IDrawable
    {
        public void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);
            graphics.DrawEllipse(pen, vars.x, vars.y, vars.width, vars.height);
        }

        public void Draw(Graphics graphics, Brush brush, Pen pen, Point pos1, Point pos2)
        {
            Vars vars = CalculateVars(pos1, pos2);
            graphics.FillEllipse(brush, vars.x, vars.y, vars.width, vars.height);
            graphics.DrawEllipse(pen, vars.x, vars.y, vars.width, vars.height);
        }

        public IDrawable GetInstance()
        {
            return new Ellipse();
        }
    }
}
