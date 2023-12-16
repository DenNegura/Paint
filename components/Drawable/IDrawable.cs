using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    public interface IDrawable
    {
        IDrawable GetInstance();

        void Draw(Graphics graphics, Brush brush, Pen pen, Point pos1, Point pos2);

        void Draw(Graphics graphics, Pen pen, Point pos1, Point pos2);
    }
}
