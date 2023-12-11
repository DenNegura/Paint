using Paint.components.Canvas;
using Paint.components.Drawable;
using Paint.components.Figurs;
using Paint.components.Tools;
using System.Xml;

namespace Paint
{
    internal partial class Canvas: PictureBox
    {

        Point beginMouseLocation; // позиция мыши при нажатии

        private Bitmap bitmap; // основной bitmap

        private Bitmap drawBitmap; // bitmap используемый в процессе рисования

        private Graphics graphics; 

        private StateHistory<Bitmap> bitmapHistory; // запоминает сосотояние bitmap'ов, функции redo undo

        public Canvas() 
        {
            bitmapHistory = new StateHistory<Bitmap>(20);  
            bitmap = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bitmap);
            this.Image = bitmap;

            this.MouseDown += new MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new MouseEventHandler(this.Canvas_MouseUp);
        }

        // Оброботчик события нажатия мыши на канвас
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            beginMouseLocation = e.Location;
            bitmapHistory.SetCurrentState(bitmap);

            if (isFillMode)
            {
                Fill(e.Location, pen.Color);
            }
            else
            {
                ResizeMouseDown(e);

                if (!isResize)
                {
                    DrawMouseDown(e);
                }
            }
        }

        // Обработчик события перетаскивания мыши
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            ResizeMouseMove(e);
            DrawMouseMove(e);
        }

        // Обработчик события отпускания мыши с канваса
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isFillMode)
            {
                bitmapHistory.SaveState();
                return;
            } 
            if (!isResize)
            {
                DrawMouseUp(e);
            }
            if (!beginMouseLocation.Equals(e.Location))
            {
                ResizeBitmap();
                bitmapHistory.SaveState();
            }
            ResizeMouseUp(e);
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

        // Изменение размера bitmap
        // Прошлый bitmap сохраняется в историю
        private void ResizeBitmap()
        {
            Bitmap oldBitmap = (Bitmap)bitmap.Clone();
            bitmap = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(oldBitmap, 0, 0);
            Image = bitmap;
        }


        // Замена битмапа
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


    // draw 
    partial class Canvas
    {

        private static readonly Pen DEFAULT_PEN = new Pen(Color.Black, 3);

        private static readonly IDrawable DEFAULT_DRAWABLE = new Polyline();

        private bool isDraw = false;

        private IDrawable drawObj = DEFAULT_DRAWABLE; // Объект рисования

        private Pen pen = DEFAULT_PEN;

        // Установка объекта рисования 
        public void setDrawableObject(IDrawable drawable)
        {
            drawObj = drawable;
        }

        public void setPenColor(Color color)
        {
            pen.Color = color;
        }

        public void setPenWidth(int width)
        {
            pen.Width = width;
        }
        private void DrawMouseDown(MouseEventArgs e)
        {
            if (drawObj != null)
            {  
                drawObj = drawObj.getInstance();
                isDraw = true;
            }
            
        }

        private void DrawMouseMove(MouseEventArgs e)
        {
            if(isDraw)
            {
                drawBitmap = (Bitmap)bitmap.Clone();

                drawObj.Draw(Graphics.FromImage(drawBitmap), pen, beginMouseLocation, e.Location);

                Image = drawBitmap;
            }
        }

        private void DrawMouseUp(MouseEventArgs e)
        {
            if (drawBitmap != null)
            {
                bitmap = drawBitmap;
            }

            isDraw = false;
        }

        private bool isFillMode;
        
        public void setFillMode(bool isFillMode)
        {
            this.isFillMode = isFillMode;
        }

        private void Fill(Point selectPixel, Color color)
        {
            Color defaultColor = bitmap.GetPixel(selectPixel.X, selectPixel.Y);
            if(defaultColor.ToArgb() == color.ToArgb())
            {
                return;
            }

            Stack<Point> stack = new Stack<Point>();
            stack.Push(selectPixel);

            while(stack.Count > 0)
            {
                Point current = stack.Pop();
                
                if(current.X < 0  || current.Y < 0 || current.X >= bitmap.Width || current.Y >= bitmap.Height)
                {
                    continue;
                }
                if(bitmap.GetPixel(current.X, current.Y) == defaultColor)
                {
                    bitmap.SetPixel(current.X, current.Y, color);
                    stack.Push(new Point(current.X + 1, current.Y));
                    stack.Push(new Point(current.X - 1, current.Y));
                    stack.Push(new Point(current.X, current.Y + 1));
                    stack.Push(new Point(current.X, current.Y - 1));
                }
            }

            changeBitmap(bitmap);
        }
    }


    // resize property
    partial class Canvas
    {
        private static readonly int MIN_HEIGHT = 50;

        private static readonly int MIN_WIDTH = 50;

        private static readonly int SIZE_GRIP = 10;

        private Point currentMousePos;

        private bool isResize = false;

        private bool isResizeX = false;

        private bool isResizeY = false;

        private bool IsInResize()
        {
            return isResize;
        }
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
