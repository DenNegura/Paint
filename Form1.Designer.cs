namespace Paint
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            colorsBox = new FlowLayoutPanel();
            buttonNewColor = new Button();
            colorDialog = new ColorDialog();
            menuStrip1.SuspendLayout();
            colorsBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1056, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(46, 24);
            toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(126, 26);
            toolStripMenuItem2.Text = "Save";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(126, 26);
            toolStripMenuItem3.Text = "Clear";
            // 
            // colorsBox
            // 
            colorsBox.AutoScroll = true;
            colorsBox.Controls.Add(buttonNewColor);
            colorsBox.Location = new Point(12, 31);
            colorsBox.Name = "colorsBox";
            colorsBox.Size = new Size(156, 92);
            colorsBox.TabIndex = 2;
            // 
            // buttonNewColor
            // 
            buttonNewColor.BackgroundImage = (Image)resources.GetObject("buttonNewColor.BackgroundImage");
            buttonNewColor.FlatAppearance.BorderSize = 0;
            buttonNewColor.FlatStyle = FlatStyle.Flat;
            buttonNewColor.Location = new Point(3, 3);
            buttonNewColor.Name = "buttonNewColor";
            buttonNewColor.Size = new Size(24, 24);
            buttonNewColor.TabIndex = 0;
            buttonNewColor.UseVisualStyleBackColor = true;
            buttonNewColor.Click += buttonNewColor_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 608);
            Controls.Add(colorsBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            colorsBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonPen;
        private ToolStripButton toolStripButtonLine;
        private ToolStripButton toolStripButtonRectangle;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private FlowLayoutPanel colorsBox;
        private Button buttonNewColor;
        private ColorDialog colorDialog;
    }
}