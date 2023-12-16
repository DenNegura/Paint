using Paint.components.Canvas;
using Paint.components.Drawable;
using System.Linq;
using System.Windows.Forms;

namespace Paint
{
    internal partial class Canvas: PictureBox
    {

        // констинты
        private static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;

        // переменные
        private Color backgroundColor = DEFAULT_BACKGROUND_COLOR;

        Point beginMouseLocation; // позиция мыши при нажатии

        private Bitmap bitmap; // основной bitmap

        private Bitmap drawBitmap; // bitmap используемый в процессе рисования

        private Graphics graphics; // Класс Graphics для рисования

        private StateHistory<Bitmap> bitmapHistory; // запоминает сосотояние bitmap'ов, функции redo undo

        private RichTextBox textBox;

        // константы
        private bool isResize = false; // флаг изменения размера

        private bool isFill = false; // флаг заполнения

        private bool isDraw = false; // флаг рисования

        private bool isText = false; // флаг написания текста


        // события
        public event EventHandler OnMoveMouse; // Когда мышка перемещается

        public event EventHandler OnResize; // когда размер изменяется

        public event EventHandler OnSelectPixel; // когда есть нажатие на канвас


        public Canvas()
        {
            bitmapHistory = new StateHistory<Bitmap>(20);
            bitmap = new Bitmap(this.Width, this.Height);

            graphics = Graphics.FromImage(bitmap);
            SetBackgroundColor(backgroundColor);

            this.Image = bitmap;

            textBox = new RichTextBox();
            textBox.Visible = false;
            this.Controls.Add(textBox);
            textBox.Leave += OnLeave_Event;
            textBox.TextChanged += OnTextChanged_Event;

            this.MouseDown += new MouseEventHandler(this.Canvas_MouseDown);
            this.MouseMove += new MouseEventHandler(this.Canvas_MouseMove);
            this.MouseUp += new MouseEventHandler(this.Canvas_MouseUp);
        }

        // передача размера
        private void OnResize_Event()
        {
            OnResize?.Invoke(this.Size, EventArgs.Empty);
        }

        // передача координат мыши
        private void OnMouseMove_Event(MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;
            if (x < 0 || x > Width) x = 0;
            if (y < 0 || y > Height) y = 0;
            OnMoveMouse?.Invoke(new Point(x, y), EventArgs.Empty);
        }

        // передача цвета пикселя
        private void OnSelectPixel_Event(MouseEventArgs e)
        {
            OnSelectPixel?.Invoke(bitmap.GetPixel(e.X, e.Y), EventArgs.Empty);
        }

        // Оброботчик события нажатия мыши на канвас
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            OnSelectPixel_Event(e);

            beginMouseLocation = e.Location;
            bitmapHistory.SetCurrentState(bitmap);

            IsResizeMode_MouseDown(e);
            if(!isResize)
            {
                IsDraw_MouseDown();
            }

            if (isResize)
            {
                Resize_MouseDown(e);
            }
            else if (isFill)
            {
                Fill_MouseDown(e);
            }
            else if (isDraw)
            {
                Draw_MouseDown();
            }
            else if(isText)
            {
                if(!textBox.Visible)
                {
                    WriteText_MouseDown(e);
                }
                else
                {
                    DrawText_MouseDown();
                }
            }
        }

        // Обработчик события перетаскивания мыши
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove_Event(e);

            ChangeMouseState_MouseMove(e);

            if (isResize)
            {
                Resize_MouseMove(e);
            }
            else if(isDraw)
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
            else if (isFill)
            {
                bitmapHistory.SaveState();
            } 
            else if (isDraw)
            {
                if(isMouseMoved(e.Location))
                {
                    Draw_MouseUp(e);
                }
                else
                {
                    isDraw = false;
                }
            }
            if ((isText && !textBox.Visible) || isMouseMoved(e.Location))
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

        public void Clear()
        {
            bitmapHistory.SetCurrentState(bitmap);
            bitmapHistory.SaveState();
            SetBackgroundColor(backgroundColor);
            changeBitmap(bitmap);
            ClearHistory();
        }

        public bool isEmpty()
        {
            return bitmapHistory.isEmpty();
        }

        public void ClearHistory()
        {
            bitmapHistory.Clear();
        }

        public void Save(string filePath)
        {
            bitmap.Save(filePath);
        }

        public void SetImage(Image image)
        {
            Size = image.Size;
            graphics.DrawImage(image, new RectangleF(0, 0, image.Width, image.Height));
        }
    }


    // draw 
    partial class Canvas
    {
        private IDrawable drawObj; // Объект рисования

        private Pen pen; // перо

        private Brush brush; // заливка

        // Установка объекта рисования 
        public void SetDrawableObject(IDrawable? drawable)
        {
            drawObj = drawable;
            if(drawObj != null)
            {
                SetFillMode(false);
                SetTextMode(false);
            }
        }

        public void SetBrush(Brush brush)
        {
            this.brush = brush;
        }

        // установка пера
        public void SetPen(Pen pen)
        {
            this.pen = pen;
        }

        // установка размера
        public void SetPenSize(int size)
        {
            if(pen != null)
            {
                pen.Width = size;
            }
        }

        // установка цвета
        public void SetPenColor(Color color)
        {
            if (pen != null)
            {
                pen.Color = color;
            }
        }

        // функция изменения флага при рисовании
        private void IsDraw_MouseDown()
        {
            isDraw = false;
            if (drawObj != null)
            {
                isDraw = true;
            }
        }

        // функция начала рисования
        private void Draw_MouseDown()
        {
            drawObj = drawObj.GetInstance();
        }

        // функция рисования
        private void Draw_MouseMove(MouseEventArgs e)
        {
            if(isDraw)
            {
                drawBitmap = (Bitmap)bitmap.Clone();

                if(brush != null)
                {
                    drawObj.Draw(Graphics.FromImage(drawBitmap), brush, pen, beginMouseLocation, e.Location);
                } else
                {
                    drawObj.Draw(Graphics.FromImage(drawBitmap), pen, beginMouseLocation, e.Location);
                }

                Image = drawBitmap;
            }
        }

        // функция остановки рисования
        private void Draw_MouseUp(MouseEventArgs e)
        {
            if (drawBitmap != null)
            {
                bitmap = drawBitmap;
            }

            isDraw = false;
        }

        // установка заливки
        public void SetFillMode(bool isFillMode)
        {
            this.isFill = isFillMode;
            if (isFillMode)
            {
                SetDrawableObject(null);
                SetTextMode(false);
            }
        }

        // функция вызова заливки
        private void Fill_MouseDown(MouseEventArgs e)
        {
            Fill(e.Location, ((SolidBrush) brush).Color);
        }

        // заливка
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

    // write text
    partial class Canvas
    {
        private Font font;

        public void SetFont(Font font)
        {
            this.font = font;
            if(font != null)
            {
                textBox.Font = font;
            }
        }

        public void SetTextMode(bool isTextMode)
        {
            if(font != null)
            {
                isText = isTextMode;
                if(isTextMode)
                {
                    SetFillMode(false);
                    SetDrawableObject(null);
                }
            }
            else
            {
                isText = false;
            }
        }

        private void WriteText_MouseDown(MouseEventArgs e)
        {
            textBox.Location = new Point(e.X, e.Y);
            textBox.Visible = true;
            textBox.Focus();
        }

        private void DrawText_MouseDown()
        {
            textBox.Visible = false;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                graphics.DrawString(textBox.Text, font, new SolidBrush(pen.Color), textBox.Location);
                textBox.Text = "";
            }
        }

        private void OnLeave_Event(object sender, EventArgs e)
        {
            textBox.Visible = false;
        }

        private void OnTextChanged_Event(object sender, EventArgs e)
        {
            int width = TextRenderer.MeasureText(textBox.Text, textBox.Font).Width + 20;
            width = textBox.Location.X + width > Width ? Width - textBox.Location.X : width;

            int height = font.Height * (textBox.Lines.Length + 1);
            height = textBox.Location.Y + height > Height ? Height - textBox.Location.Y : height;

            textBox.Height = height;
            textBox.Width = width;
        }

        private void OpenTextBox(Point location)
        {
            textBox.Location = location;
            textBox.Visible = true;
            textBox.BackColor = pen.Color;
            textBox.Focus();
        }
    }

    // resize property
    partial class Canvas
    {
        // константы
        private static readonly int MIN_HEIGHT = 50;

        private static readonly int MIN_WIDTH = 50;

        private static readonly int SIZE_GRIP = 10;

        // переменные
        private int maxHeight;

        private int maxWidth;

        private Point currentMousePos;


        private bool isResizeX = false;

        private bool isResizeY = false;

        // установка макимального размера по высоте
        public void setMaxHeight(int height)
        {
            this.maxHeight = height;
        }

        // установка макимального размера по ширине
        public void setMaxWight(int wigth)
        {
            this.maxWidth = wigth;
        }

        // установка макимального размера по высоте и ширине
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

        // Изменяет флаг размера
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
                OnResize_Event();
            }
            else if (isResizeX)
            {
                this.Width = GetResizeWidth(e.X);
                OnResize_Event();
            }
            else if (isResizeY)
            {
                this.Height = GetResizeHeight(e.Y);
                OnResize_Event();
            }
        }

        private void Resize_MouseUp(MouseEventArgs e)
        {
            isResize = isResizeX = isResizeY = false;
            this.Capture = false;
        }
    }
}
