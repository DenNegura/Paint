using Paint.components.Drawable;
using Paint.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.components
{
    internal class FigureGroupBox: CustomGroupBox
    {
        private class Figure
        {
            public readonly Image image;

            public readonly IDrawable drawable;

            public Figure(Image image, IDrawable drawable)
            {
                this.image = image;
                this.drawable = drawable;
            }
        }

        private static readonly Figure[] DEFAULT_FIGURES = new Figure[]
        {
            new Figure(Resources.Line, new Line()),
            new Figure(Resources.Rectangle, new Drawable.Rectangle()),
            new Figure(Resources.Circle, new Drawable.Rectangle())
        };

        private static readonly string TITLE = "Figures";

        private static readonly Figure DEFAULT_SELECT_FIGURE = 
            new Figure(Resources.Line, new Line());

        private static readonly Figure DEFAULT_PREVIOUS_FIGURE = 
            new Figure(Resources.Rectangle, new Drawable.Rectangle());

        private ImageButton selectButton;

        private ImageButton previousButton;

        private FlowLayoutPanel layout;

        public event EventHandler OnSelect;

        public FigureGroupBox(): base(TITLE)
        {
            selectButton = (ImageButton) initSelectButton(new ImageButton());
            previousButton = (ImageButton) initPreviousButton(new ImageButton());

            layout = initLayout(new FlowLayoutPanel());

            selectButton.Image = DEFAULT_SELECT_FIGURE.image;
            selectButton.Tag = DEFAULT_SELECT_FIGURE;
            selectButton.Click += OnSelect_Click;

            previousButton.Image = DEFAULT_PREVIOUS_FIGURE.image;
            previousButton.Tag = DEFAULT_PREVIOUS_FIGURE;
            previousButton.Click += OnSelect_Click;
            previousButton.Click += ChangeFigure_Click;

            InitDefalutValues();
        }

        private void InitDefalutValues()
        {
            foreach (Figure figure in DEFAULT_FIGURES)
            {
                ImageButton imageButton = new ImageButton();
                imageButton.Image = figure.image;
                imageButton.Tag = figure;
                imageButton.Click += ChangeFigure_Click;
                imageButton.Click += OnSelect_Click;
                layout.Controls.Add(imageButton);
            }
        }

        private void ChangeFigure_Click(object sender, EventArgs e)
        {
            Figure? figure = ((ImageButton)sender).Tag as Figure;
            if (figure != null && (selectButton.Tag as Figure).drawable.GetType() != figure.drawable.GetType())
            {
                previousButton.Image = selectButton.Image;
                previousButton.Tag = selectButton.Tag;
                selectButton.Image = figure.image;
                selectButton.Tag = figure;
            }

        }

        private void OnSelect_Click(object sender, EventArgs e)
        {
            Figure? figure = ((ImageButton)sender).Tag as Figure;
            if (figure != null)
            {
                OnSelect.Invoke(figure.drawable, e);
            }
        }
    }
}
