using Paint.components.Canvas;
using Paint.components.Figurs;
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

        private Bitmap bitmap;

        private Graphics graphics;

        StateHistory<Bitmap> bitmapHistory;

        public Canvas() 
        {
            bitmapHistory = new StateHistory<Bitmap>();   
            bitmap = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bitmap);
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
            CrookedLineMouseDown(e);
            bitmapHistory.SetCurrentState(bitmap);
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
                bitmapHistory.SaveState();
            } 
        }

        public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                base.Size = value;
                ResizeBitmap();
                bitmapHistory.SetCurrentState(bitmap);
                bitmapHistory.SaveState();
            }
        }

        private void ResizeBitmap()
        {
            Bitmap oldBitmap = (Bitmap)bitmap.Clone();
            bitmap = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(oldBitmap, 0, 0);
            Image = bitmap;
        }

        private void changeBitmap(Bitmap newBitmap)
        {
            bitmap = newBitmap;

            if (newBitmap.Width != Width || newBitmap.Height != Height)
            {
                base.Size = newBitmap.Size;
            }

            graphics = Graphics.FromImage(bitmap);
            Image = bitmap;
        }

        public void Redo()
        {
            Bitmap? nextBitmap = bitmapHistory.Redo(bitmap);
            if (nextBitmap != null)
            {
                changeBitmap(nextBitmap);
            }
        }

        public void Undo()
        {
            Bitmap? nextBitmap = bitmapHistory.Undo(bitmap);
            if (nextBitmap != null)
            {
                changeBitmap(nextBitmap);
            }
        }
    }


    // drow 
    partial class Canvas
    {
        private bool isDrawCrookedLine = false;

        private bool isRectangle = false;

        List<Point> crookedLine;

        private Figure figure;

        private void CrookedLineMouseDown(MouseEventArgs e)
        {
            if(!isResize)
            {
                //isDrawCrookedLine = true;
                isRectangle = true;
                crookedLine = new List<Point>();
                m = (Bitmap) bitmap.Clone();
                figure = new FSircle(bitmap, e.Location);
            } 
        }

        private void CrookedLineMouseUp()
        {
            isDrawCrookedLine = false;
            isRectangle = false;
            
        }
        private Bitmap m;
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

            if(isRectangle)
            {
                Image = figure.GetFirstState();
                bitmap = figure.Draw(bitmap, new Pen(Color.Red, 4), e.Location);
                Image = bitmap;

                //bitmap = (Bitmap) firstBitmap.Clone();
                //Image = bitmap;
                //Graphics.FromImage(bitmap).DrawRectangle(new Pen(Color.Red, 4), 
                //    firstPosition.X, firstPosition.Y, 
                //    e.X - firstPosition.X, e.Y - firstPosition.Y);
                //Image = bitmap;
            }
        }

        private void SetImage(Image image)
        {
            //this.bitmap = (Bitmap)image;
            this.Image = image;
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
}
