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
            Button selectButton = button;
            selectButton.Location = new Point(6, 30);
            selectButton.Name = "buttonSelect";
            selectButton.FlatStyle = FlatStyle.Standard;
            selectButton.Size = new Size(32, 32);
            Controls.Add(selectButton);
            return selectButton;
        }

        protected Button initPreviousButton(Button button)
        {

            Button previousButton = button;
            previousButton.Location = new Point(6, 76);
            previousButton.Name = "buttonPevious";
            previousButton.Size = new Size(32, 32);
            previousButton.FlatStyle = FlatStyle.Standard;
            Controls.Add(previousButton);
            return previousButton;
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
