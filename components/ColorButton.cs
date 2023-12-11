using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components
{
    internal class ColorButton: Button
    {

        public Color Color { get => BackColor; set => BackColor = value; }

        public ColorButton() 
        {
            ForeColor = Color.Black;
            FlatAppearance.BorderSize = 1;
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.Black;
            Location = new Point(3, 3);
            Size = new Size(24, 24);
            UseVisualStyleBackColor = false;
        }
    }
}
