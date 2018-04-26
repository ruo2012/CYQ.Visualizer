using CYQ.Data.Table;
using CYQ.Visualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CYQ.Visualizer
{
    public class EnumerableVisualizer : DialogDebuggerVisualizer
    {
        public const string Description = "Enumerable Visualizer";
        override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MDataTable dt = objectProvider.GetObject() as DataTable;
            if (dt != null)
            {
                try
                {
                    FormCreate.BindTable(windowService, dt, null);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }
        }
    }
    public class EnumerableVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, System.IO.Stream outgoingData)
        {
            MDataTable dt = null;
            #region 类型判断

           
            if (target is MDataTable)
            {
                dt = target as MDataTable;
            }
            else if (target is MDataRow)
            {
                dt = ((MDataRow)target).ToTable();
            }
            else if (target is MDataColumn)
            {
                dt = ((MDataColumn)target).ToTable();
            }
            else if (target is MDataRowCollection)
            {
                dt = target as MDataRowCollection;
            }
            else if (target is DataRow)
            {
                MDataRow row = target as DataRow;
                dt = row.ToTable();
            }
            else if (target is DataColumnCollection)
            {
                MDataColumn mdc = target as DataColumnCollection;
                dt = mdc.ToTable();
            }
            else if (target is DataRowCollection)
            {
                MDataRowCollection rows = target as DataRowCollection;
                dt = rows;
            }
            else if (target is NameObjectCollectionBase)
            {
                dt = MDataTable.CreateFrom(target as NameObjectCollectionBase);
            }
            else if (target is IEnumerable)
            {
                dt = MDataTable.CreateFrom(target as IEnumerable);
            }
            else
            {
                dt = MDataTable.CreateFrom(target);
                if (dt == null)
                {
                    MDataRow row = MDataRow.CreateFrom(target);
                    if (row != null)
                    {
                        dt = row.ToTable();
                    }
                }
            }
            #endregion
            dt = Format(dt);
            if (dt != null)
            {
                base.GetData(dt.ToDataTable(), outgoingData);
            }
            else
            {

                base.GetData(new DataTable("Empty Table"), outgoingData);
            }
        }
        private MDataTable Format(MDataTable dt)
        {
            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].SqlType == System.Data.SqlDbType.Variant)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            if (!dt.Rows[k][i].IsNull)
                            {
                                dt.Rows[k][i].Value = dt.Rows[k][i].ToString();
                            }
                        }
                        dt.Columns[i].SqlType = System.Data.SqlDbType.NVarChar;//避开未序列化的对象。
                    }
                }
            }
            return dt;
        }
    }
}
