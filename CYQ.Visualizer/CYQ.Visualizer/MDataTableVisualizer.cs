using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;
using System.Drawing;
using CYQ.Data.Table;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using CYQ.Visualizer;


[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(CYQ.Visualizer.MDataTableVisualizer),
typeof(EnumerableVisualizerObjectSource),
Target = typeof(CYQ.Data.Table.MDataTable),
Description = "MDataTable Visualizer")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(CYQ.Visualizer.MDataRowVisualizer),
typeof(EnumerableVisualizerObjectSource),
Target = typeof(CYQ.Data.Table.MDataRow),
Description = "MDataRow Visualizer")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(CYQ.Visualizer.MDataColumnVisualizer),
typeof(EnumerableVisualizerObjectSource),
Target = typeof(CYQ.Data.Table.MDataColumn),
Description = "MDataColumn Visualizer")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(CYQ.Visualizer.MDataRowCollectionVisualizer),
typeof(EnumerableVisualizerObjectSource),
Target = typeof(CYQ.Data.Table.MDataRowCollection),
Description = "MDataRowCollection Visualizer")]

namespace CYQ.Visualizer
{

    public class MDataTableVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MDataTable dt = objectProvider.GetObject() as DataTable;
            FormCreate.BindTable(windowService, dt, null);
        }
    }
    public class MDataRowVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MDataTable dt = objectProvider.GetObject() as DataTable;
            string title = string.Format("TableName : {0}    Columns£º {1}", dt.TableName, dt.Columns.Count);
            FormCreate.BindTable(windowService, dt, title);
        }
    }
    public class MDataColumnVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MDataTable dt = objectProvider.GetObject() as DataTable;
            string title = string.Format("TableName : {0}    Columns£º {1}", dt.TableName, dt.Columns.Count);
            FormCreate.BindTable(windowService, dt, title);
        }
    }
    public class MDataRowCollectionVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MDataTable dt = objectProvider.GetObject() as DataTable;
            //MDataTable dt = objectProvider.GetObject() as MDataRowCollection;
            FormCreate.BindTable(windowService, dt, null);
        }
    }


}
