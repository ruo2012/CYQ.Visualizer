using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYQ.VisualizerDemo
{
    class DebuggerSide
    {
        public static void TestShowVisualizer(object objectToVisualize)
        {
            string[] item = "a,b,c".Split(',');
            Console.WriteLine(item);
        }

    }
}
