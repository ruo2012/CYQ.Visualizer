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
using System.Diagnostics;
/*
[assembly: DebuggerVisualizer(typeof(MDataRowVisualizer),typeof(DataRowVisualizerObjectSource),Target = typeof(DataRow),Description = "DataRow Visualizer")]
[assembly: DebuggerVisualizer(typeof(MDataRowCollectionVisualizer), typeof(DataRowCollectionVisualizerObjectSource), Target = typeof(DataRowCollection), Description = "DataRowCollection Visualizer")]
[assembly: DebuggerVisualizer(typeof(MDataColumnVisualizer), typeof(DataColumnCollectionVisualizerObjectSource), Target = typeof(DataColumnCollection), Description = "DataColumnCollection Visualizer")]

namespace CYQ.Visualizer
{
    public class DataRowVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            MDataRow row = target as DataRow;
            base.GetData(row, outgoingData);
        }
    }
    public class DataColumnCollectionVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            MDataColumn mdc = target as DataColumnCollection;
            base.GetData(mdc, outgoingData);
        }
    }
    public class DataRowCollectionVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            MDataRowCollection mrc = target as DataRowCollection;
            base.GetData(mrc, outgoingData);
        }
    }
    
}
 * */
