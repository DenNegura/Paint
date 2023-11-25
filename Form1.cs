using Paint.components;
using System.Drawing;

namespace Paint
{
    public partial class Form1 : Form
    {

        static Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form_KeyDown;
            this.KeyPreview = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SelectTool(object sender, EventArgs e)
        {

        }

        
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) {
                canvas.Redo();
            }
            else if(e.Control && e.KeyCode == Keys.Y)
            {
                canvas.Undo();
            }
        }
    }
}