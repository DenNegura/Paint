using Paint.components;

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
            addImageToolStripMenuItem = new ToolStripMenuItem();
            groupBoxColors = new ColorGroupBox();
            groupBoxTools = new ToolGroupBox();
            groupBox2 = new GroupBox();
            panel1 = new Panel();
            canvas = new Canvas();
            groupBoxSize = new SizeGroupBox();
            testLabel = new Label();
            figureGroupBox = new FigureGroupBox();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
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
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, addImageToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(46, 24);
            toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(166, 26);
            toolStripMenuItem2.Text = "Save";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(166, 26);
            toolStripMenuItem3.Text = "Clear";
            // 
            // addImageToolStripMenuItem
            // 
            addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            addImageToolStripMenuItem.Size = new Size(166, 26);
            addImageToolStripMenuItem.Text = "Add Image";
            // 
            // groupBoxColors
            // 
            groupBoxColors.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxColors.Location = new Point(827, 31);
            groupBoxColors.Name = "groupBoxColors";
            groupBoxColors.Size = new Size(229, 125);
            groupBoxColors.TabIndex = 7;
            groupBoxColors.TabStop = false;
            groupBoxColors.Text = "Colors";
            // 
            // groupBoxTools
            // 
            groupBoxTools.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxTools.Location = new Point(592, 31);
            groupBoxTools.Name = "groupBoxTools";
            groupBoxTools.Size = new Size(229, 125);
            groupBoxTools.TabIndex = 7;
            groupBoxTools.TabStop = false;
            groupBoxTools.Text = "Tools";
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(102, 31);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(199, 125);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tools";
            // 
            // panel1
            // 
            panel1.Controls.Add(canvas);
            panel1.Location = new Point(113, 178);
            panel1.Name = "panel1";
            panel1.Size = new Size(864, 418);
            panel1.TabIndex = 8;
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.Image = (Image)resources.GetObject("canvas.Image");
            canvas.Location = new Point(3, 3);
            canvas.Name = "canvas";
            canvas.Size = new Size(858, 412);
            canvas.TabIndex = 0;
            canvas.TabStop = false;
            // 
            // groupBoxSize
            // 
            groupBoxSize.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxSize.Location = new Point(307, 31);
            groupBoxSize.Name = "groupBoxSize";
            groupBoxSize.Size = new Size(74, 125);
            groupBoxSize.TabIndex = 11;
            groupBoxSize.TabStop = false;
            groupBoxSize.Text = "Size";
            // 
            // testLabel
            // 
            testLabel.AutoSize = true;
            testLabel.Location = new Point(41, 271);
            testLabel.Name = "testLabel";
            testLabel.Size = new Size(33, 20);
            testLabel.TabIndex = 12;
            testLabel.Text = "test";
            // 
            // figureGroupBox
            // 
            figureGroupBox.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            figureGroupBox.Location = new Point(387, 31);
            figureGroupBox.Name = "figureGroupBox";
            figureGroupBox.Size = new Size(199, 125);
            figureGroupBox.TabIndex = 0;
            figureGroupBox.TabStop = false;
            figureGroupBox.Text = "Figures";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 608);
            Controls.Add(figureGroupBox);
            Controls.Add(testLabel);
            Controls.Add(groupBoxSize);
            Controls.Add(panel1);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxColors);
            Controls.Add(groupBoxTools);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
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
        private ColorGroupBox groupBoxColors;
        private ToolGroupBox groupBoxTools;
        private GroupBox groupBox2;
        private Panel panel1;
        private Canvas canvas;
        private ToolStripMenuItem addImageToolStripMenuItem;
        private SizeGroupBox groupBoxSize;
        private Label testLabel;
        private FigureGroupBox figureGroupBox;
    }
}