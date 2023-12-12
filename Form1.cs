using Paint.components;
using Paint.components.Drawable;
using Paint.components.Tools;
using System.Drawing;
using System.Xml.Serialization;

namespace Paint
{
    public partial class Form1 : Form
    {

        static Bitmap bitmap;

        private bool isColourPicker = false;

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form_KeyDown;
            this.KeyPreview = true;

            figureGroupBox.OnSelect += onSelectFigure;
            groupBoxTools.OnSelect += onSelectTool;
            groupBoxColors.OnSelect += onSelectColor;
            canvas.OnSelectPixel += onSelectPixel;
            canvas.setMaxHeight(canvasContainer.Height);
            canvas.setMaxWight(canvasContainer.Width);
        }

        private void onSelectFigure(object sender, EventArgs e)
        {
            isColourPicker = false;
            canvas.setFillMode(false);
            canvas.setDrawableObject((IDrawable)sender);
        }

        private void onSelectTool(object sender, EventArgs e)
        {
            EnumTool tool = (EnumTool)sender;
            canvas.setFillMode(false);
            isColourPicker = false;
            switch (tool)
            {
                case EnumTool.ERASER:
                    canvas.setDrawableObject(new Polyline());
                    canvas.setPenColor(canvas.GetBackroundColor());
                    break;
                case EnumTool.COLOUR_PICKER:
                    isColourPicker = true;
                    canvas.setFillMode(false);
                    canvas.setDrawableObject(null);
                    break;
                case EnumTool.PEN:
                    canvas.setDrawableObject(new Polyline());
                    canvas.setPenColor(groupBoxColors.getCurrentColor());
                    break;
                case EnumTool.FILL:
                    canvas.setFillMode(true);
                    break;
            }
        }

        private void onSelectPixel(object sender, EventArgs e)
        {
            if(isColourPicker)
            {
                groupBoxColors.setCurrentColor((Color)sender);
                canvas.setPenColor((Color)sender);
            }
            
        }

        private void onSelectColor(object sender, EventArgs e)
        {
            canvas.setPenColor((Color)sender);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                canvas.Redo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                canvas.Undo();
            }
        }

        private void changeColor()
        {

        }

        private void changeDraw()
        {

        }
    }
}