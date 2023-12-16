using Paint.Properties;

namespace Paint.components.Tools
{
    internal class Tool
    {
        public readonly EnumTool tool;

        public readonly Image image;

        public readonly string name;

        public readonly bool hasPen;

        public readonly bool hasBrush;

        public Tool(Image image, EnumTool tool, string name, bool hasPen, bool hasBrush)
        {
            this.tool = tool;
            this.image = image;
            this.name = name;
            this.hasPen = hasPen;
            this.hasBrush = hasBrush;
        }

        public static readonly Tool PEN = 
            new Tool(Resources.Pen, EnumTool.PEN, "Pen", true, false);

        public static readonly Tool ERASER = 
            new Tool(Resources.Eraser, EnumTool.ERASER, "Eraser", true, false);

        public static readonly Tool FILL = 
            new Tool(Resources.Fill, EnumTool.FILL, "Fill", false, true);

        public static readonly Tool COLOUR_PICKER = 
            new Tool(Resources.ColourPicker, EnumTool.COLOUR_PICKER, "Colour picker", false, false);

        public static readonly Tool TEXT =
            new Tool(Resources.Text, EnumTool.TEXT, "Text", true, false);


        public static readonly Tool[] TOOLS = new Tool[] {
            PEN, ERASER, FILL, COLOUR_PICKER, TEXT,
        };
    }
}
