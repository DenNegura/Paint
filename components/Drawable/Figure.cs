using Paint.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components.Drawable
{
    internal class Figure
    {
        public enum Mode
        {
            FILL_BORDER,

            BORDER,

            FILL,
        }

        public readonly Image image;

        public readonly IDrawable drawable;

        public readonly string name;

        public readonly bool hasPen;

        public readonly bool hasBrush;

        public Figure(Image image, IDrawable drawable, string name, bool hasPen, bool hasBrush)
        {
            this.image = image;
            this.drawable = drawable;
            this.name = name;
            this.hasPen = hasPen;
            this.hasBrush = hasBrush;
        }

        public static readonly Figure LINE = 
            new Figure(Resources.Line, new Line(), "Line", true, false);

        public static readonly Figure RECTANGLE = 
            new Figure(Resources.Rectangle, new Rectangle(), "Rectangle", true, true);

        public static readonly Figure ELLIPSE =
            new Figure(Resources.Ellipse, new Ellipse(), "Ellipse", true, true);

        public static readonly Figure STAR =
            new Figure(Resources.Star, new Star(), "Star", true, true);

        public static readonly Figure HEART =
            new Figure(Resources.Heart, new Heart(), "Heart", true, true);

        public static readonly Figure[] FIGURES = new Figure[] {
            LINE, RECTANGLE, ELLIPSE, STAR, HEART,
        };
    }
}
