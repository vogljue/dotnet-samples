// Simple Encoder Utilities in C# (Tutorial).
using System;
using System.Data;
using System.IO;

namespace Carservice.Utilities
{
    /// <summary>
    /// Represents the UvgtFormatter class.
    /// </summary>
    class UvgtFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return this;
        }
    
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            DataRow dataRow = (DataRow)arg;
            return Convert.ToString(dataRow[format]);
        }
    }

    /// <summary>
    /// Represents the UvgtSortKey class for custom sorting.
    /// </summary>
    class UvgtSortKey : IComparable
    {
        string sortKey; 
        
        public UvgtSortKey(string sortKey) 
        {
            this.sortKey = sortKey;
        }
        
        public override string ToString() {
            return this.sortKey;
        }

        public int CompareTo(object obj) {
            if (obj == null) return (1);
            
            UvgtSortKey otherSortKey = (UvgtSortKey) obj;
            return this.sortKey.CompareTo(otherSortKey.sortKey);
        }      
    }
    
    /// <summary>
    /// Represents the UvgtGeneratorUtils class.
    /// </summary>
    public class UvgtGeneratorUtils
    {
        public static string BuildUvgtDataSet() 
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable("uvgt");
            dataSet.Tables.Add(table);

            DataColumn column;
            column = new DataColumn("sortkey", typeof(UvgtSortKey));
            table.Columns.Add(column);
            column = new DataColumn("bbnr-uv", typeof(String));
            table.Columns.Add(column);
            column = new DataColumn("gts-nr", typeof(String));
            table.Columns.Add(column);
            column = new DataColumn("gts-name", typeof(String));
            table.Columns.Add(column);
            
            DataRow row = table.NewRow();
            row["sortkey"] = new UvgtSortKey("12345678-01-AAA");
            row["bbnr-uv"] = "12345678";
            row["gts-nr"] = "01-AAA";
            row["gts-name"] = "Gefahrtarifstelle-AAA";
            table.Rows.Add(row);
            row = table.NewRow();
            row["sortkey"] = new UvgtSortKey("12345678-01-222");
            row["bbnr-uv"] = "12345678";
            row["gts-nr"] = "01-222";
            row["gts-name"] = "Gefahrtarifstelle-222";
            table.Rows.Add(row);
            row = table.NewRow();
            row["sortkey"] = new UvgtSortKey("12345678-01-111");
            row["bbnr-uv"] = "12345678";
            row["gts-nr"] = "01-111";
            row["gts-name"] = "GTSTest-111";
            table.Rows.Add(row);
            
            table.DefaultView.Sort = "sortkey";
            DataTable sortedTable = table.DefaultView.ToTable();
            
            string template = "BBNR {0:bbnr-uv}. GTS-NR {0:gts-nr}. GTS-NAME {0:gts-name}. BBNR-WH {0:bbnr-uv}.";
            string content = String.Empty;
            UvgtFormatter formatter = new UvgtFormatter();
            using (StringWriter writer = new StringWriter())
            {
                foreach(DataRow r in sortedTable.Rows)
                {
                    string line = String.Format(formatter, template, r);
                    writer.WriteLine(line);
                }
                content = writer.ToString();
            }
            return content;
        }
    }
}