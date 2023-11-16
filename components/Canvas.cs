using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.components
{
    internal class Canvas: PictureBox
    {
        private readonly int MIN_HEIGHT = 50;

        private readonly int MIN_WIDTH = 50;

        private readonly int SIZE_GRIP = 10;

        private Point currentMousePos;

        private bool isResize = false;

        private bool isResizeX = false;

        private bool isResizeY = false;

        public Canvas() 
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            BackColor = Color.White;
            this.MouseDown += new MouseEventHandler(this.Function_MouseDown);
            this.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            this.MouseUp += new MouseEventHandler(this.pictureBox1_MouseUp);
        }

        private bool IsResizeByX(int x)
        {
            return x >= this.Width - SIZE_GRIP;
        }

        private bool IsResizeByY(int y)
        {
            return y >= this.Height - SIZE_GRIP;
        }

        private void Function_MouseDown(object sender, MouseEventArgs e)
        {
            isResize = true;
            currentMousePos = e.Location;
            this.Capture = true;

            if (IsResizeByX(e.X) && IsResizeByY(e.Y))
            {
                isResizeX = isResizeY = true;
            }
            else if (IsResizeByX(e.X))
            {
                isResizeX = true;
                isResizeY = false;
            }
            else if (IsResizeByY(e.Y))
            {
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

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
                else if(isResizeY) 
                {
                    this.Height = GetResizeHeight(e.Y);                    
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isResize = isResizeX = isResizeY = false;
            this.Capture = false;
        }
    }
}
