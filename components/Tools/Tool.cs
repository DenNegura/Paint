using Paint.Properties;

namespace Paint.components.Tools
{
    internal class Tool
    {
        public readonly EnumTool tool;

        public readonly Image image;

        public readonly string name;

        public Tool(Image image, EnumTool tool, string name)
        {
            this.tool = tool;
            this.image = image;
            this.name = name;
        }

        public static readonly Tool PEN = 
            new Tool(Resources.Pen, EnumTool.PEN, "Pen");

        public static readonly Tool ERASER = 
            new Tool(Resources.Eraser, EnumTool.ERASER, "Eraser");

        public static readonly Tool FILL = 
            new Tool(Resources.Fill, EnumTool.FILL, "Fill");

        public static readonly Tool COLOUR_PICKER = 
            new Tool(Resources.ColourPicker, EnumTool.COLOUR_PICKER, "Colour picker");


        public static readonly Tool[] TOOLS = new Tool[] {
            PEN, ERASER, FILL, COLOUR_PICKER,
        };
    }
}
