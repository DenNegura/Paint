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
        public readonly Image image;

        public readonly IDrawable drawable;

        public readonly string name;

        public Figure(Image image, IDrawable drawable, string name)
        {
            this.image = image;
            this.drawable = drawable;
            this.name = name;
        }

        public static readonly Figure LINE = 
            new Figure(Resources.Line, new Line(), "Line");

        public static readonly Figure RECTANGLE = 
            new Figure(Resources.Rectangle, new Rectangle(), "Rectangle");

        public static readonly Figure ELLIPSE =
            new Figure(Resources.Ellipse, new Ellipse(), "Ellipse");

        public static readonly Figure STAR =
            new Figure(Resources.Star, new Star(), "Star");

        public static readonly Figure HEART =
            new Figure(Resources.Heart, new Heart(), "Heart");

        public static readonly Figure[] FIGURES = new Figure[] {
            LINE, RECTANGLE, ELLIPSE, STAR, HEART,
        };
    }
}
