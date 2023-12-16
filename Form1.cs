using Paint.components.Drawable;
using Paint.components.Tools;
using Paint.Properties;

namespace Paint
{
    public partial class Form1 : Form
    {

        private bool isColourPicker = false;

        private static readonly string Q_SAVE_CANVAS = "Сохранить изображение?";

        private static readonly string TITLE = "Paint 0.1.1 Alfa";

        public Form1()
        {
            InitializeComponent();
            Text = TITLE;
            Icon = new Icon(Icon.FromHandle(Resources.Pen.GetHicon()), 24, 24);

            mousePositionInfo.Text = "";
            sizeCanvasInfo.Text = "";

            this.KeyDown += Form_KeyDown;
            this.KeyPreview = true;

            figureGroupBox.OnSelect += OnSelectFigure;

            groupBoxTools.OnSelect += OnSelectTool;

            groupBoxColors.OnSelect += OnSelectColor;

            groupBoxSize.OnSelect += OnSelectSize;

            fillFigureGroupBox.OnFigureModeChanged += OnFillChanged;

            stateGroupBox.OnPenColorChanged += OnPenColorChanged;
            stateGroupBox.OnBrushColorChanged += OnBrushColorChanged;

            fontGroupBox.OnChangeFont += OnChangeFont;

            canvas.OnMoveMouse += OnMoveMouse;
            canvas.OnResize += OnResizeCanvas;
            canvas.OnSelectPixel += OnSelectPixel;

            canvas.setMaxHeight(canvasContainer.Height);
            canvas.setMaxWight(canvasContainer.Width);
            OnResizeCanvas(canvas.Size, EventArgs.Empty);

            canvas.SetDrawableObject(new Polyline());
            canvas.SetPen(new Pen(groupBoxColors.getCurrentColor(), groupBoxSize.GetSize()));

            stateGroupBox.SetTool(Tool.PEN);
            canvas.SetFont(fontGroupBox.GetFont());
            stateGroupBox.SetPenColor(groupBoxColors.getCurrentColor());
            stateGroupBox.SetBrushColor(groupBoxColors.getCurrentColor());

            Resize += Form_Resize;
            ReplaceControlPanel();
            canvas.ClearHistory();
        }

        // изменение размера формы
        private void Form_Resize(object sender, EventArgs e)
        {
            ReplaceControlPanel();
            canvas.setMaxHeight(canvasContainer.Height);
            canvas.setMaxWight(canvasContainer.Width);
            sizeCanvasInfo.Text = $"{canvas.Size.Width} x {canvas.Size.Height}";
        }

        // перемещение панели инструментов в центр
        private void ReplaceControlPanel()
        {
            controlPanel.Location = new Point((controlContainer.Width - controlPanel.Width) / 2, 0);
        }

        private void OnChangeFont(object? sender, EventArgs e)
        {
            Font? font = (Font?)sender;
            if (font != null)
            {
                canvas.SetFont(font);
            }

        }
        private void OnFillChanged(object sender, EventArgs e)
        {
            if (stateGroupBox.hasFigure())
            {
                Figure.Mode mode = (Figure.Mode)sender;
                if (mode == Figure.Mode.FILL)
                {
                    stateGroupBox.IsBrushDisabled(false);
                    stateGroupBox.IsPenDisabled(true);
                    canvas.SetPenColor(stateGroupBox.GetBrushColor());
                }
                else if (mode == Figure.Mode.BORDER)
                {
                    stateGroupBox.IsBrushDisabled(true);
                    stateGroupBox.IsPenDisabled(false);
                }
                else if (mode == Figure.Mode.FILL_BORDER)
                {
                    stateGroupBox.IsBrushDisabled(false);
                    stateGroupBox.IsPenDisabled(false);
                }
                stateGroupBox.SetBrushColor(groupBoxColors.getCurrentColor());
                stateGroupBox.SetPenColor(groupBoxColors.getCurrentColor());
            }
        }

        private void OnPenColorChanged(object sender, EventArgs e)
        {
            Color color = (Color)sender;
            if (!color.IsEmpty)
            {
                canvas.SetPenColor(color);
            }
        }

        private void OnBrushColorChanged(object sender, EventArgs e)
        {
            Color color = (Color)sender;
            if (!color.IsEmpty)
            {
                canvas.SetBrush(new SolidBrush(color));
            }
            else
            {
                canvas.SetBrush(null);
            }
        }

        // слушатель на изменение размера канваса
        private void OnResizeCanvas(object sender, EventArgs e)
        {
            Size size = (Size)sender;
            if (size != Size.Empty)
            {
                sizeCanvasInfo.Text = $"{size.Width} x {size.Height}";
            }
            else
            {
                sizeCanvasInfo.Text = "";
            }
        }

        // слушатель на перемещение мыши по канвасу
        private void OnMoveMouse(object sender, EventArgs e)
        {
            Point mousePos = (Point)sender;
            if (mousePos != Point.Empty)
            {
                mousePositionInfo.Text = $"{mousePos.X} {mousePos.Y}";
            }
            else
            {
                mousePositionInfo.Text = "";
            }
        }

        // слушатель на выбор размера пера
        private void OnSelectSize(object sender, EventArgs e)
        {
            int size = (int)sender;
            canvas.SetPenSize(size);
        }

        // слушатель на выбор фигуры 
        private void OnSelectFigure(object sender, EventArgs e)
        {
            isColourPicker = false;

            Figure figure = (Figure)sender;

            stateGroupBox.SetFigure(figure);
            stateGroupBox.SetPenColor(groupBoxColors.getCurrentColor());
            stateGroupBox.SetBrushColor(groupBoxColors.getCurrentColor());

            canvas.SetFillMode(false);
            canvas.SetDrawableObject(figure.drawable);
        }

        // слушатель на выбор инструмента
        private void OnSelectTool(object sender, EventArgs e)
        {
            Tool tool = (Tool)sender;
            canvas.SetFillMode(false);
            stateGroupBox.SetTool(tool);
            stateGroupBox.SetPenColor(groupBoxColors.getCurrentColor());
            stateGroupBox.SetBrushColor(groupBoxColors.getCurrentColor());
            isColourPicker = false;
            switch (tool.tool)
            {
                case EnumTool.ERASER:
                    canvas.SetDrawableObject(new Polyline());
                    break;
                case EnumTool.COLOUR_PICKER:
                    isColourPicker = true;
                    canvas.SetFillMode(false);
                    canvas.SetDrawableObject(null);
                    break;
                case EnumTool.PEN:
                    canvas.SetDrawableObject(new Polyline());
                    break;
                case EnumTool.FILL:
                    canvas.SetFillMode(true);
                    break;
                case EnumTool.TEXT:
                    canvas.SetTextMode(true);
                    break;
                default:
                    throw new ArgumentException($"{tool.tool} not faund!");
            }
        }

        // слушатель но нажатие на канвас
        private void OnSelectPixel(object sender, EventArgs e)
        {
            if (isColourPicker)
            {
                Color color = (Color)sender;
                groupBoxColors.setCurrentColor(color);
                canvas.SetPenColor(color);
                stateGroupBox.SetPenColor(color);
                stateGroupBox.SetBrushColor(color);
            }

        }

        // Слушатель на выбор цвета
        private void OnSelectColor(object sender, EventArgs e)
        {
            stateGroupBox.SetPenColor((Color)sender);
            stateGroupBox.SetBrushColor((Color)sender);
        }

        // слкшвтель на нажатие на клавиатуру
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                canvas.Redo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                canvas.Undo();
            }
        }

        // очищение формы
        private void ClearFileItem_Click(object sender, EventArgs e)
        {
            if (!canvas.isEmpty())
            {
                DialogResult result = OpenQiestionDialog(Q_SAVE_CANVAS);
                if (result == DialogResult.Yes)
                {
                    SaveFileItem_Click(sender, e);
                }
                else if (result == DialogResult.No)
                {
                    canvas.Clear();
                }
            }
        }

        // Открытие диалогового окна
        private DialogResult OpenQiestionDialog(string question)
        {
            return MessageBox.Show(
                question,
                "Question",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        // сохранение изображения
        private void SaveFileItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Изображения (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Сохранить изображение";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                canvas.Save(filePath);
            }
        }

        // открытие изображения
        private void OpenFileItem_Click(object sender, EventArgs e)
        {
            if (!canvas.isEmpty())
            {
                DialogResult result = OpenQiestionDialog(Q_SAVE_CANVAS);
                if (result == DialogResult.Yes)
                {
                    SaveFileItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    canvas.SetImage(Image.FromFile(filePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть изображение. Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}