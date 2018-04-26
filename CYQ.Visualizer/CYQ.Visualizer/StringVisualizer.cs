using CYQ.Data.Table;
using CYQ.Data.Tool;
using CYQ.Visualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

[assembly: DebuggerVisualizer(typeof(StringVisualizer),typeof(VisualizerObjectSource),Target = typeof(System.String),Description = "JSON Visualizer")]
namespace CYQ.Visualizer
{
    public class StringVisualizer : DialogDebuggerVisualizer
    {
        override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            string json = objectProvider.GetObject() as string;
            MDataTable dt = MDataTable.CreateFrom(JsonHelper.ToJson(json));
            FormCreate.BindTable(windowService, dt, null);
        }
    }
}
