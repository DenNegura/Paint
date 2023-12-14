using System.Xml.Serialization;

namespace Paint.components
{
    internal class CustomGroupBox : GroupBox
    {
        public CustomGroupBox(String title) 
        {
            Text = title;
            Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            ResumeLayout(false);
        }

        protected Button initSelectButton(Button button)
        {
            button.Location = new Point(6, 30);
            button.Name = "buttonSelect";
            button.FlatStyle = FlatStyle.Standard;
            button.Size = new Size(32, 32);
            Controls.Add(button);
            return button;
        }

        protected Button initPreviousButton(Button button)
        {
            button.Location = new Point(6, 76);
            button.Name = "buttonPevious";
            button.Size = new Size(32, 32);
            button.FlatStyle = FlatStyle.Standard;
            Controls.Add(button);
            return button;
        }

        protected FlowLayoutPanel initLayout(FlowLayoutPanel boxPanel)
        {
            FlowLayoutPanel box = boxPanel;
            box.AutoScroll = true;
            box.Location = new Point(48, 27);
            box.Name = "box";
            box.Size = new Size(175, 92);
            box.ResumeLayout(false);
            Controls.Add(box);
            return box;
        }
    }
}
