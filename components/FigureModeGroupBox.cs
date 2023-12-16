using Paint.components.Drawable;
using Paint.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint.components
{
    internal class FigureModeGroupBox : CustomGroupBox
    {
        private static readonly string TITLE = "Fill";

        private static readonly Image FILL_FIGURES = Resources.FillFigures;

        private static readonly Image EMPTY_FIGURES = Resources.EmptyFigures;

        private static readonly Image NO_BORDER_FIGURES = Resources.Figures;

        ImageButton buttonFillBorder;

        ImageButton buttonBorder;

        ImageButton buttonFill;

        private Figure.Mode selectMode = Figure.Mode.BORDER;

        public event EventHandler? OnFigureModeChanged;

        public FigureModeGroupBox(): base(TITLE) {

            buttonFillBorder = (ImageButton)initSelectButton(new ImageButton());
            buttonFillBorder.Image = FILL_FIGURES;

            buttonBorder = (ImageButton) initPreviousButton(new ImageButton());
            buttonBorder.Image = EMPTY_FIGURES;

            buttonFill = (ImageButton)initPreviousButton(new ImageButton());
            buttonFill.Image = NO_BORDER_FIGURES;
            buttonFill.Location = new Point(45, 52);

            buttonFillBorder.Click += (object? sender, EventArgs e) => selectMode = Figure.Mode.FILL_BORDER;
            buttonBorder.Click += (object? sender, EventArgs e) => selectMode = Figure.Mode.BORDER;
            buttonFill.Click += (object? sender, EventArgs e) => selectMode = Figure.Mode.FILL;

            buttonFillBorder.Click += OnFigureModeChanged_Event;
            buttonBorder.Click += OnFigureModeChanged_Event;
            buttonFill.Click += OnFigureModeChanged_Event;
        } 

        private void OnFigureModeChanged_Event(object? sender, EventArgs e)
        {
            OnFigureModeChanged?.Invoke(selectMode, EventArgs.Empty);
        }
    }
}
