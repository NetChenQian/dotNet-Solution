using System.Windows.Forms;

namespace PluginStandard
{
    public interface IPluginStandard
    {
        string RootMenuStripName { get; }
        string MenuStripName { get; }
        void Operation(RichTextBox richTextBox);
    }
}
