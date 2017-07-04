using PluginStandard;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "plugins");
            string[] files = Directory.GetFiles(path, "*.dll");
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    Type[] types = assembly.GetTypes();
                    if (types != null && types.Length > 0)
                    {
                        foreach (Type type in types)
                        {
                            if (!type.IsAbstract && typeof(IPluginStandard).IsAssignableFrom(type))
                            {
                                IPluginStandard plugin = Activator.CreateInstance(type) as IPluginStandard;
                                ToolStripMenuItem toolStripMenuItem = GetToolStripMenuItem(plugin.RootMenuStripName);
                                if (toolStripMenuItem != null)
                                {
                                    ToolStripItem toolStripItem = toolStripMenuItem.DropDownItems.Add(plugin.MenuStripName);
                                    toolStripItem.Tag = plugin;
                                    toolStripItem.Click += ToolStripItem_Click;
                                }
                            }
                        }
                    }
                }
            }
        }
        private ToolStripMenuItem GetToolStripMenuItem(string name)
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
        private void ToolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripItem toolStripItem = sender as ToolStripItem;
            IPluginStandard plugin = toolStripItem.Tag as IPluginStandard;
            plugin.Operation(richTextBox1);
        }
    }
}
