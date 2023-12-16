using Paint.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.components
{
    internal class FontGroupBox: CustomGroupBox
    {
        private static readonly string TITLE = "Font";

        private static readonly int FONT_SIZE = 10;

        private Button buttonFontDialog;

        private FontDialog fontDialog;

        private PictureBox imageSize;

        private PictureBox imageStyle;

        private PictureBox imageFont;

        private Label labelSize;

        private Label labelStyle;

        private Label labelFont;

        private Font font;

        public event EventHandler OnChangeFont;

        public FontGroupBox() : base(TITLE)
        {
            buttonFontDialog = new Button();
            fontDialog = new FontDialog();
            labelSize = new Label();
            labelStyle = new Label();
            labelFont = new Label();
            imageSize = new PictureBox();
            imageStyle = new PictureBox();
            imageFont = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)imageSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageStyle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageFont).BeginInit();

            buttonFontDialog.Image = Resources.Settings;
            buttonFontDialog.Location = new Point(195, 85);
            buttonFontDialog.Name = "buttonFontDialog";
            buttonFontDialog.Size = new Size(32, 32);
            buttonFontDialog.TabIndex = 0;
            buttonFontDialog.UseVisualStyleBackColor = true;
 
            labelSize.AutoSize = true;
            labelSize.Location = new Point(36, 88);
            labelSize.Name = "labelSize";
            labelSize.Size = new Size(0, 22);
            labelSize.TabIndex = 6;
     
            labelStyle.AutoSize = true;
            labelStyle.Location = new Point(36, 58);
            labelStyle.Name = "labelStyle";
            labelStyle.Size = new Size(0, 22);
            labelStyle.TabIndex = 5;
   
            labelFont.AutoSize = true;
            labelFont.Location = new Point(36, 28);
            labelFont.Name = "labelFont";
            labelFont.Size = new Size(0, 22);
            labelFont.TabIndex = 4;
 
            imageSize.Image = Properties.Resources.FontSize;
            imageSize.Location = new Point(6, 86);
            imageSize.Name = "imageSize";
            imageSize.Size = new Size(24, 24);
            imageSize.TabIndex = 3;
            imageSize.TabStop = false;

            imageStyle.Image = Properties.Resources.Style;
            imageStyle.Location = new Point(6, 56);
            imageStyle.Name = "imageStyle";
            imageStyle.Size = new Size(24, 24);
            imageStyle.TabIndex = 2;
            imageStyle.TabStop = false;

            imageFont.Image = Properties.Resources.Text;
            imageFont.Location = new Point(6, 26);
            imageFont.Name = "imageFont";
            imageFont.Size = new Size(24, 24);
            imageFont.TabIndex = 1;
            imageFont.TabStop = false;

            Controls.Add(buttonFontDialog);
            Controls.Add(labelSize);
            Controls.Add(labelStyle);
            Controls.Add(labelFont);
            Controls.Add(imageSize);
            Controls.Add(imageStyle);
            Controls.Add(imageFont);

            ((System.ComponentModel.ISupportInitialize)imageSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageStyle).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageFont).EndInit();

            buttonFontDialog.Click += OpenFontDialog;

            font = this.Font;
            ChangeFont(this.Font);
        }

        private void OpenFontDialog(object? sender, EventArgs e)
        {
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog.Font;
                ChangeFont(font);
                OnChangeFont_Event(font);
            }
        }

        private void ChangeFont(Font font) {
            Font labelsFont = new Font(font.Name, FONT_SIZE, font.Style);
            labelFont.Font = labelsFont;
            labelFont.Text = labelsFont.Name;
            labelStyle.Text = labelsFont.Style.ToString();
            labelSize.Text = font.Size.ToString() + "px";
        }

        private void OnChangeFont_Event(Font font)
        {
            OnChangeFont?.Invoke(font, EventArgs.Empty);
        }

        public Font GetFont()
        {
            return font;
        }
    }
}
