using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.components.Drawable;

namespace Paint.components.Figurs
{
    internal class Figure
    {
        protected Bitmap firstBitmap;

        protected Bitmap drawBitmap;

        protected Point firstMousePosition;

        protected int x, y, width, height;

        public Figure(Bitmap bitmap, Point mousePosition) 
        {
            firstBitmap = Copy(bitmap);
            firstMousePosition = mousePosition;
        }

        private Bitmap Copy(Bitmap bitmap)
        {
            return (Bitmap) bitmap.Clone();
        }

        virtual public Bitmap Draw(Bitmap bitmap, Pen pen, Point mousePosition)
        {
            throw new NotImplementedException();
        }

        protected Bitmap Draw(Bitmap bitmap, Pen pen, Point mousePosition, 
            Action<Pen, int, int, int, int> DrawFunction)
        {
            calculateSize(mousePosition);
            DrawFunction(pen, x, y, width, height);
            return drawBitmap;
        }

        protected Graphics GetGraphics() 
        {
            drawBitmap = Copy(firstBitmap);
            return Graphics.FromImage(drawBitmap);
        }

        protected void calculateSize(Point mousePosition)
        {
            if(mousePosition.X >= firstMousePosition.X)
            {
                x = firstMousePosition.X;
                width = mousePosition.X - firstMousePosition.X;
            }
            else
            {
                x = mousePosition.X;
                width = firstMousePosition.X - mousePosition.X;
            }
            if(mousePosition.Y >= firstMousePosition.Y)
            {
                y = firstMousePosition.Y;
                height = mousePosition.Y - firstMousePosition.Y;
            }
            else
            {
                y = mousePosition.Y;
                height = firstMousePosition.Y - mousePosition.Y;
            }
        }


        protected Point getLastPosition(Point lastPosition)
        {
            int x = lastPosition.X > firstMousePosition.X ? lastPosition.X - firstMousePosition.X :- lastPosition.X;
            int y = lastPosition.Y > firstMousePosition.Y ? lastPosition.Y - firstMousePosition.Y :- lastPosition.Y;
            return new Point(x, y);
        }

        public Bitmap GetFirstState()
        {
            return firstBitmap;
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
