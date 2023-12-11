using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Figurs
{
    internal class FRectangle : Figure
    {

        public FRectangle(Bitmap bitmap, Point mousePosition) : 
            base(bitmap, mousePosition) {}

        override public Bitmap Draw(Bitmap bitmap, Pen pen, Point mousePosition)
        {
            return Draw(bitmap, pen, mousePosition, GetGraphics().DrawRectangle);
        }
    }
}
