using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Figurs
{
    internal class FSircle: Figure
    {
        public FSircle(Bitmap bitmap, Point mousePosition) :
            base(bitmap, mousePosition)
        { }


        override public Bitmap Draw(Bitmap bitmap, Pen pen, Point mousePosition)
        {
            return Draw(bitmap, pen, mousePosition, GetGraphics().DrawEllipse);
        }
    }
}
