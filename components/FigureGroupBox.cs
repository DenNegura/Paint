﻿using Paint.components.Drawable;
using Paint.Properties;

namespace Paint.components
{
    internal class FigureGroupBox: CustomGroupBox
    {
       
        private static readonly string TITLE = "Figures";

        private static readonly Figure DEFAULT_SELECT_FIGURE = Figure.LINE;

        private static readonly Figure DEFAULT_PREVIOUS_FIGURE = Figure.RECTANGLE;

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
            foreach (Figure figure in Figure.FIGURES)
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
            OnSelect.Invoke(((ImageButton)sender).Tag as Figure, EventArgs.Empty);
            //Figure? figure = ((ImageButton)sender).Tag as Figure;
            //if (figure != null)
            //{
            //    OnSelect.Invoke(figure.drawable, e);
            //}
        }

        public Figure? GetSelectFigure()
        {
            return selectButton.Tag as Figure;
        }
    }
}
