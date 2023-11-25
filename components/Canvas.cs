using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    internal partial class Canvas: PictureBox
    {

        Point beginMouseLocation;

        Point endMouseLocation;

        Bitmap lastBitmap;

        public Canvas() 
        {
            bitmap = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bitmap);
            redo = new Stack<Object>();
            undo = new Stack<Object>();
            this.Image = bitmap;
            InitializeHendlres();
        }

        private void InitializeHendlres()
        {
            this.MouseDown += new MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new MouseEventHandler(this.Canvas_MouseUp);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            beginMouseLocation = e.Location;
            ResizeMouseDown(e);
            CrookedLineMouseDown();
            lastBitmap = (Bitmap) bitmap.Clone();
        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            int width = this.Width;
            int height = this.Height;

            ResizeMouseMove(e);
           
            if (width != this.Width || height != this.Height)
            {
                ResizeBitmap();
            }

            CrookedLineMouseMove(e);
        }

      
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            endMouseLocation = e.Location;
            ResizeMouseUp(e);
            CrookedLineMouseUp();
            if(!beginMouseLocation.Equals(endMouseLocation))
            {
                SaveState(lastBitmap);
            } 
        }
    }

    // bitmap 
    partial class Canvas
    {
        private Bitmap bitmap;

        private Graphics graphics;

        
        public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                base.Size = value;
                ResizeBitmap();
                SaveState(bitmap);
            }
        }

        private void ResizeBitmap()
        {
            Bitmap oldBitmap = (Bitmap) bitmap.Clone();
            bitmap = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(oldBitmap, 0, 0);
            Image = bitmap;
        }

        private void changeBitmap(Bitmap newBitmap)
        {
            bitmap = newBitmap;

            if(newBitmap.Width != Width || newBitmap.Height != Height)
            {
                base.Size = newBitmap.Size;
            }
            
            graphics = Graphics.FromImage(bitmap);
            Image = bitmap;
        }
    }

    // drow crooked line 
    partial class Canvas
    {
        private bool isDrawCrookedLine = false;

        List<Point> crookedLine;
        private class CrookedLine
        {
            List<Point> Points;

            public CrookedLine()
            {
                Points = new List<Point>();
            }
            
            public void add(Point point)
            {
                Points.Add(point);
            }

            public List<Point> Get()
            {
                return Points;
            }
            public void Clear() 
            { 
                Points = new List<Point>(); 
            }
        }


        private void CrookedLineMouseDown()
        {
            if(!isResize)
            {
                isDrawCrookedLine = true;
                crookedLine = new List<Point>();
            } 
        }

        private void CrookedLineMouseUp()
        {
            isDrawCrookedLine = false;
           
        }

        private void CrookedLineMouseMove(MouseEventArgs e)
        {
            if(isDrawCrookedLine)
            {

                crookedLine.Add(e.Location);
                if (crookedLine.Count > 1)
                {
                    graphics.DrawLines(new Pen(Color.Red, 4), crookedLine.ToArray());
                    Image = bitmap;
                }
            }
        }
    }

    // resize property
    partial class Canvas
    {
        private readonly int MIN_HEIGHT = 50;

        private readonly int MIN_WIDTH = 50;

        private readonly int SIZE_GRIP = 10;

        private Point currentMousePos;

        private bool isResize = false;

        private bool isResizeX = false;

        private bool isResizeY = false;
        private bool IsResizeByX(int x)
        {
            return x >= this.Width - SIZE_GRIP;
        }

        private bool IsResizeByY(int y)
        {
            return y >= this.Height - SIZE_GRIP;
        }

        private void ResizeMouseDown(MouseEventArgs e)
        {
            currentMousePos = e.Location;
            this.Capture = true;

            if (IsResizeByX(e.X) && IsResizeByY(e.Y))
            {
                isResize = isResizeX = isResizeY = true;
            }
            else if (IsResizeByX(e.X))
            {
                isResize = true;
                isResizeX = true;
                isResizeY = false;
            }
            else if (IsResizeByY(e.Y))
            {
                isResize = true;
                isResizeY = true;
                isResizeX = false;
            }
            else
            {
                isResize = false;
                isResizeY = false;
                isResizeX = false;
            }
        }
        private int GetResizeWidth(int x)
        {
            int width = this.Width + x - currentMousePos.X;
            if (width > MIN_WIDTH)
            {
                currentMousePos.X = x;
                return width;
            }
            return this.Width;
        }

        private int GetResizeHeight(int y)
        {
            int height = this.Height + y - currentMousePos.Y;
            if (height > MIN_HEIGHT)
            {
                currentMousePos.Y = y;
                return height;
            }
            return this.Height;
        }

        private void ResizeMouseMove(MouseEventArgs e)
        {
            if (IsResizeByX(e.X) && IsResizeByY(e.Y))
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else if (IsResizeByX(e.X))
            {
                this.Cursor = Cursors.SizeWE;
            }
            else if (IsResizeByY(e.Y))
            {
                this.Cursor = Cursors.SizeNS;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            if (isResize)
            {
                if (isResizeX && isResizeY)
                {
                    this.Width = GetResizeWidth(e.X);
                    this.Height = GetResizeHeight(e.Y);
                }
                else if (isResizeX)
                {
                    this.Width = GetResizeWidth(e.X);
                }
                else if (isResizeY)
                {
                    this.Height = GetResizeHeight(e.Y);
                }
            }
        }

        private void ResizeMouseUp(MouseEventArgs e)
        {
            isResize = isResizeX = isResizeY = false;
            this.Capture = false;
        }
    }

    // redo / undo options
    partial class Canvas
    {
        private Stack<Object> redo;

        private Stack<Object> undo;

        private void SaveState(Bitmap bitmap)
        {
            redo.Push(bitmap.Clone());
        }

        public void Redo()
        {
            if(redo.Count != 0)
            {
                Bitmap nextBitmap = (Bitmap)redo.Pop();
                undo.Push(bitmap.Clone());
                changeBitmap(nextBitmap);
            } 
        }
        public void Undo() 
        { 
            if(undo.Count != 0)
            {
                Bitmap nextBitmap = (Bitmap)undo.Pop();
                redo.Push(bitmap.Clone());
                changeBitmap(nextBitmap);
            }
        }
    }
}
