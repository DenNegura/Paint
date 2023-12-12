using Microsoft.VisualBasic.Devices;
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

        private bool isResize = false; // флаг изменения размера

        private bool isFill = false; // флаг заполнения

        private bool isDraw = false; // флаг рисования

        public event EventHandler OnMoveMouse;

        public event EventHandler OnSelectPixel;

        private static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;

        private Color backgroundColor = DEFAULT_BACKGROUND_COLOR;


        private void OnSelectPixelHandler(MouseEventArgs e)
        {
            OnSelectPixel?.Invoke(bitmap.GetPixel(e.X, e.Y), EventArgs.Empty);
        }
        
        private void SetBackgroundColor(Color backgroundColor)
        {
            this.backgroundColor = backgroundColor;
            BackColor = backgroundColor;
            graphics?.Clear(backgroundColor);
        }

        public Color GetBackroundColor()
        {
            return backgroundColor;
        }
        

        public Canvas()
        {
            bitmapHistory = new StateHistory<Bitmap>(20);  
            bitmap = new Bitmap(this.Width, this.Height);
            
            graphics = Graphics.FromImage(bitmap);
            SetBackgroundColor(backgroundColor);
          
            this.Image = bitmap;

            this.MouseDown += new MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new MouseEventHandler(this.Canvas_MouseUp);
        }

        // Оброботчик события нажатия мыши на канвас
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            OnSelectPixelHandler(e);

            beginMouseLocation = e.Location;
            bitmapHistory.SetCurrentState(bitmap);

            IsResizeMode_MouseDown(e);
            
            if(!isResize && !isFill)
            {
                IsDrawMode_MouseDown(e);
            }

            if (isResize)
            {
                Resize_MouseDown(e);
            }

            if (!isResize && isFill)
            {
                Fill_MouseDown(e);
            }
            if (!isResize && !isFill && isDraw)
            {
                Draw_MouseDown(e);
            }
        }

        // Обработчик события перетаскивания мыши
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeMouseState_MouseMove(e);
            
            if (isResize)
            {
                Resize_MouseMove(e);
            }
            if(!isResize && !isFill && isDraw)
            {
                Draw_MouseMove(e);
            }
        }

        // Обработчик события отпускания мыши с канваса
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Resize_MouseUp(e);
            }
            
            if (!isResize && isFill)
            {
                bitmapHistory.SaveState();
            } 
            if (!isResize && !isFill && isDraw && isMouseMoved(e.Location))
            {
                Draw_MouseUp(e);
            }

            if (isMouseMoved(e.Location))
            {
                ResizeBitmap();
                bitmapHistory.SaveState();
            }
            
        }

        private bool isMouseMoved(Point mousePos)
        {
            return !beginMouseLocation.Equals(mousePos);
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
            SetBackgroundColor(backgroundColor);
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


        private IDrawable drawObj = DEFAULT_DRAWABLE; // Объект рисования

        private Pen pen = DEFAULT_PEN;

        // Установка объекта рисования 
        public void setDrawableObject(IDrawable? drawable)
        {
            drawObj = drawable;
            if(drawable != null)
            {
                setFillMode(false);
            }
        }

        public void setPenColor(Color color)
        {
            pen.Color = color;
        }

        public void setPenWidth(int width)
        {
            pen.Width = width;
        }

        private bool IsDrawMode_MouseDown(MouseEventArgs e)
        {
            isDraw = false;
            if(drawObj != null)
            {
                isDraw = true;
            }
            return isDraw;
        }
        private void Draw_MouseDown(MouseEventArgs e)
        {
            drawObj = drawObj.GetInstance();
        }

        private void Draw_MouseMove(MouseEventArgs e)
        {
            if(isDraw)
            {
                drawBitmap = (Bitmap)bitmap.Clone();

                drawObj.Draw(Graphics.FromImage(drawBitmap), pen, beginMouseLocation, e.Location);

                Image = drawBitmap;
            }
        }

        private void Draw_MouseUp(MouseEventArgs e)
        {
            if (drawBitmap != null)
            {
                bitmap = drawBitmap;
            }

            isDraw = false;
        }

        public void setFillMode(bool isFillMode)
        {
            this.isFill = isFillMode;
            if (isFillMode)
            {
                setDrawableObject(null);
            }
        }

        private void Fill_MouseDown(MouseEventArgs e)
        {
            Fill(e.Location, pen.Color);
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

        private int maxHeight;

        private int maxWidth;

        private Point currentMousePos;


        private bool isResizeX = false;

        private bool isResizeY = false;


        public void setMaxHeight(int height)
        {
            this.maxHeight = height;
        }

        public void setMaxWight(int wigth)
        {
            this.maxWidth = wigth;
        }

        public void setMaxSize(Point size)
        {
            setMaxWight(size.X);
            setMaxHeight(size.Y);
        }

        private bool IsResizeByX(int x)
        {
            return x >= this.Width - SIZE_GRIP;
        }

        private bool IsResizeByY(int y)
        {
            return y >= this.Height - SIZE_GRIP;
        }

        private bool IsResizeMode_MouseDown(MouseEventArgs e)
        {
            isResize = isResizeX = isResizeY = false;

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
            return isResize;
        }

        private int GetResizeWidth(int x)
        {
            int width = this.Width + x - currentMousePos.X;
            if (width < MIN_WIDTH)
            {
                width = MIN_WIDTH;
            }
            else
            {
                currentMousePos.X = x;
            }
            if(width > maxWidth)
            {
                width = maxWidth;
            }
            else
            {
                currentMousePos.X = x;
            }
            return width;
        }

        private int GetResizeHeight(int y)
        {
            int height = this.Height + y - currentMousePos.Y;
            if (height < MIN_HEIGHT)
            {
                height = MIN_HEIGHT;
            }
            else
            {
                currentMousePos.Y = y;
            }
            if (height > maxHeight)
            {
                height = maxHeight;
            }
            else
            {
                currentMousePos.Y = y;
            }
            return height;
        }

        private void Resize_MouseDown(MouseEventArgs e)
        {
            currentMousePos = e.Location;

            this.Capture = true;
        }

        private void ChangeMouseState_MouseMove(MouseEventArgs e)
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
        }
        private void Resize_MouseMove(MouseEventArgs e)
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

        private void Resize_MouseUp(MouseEventArgs e)
        {
            isResize = isResizeX = isResizeY = false;
            this.Capture = false;
        }
    }
}
