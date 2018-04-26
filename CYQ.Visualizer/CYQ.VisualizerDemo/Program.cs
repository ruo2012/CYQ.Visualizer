using CYQ.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CYQ.VisualizerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TestListDictionary();
            Console.Read();
        }
        static void TestListDictionary()
        {
            string abc = "aaaaaaaaaabc";
            List<string> s = "2,2,3,2,4,3,1,2".Split(',').OrderBy(p => p).Distinct().ToList();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("1", "a");
            dic.Add("2", s);
            dic.Add("3", "a");
            dic.Add("4", "a");
            string json = abc;
            MDataTable dt = MDataTable.CreateFrom(dic);
            Entity e = new Entity();
            e.ID = 1111;
            e.Name = "b";
            object o = e;
            System.Object ooo = new object();
            ooo = e;
            MDataRow row = MDataRow.CreateFrom(e);
 
            Console.Read();

            
        }
    }
    public class Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
