using Paint.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components
{
    internal class ImageButton : Button
    {
        public new Image? Image { get { return BackgroundImage; } set { BackgroundImage = value; } }

        public ImageButton(Image image)
        { 
            Image = image;
            ImageAlign = ContentAlignment.MiddleCenter;
            BackgroundImageLayout = ImageLayout.Stretch;
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Location = new Point(3, 3);
            Size = new Size(24, 24);
            UseVisualStyleBackColor = true;
        }
    }
}
