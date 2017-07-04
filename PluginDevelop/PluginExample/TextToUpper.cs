using PluginStandard;
using System.Windows.Forms;

namespace PluginExample
{
    public class TextToUpper : IPluginStandard
    {
        public string RootMenuStripName
        {
            get
            {
                return "Format";
            }
        }
        public string MenuStripName
        {
            get
            {
                return "TextToUpper";
            }
        }
        public void Operation(RichTextBox richTextBox)
        {
            if (!string.IsNullOrEmpty(richTextBox.Text))
            {
                richTextBox.Text = richTextBox.Text.ToUpper();
            }
        }
    }
}
