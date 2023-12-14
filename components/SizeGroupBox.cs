using Paint.Properties;
using System.Text.RegularExpressions;

namespace Paint.components
{
    internal class SizeGroupBox : GroupBox
    {
        private static string[] DEFOULT_VALUES = new string[] { "1px", "3px", "5px", "8px" };

        PictureBox imageBox = new PictureBox();

        ComboBox sizeCombobox = new ComboBox();

        public event EventHandler OnSelect;

        public SizeGroupBox() 
        {
            InitializeComponents();
            IntializeSizeCombobox();
        }

        private void InitializeComponents()
        {
            SuspendLayout();

            // 
            // sizeCombobox
            // 
            sizeCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            sizeCombobox.FlatStyle = FlatStyle.Popup;
            sizeCombobox.FormattingEnabled = true;
            sizeCombobox.Location = new Point(6, 89);
            sizeCombobox.Name = "sizeBox";
            sizeCombobox.Size = new Size(60, 30);

            sizeCombobox.SelectedIndexChanged += OnSelect_Change;
            // 
            // imageBox
            // 
            imageBox.BackgroundImage = Properties.Resources.Brush;
            imageBox.InitialImage = Properties.Resources.Brush;
            imageBox.Location = new Point(6, 26);
            imageBox.Name = "imageBox";
            imageBox.Size = new Size(62, 62);
            imageBox.TabStop = false;

            Controls.Add(sizeCombobox);
            Controls.Add(imageBox);
            Font = new Font("Cascadia Mono", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            Location = new Point(217, 31);
            Name = "groupBoxSize";
            Size = new Size(74, 125);
            TabStop = false;
            Text = "Size";

            ResumeLayout(false);
        }

        private void IntializeSizeCombobox()
        {
            foreach(string size in DEFOULT_VALUES)
            {
                sizeCombobox.Items.Add(size);
            }
            sizeCombobox.SelectedItem = sizeCombobox.Items[1];
        }

        private void OnSelect_Change(object sender, EventArgs e)
        {
            int? selectedSize = ValueToNumber(sizeCombobox.SelectedItem.ToString());
            if(OnSelect != null)
            {
                OnSelect.Invoke(selectedSize, EventArgs.Empty);
            }
        }

        private int ValueToNumber(string? value)
        {
            if(value != null)
            {
                Match match = Regex.Match(value, @"\d+");

                if (match.Success)
                {
                    if (int.TryParse(match.Value, out int result))
                    {
                        return result;
                    }
                }
            }

            return 3;
        } 

        public int GetSize()
        {
            return ValueToNumber(sizeCombobox.SelectedItem.ToString());
        }
    }
}
