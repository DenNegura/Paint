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
            FileMenu = new ToolStripMenuItem();
            SaveFileItem = new ToolStripMenuItem();
            ClearFileItem = new ToolStripMenuItem();
            OpenFileItem = new ToolStripMenuItem();
            groupBoxColors = new ColorGroupBox();
            groupBoxTools = new ToolGroupBox();
            stateGroupBox = new StateGroupBox();
            colorUseLabel = new Label();
            colorUseImage = new Canvas();
            toolUseImage = new Canvas();
            toolUseLabel = new Label();
            canvasContainer = new Panel();
            canvas = new Canvas();
            groupBoxSize = new SizeGroupBox();
            figureGroupBox = new FigureGroupBox();
            statusStrip1 = new StatusStrip();
            sizeCanvasInfo = new ToolStripStatusLabel();
            mousePositionInfo = new ToolStripStatusLabel();
            controlPanel = new Panel();
            delimiterPanel = new Panel();
            formTopPanel = new Panel();
            controlContainer = new Panel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)colorUseImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toolUseImage).BeginInit();
            canvasContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            statusStrip1.SuspendLayout();
            controlPanel.SuspendLayout();
            formTopPanel.SuspendLayout();
            controlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1277, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            FileMenu.DropDownItems.AddRange(new ToolStripItem[] { SaveFileItem, ClearFileItem, OpenFileItem });
            FileMenu.Name = "FileMenu";
            FileMenu.Size = new Size(46, 24);
            FileMenu.Text = "File";
            // 
            // SaveFileItem
            // 
            SaveFileItem.Name = "SaveFileItem";
            SaveFileItem.Size = new Size(224, 26);
            SaveFileItem.Text = "Save";
            SaveFileItem.Click += SaveFileItem_Click;
            // 
            // ClearFileItem
            // 
            ClearFileItem.Name = "ClearFileItem";
            ClearFileItem.Size = new Size(224, 26);
            ClearFileItem.Text = "Clear";
            ClearFileItem.Click += ClearFileItem_Click;
            // 
            // OpenFileItem
            // 
            OpenFileItem.Name = "OpenFileItem";
            OpenFileItem.Size = new Size(224, 26);
            OpenFileItem.Text = "Open";
            OpenFileItem.Click += OpenFileItem_Click;
            // 
            // groupBoxColors
            // 
            groupBoxColors.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxColors.Location = new Point(739, 8);
            groupBoxColors.Name = "groupBoxColors";
            groupBoxColors.Size = new Size(229, 125);
            groupBoxColors.TabIndex = 7;
            groupBoxColors.TabStop = false;
            groupBoxColors.Text = "Colors";
            // 
            // groupBoxTools
            // 
            groupBoxTools.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxTools.Location = new Point(504, 8);
            groupBoxTools.Name = "groupBoxTools";
            groupBoxTools.Size = new Size(229, 125);
            groupBoxTools.TabIndex = 7;
            groupBoxTools.TabStop = false;
            groupBoxTools.Text = "Tools";
            // 
            // stateGroupBox
            // 
            stateGroupBox.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            stateGroupBox.Location = new Point(8, 8);
            stateGroupBox.Name = "stateGroupBox";
            stateGroupBox.Size = new Size(175, 125);
            stateGroupBox.TabIndex = 14;
            stateGroupBox.TabStop = false;
            stateGroupBox.Text = "In use";
            // 
            // colorUseLabel
            // 
            colorUseLabel.Location = new Point(0, 0);
            colorUseLabel.Name = "colorUseLabel";
            colorUseLabel.Size = new Size(100, 23);
            colorUseLabel.TabIndex = 0;
            // 
            // colorUseImage
            // 
            colorUseImage.BackColor = Color.White;
            colorUseImage.Image = (Image)resources.GetObject("colorUseImage.Image");
            colorUseImage.Location = new Point(0, 0);
            colorUseImage.Name = "colorUseImage";
            colorUseImage.Size = new Size(100, 50);
            colorUseImage.TabIndex = 0;
            colorUseImage.TabStop = false;
            // 
            // toolUseImage
            // 
            toolUseImage.BackColor = Color.White;
            toolUseImage.Image = (Image)resources.GetObject("toolUseImage.Image");
            toolUseImage.Location = new Point(0, 0);
            toolUseImage.Name = "toolUseImage";
            toolUseImage.Size = new Size(100, 50);
            toolUseImage.TabIndex = 0;
            toolUseImage.TabStop = false;
            // 
            // toolUseLabel
            // 
            toolUseLabel.Location = new Point(0, 0);
            toolUseLabel.Name = "toolUseLabel";
            toolUseLabel.Size = new Size(100, 23);
            toolUseLabel.TabIndex = 0;
            // 
            // canvasContainer
            // 
            canvasContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasContainer.AutoScroll = true;
            canvasContainer.BackColor = SystemColors.Control;
            canvasContainer.Controls.Add(canvas);
            canvasContainer.Location = new Point(13, 202);
            canvasContainer.Margin = new Padding(15);
            canvasContainer.Name = "canvasContainer";
            canvasContainer.Size = new Size(1254, 431);
            canvasContainer.TabIndex = 8;
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.Image = (Image)resources.GetObject("canvas.Image");
            canvas.Location = new Point(3, 3);
            canvas.Name = "canvas";
            canvas.Size = new Size(1248, 425);
            canvas.TabIndex = 0;
            canvas.TabStop = false;
            // 
            // groupBoxSize
            // 
            groupBoxSize.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxSize.Location = new Point(189, 8);
            groupBoxSize.Name = "groupBoxSize";
            groupBoxSize.Size = new Size(74, 125);
            groupBoxSize.TabIndex = 11;
            groupBoxSize.TabStop = false;
            groupBoxSize.Text = "Size";
            // 
            // figureGroupBox
            // 
            figureGroupBox.Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            figureGroupBox.Location = new Point(269, 8);
            figureGroupBox.Name = "figureGroupBox";
            figureGroupBox.Size = new Size(229, 125);
            figureGroupBox.TabIndex = 0;
            figureGroupBox.TabStop = false;
            figureGroupBox.Text = "Figures";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { sizeCanvasInfo, mousePositionInfo });
            statusStrip1.Location = new Point(0, 644);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1277, 26);
            statusStrip1.TabIndex = 13;
            statusStrip1.Text = "statusStrip1";
            // 
            // sizeCanvasInfo
            // 
            sizeCanvasInfo.Name = "sizeCanvasInfo";
            sizeCanvasInfo.Size = new Size(82, 20);
            sizeCanvasInfo.Text = "size canvas";
            // 
            // mousePositionInfo
            // 
            mousePositionInfo.Name = "mousePositionInfo";
            mousePositionInfo.Size = new Size(111, 20);
            mousePositionInfo.Text = "mouse position";
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.None;
            controlPanel.Controls.Add(stateGroupBox);
            controlPanel.Controls.Add(groupBoxSize);
            controlPanel.Controls.Add(figureGroupBox);
            controlPanel.Controls.Add(groupBoxColors);
            controlPanel.Controls.Add(groupBoxTools);
            controlPanel.Location = new Point(3, 3);
            controlPanel.Name = "controlPanel";
            controlPanel.Padding = new Padding(5);
            controlPanel.Size = new Size(974, 140);
            controlPanel.TabIndex = 15;
            // 
            // delimiterPanel
            // 
            delimiterPanel.BackColor = SystemColors.ControlLight;
            delimiterPanel.Dock = DockStyle.Bottom;
            delimiterPanel.Location = new Point(10, 154);
            delimiterPanel.Name = "delimiterPanel";
            delimiterPanel.Size = new Size(1257, 2);
            delimiterPanel.TabIndex = 16;
            // 
            // formTopPanel
            // 
            formTopPanel.Controls.Add(controlContainer);
            formTopPanel.Controls.Add(delimiterPanel);
            formTopPanel.Dock = DockStyle.Top;
            formTopPanel.Location = new Point(0, 28);
            formTopPanel.Name = "formTopPanel";
            formTopPanel.Padding = new Padding(10, 5, 10, 5);
            formTopPanel.Size = new Size(1277, 161);
            formTopPanel.TabIndex = 17;
            // 
            // controlContainer
            // 
            controlContainer.Controls.Add(controlPanel);
            controlContainer.Dock = DockStyle.Fill;
            controlContainer.Location = new Point(10, 5);
            controlContainer.Name = "controlContainer";
            controlContainer.Size = new Size(1257, 149);
            controlContainer.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1277, 670);
            Controls.Add(formTopPanel);
            Controls.Add(statusStrip1);
            Controls.Add(canvasContainer);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)colorUseImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)toolUseImage).EndInit();
            canvasContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            controlPanel.ResumeLayout(false);
            formTopPanel.ResumeLayout(false);
            controlContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileMenu;
        private ToolStripMenuItem SaveFileItem;
        private ToolStripMenuItem ClearFileItem;
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
        private StateGroupBox stateGroupBox;
        private Panel canvasContainer;
        private Canvas canvas;
        private ToolStripMenuItem OpenFileItem;
        private SizeGroupBox groupBoxSize;
        private FigureGroupBox figureGroupBox;
        private StatusStrip statusStrip1;
        private Label toolUseLabel;
        private Label colorUseLabel;
        private Canvas colorUseImage;
        private Canvas toolUseImage;
        private Panel controlPanel;
        private Panel delimiterPanel;
        private Panel formTopPanel;
        private Panel controlContainer;
        private ToolStripStatusLabel sizeCanvasInfo;
        private ToolStripStatusLabel mousePositionInfo;
    }
}