using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components
{
    internal class ColorButton: Button
    {
        public ColorButton(Color color) 
        {
            SetColor(color);
            IntializeProperties();
        }

        private void IntializeProperties()
        {
            ForeColor = Color.Black;
            FlatAppearance.BorderSize = 1;
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.Black;
            Location = new Point(3, 3);
            Size = new Size(24, 24);
            UseVisualStyleBackColor = false;
        }

        public Color GetColor()
        {
            return BackColor;
        }

        public void SetColor(Color color)
        {
            BackColor = color;
            Name = $"{BackColor}";
        }
    }
}
