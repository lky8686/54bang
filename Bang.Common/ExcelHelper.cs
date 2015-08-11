using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.IO;

namespace Bang.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出数据为Excel
        /// <para>colName为可选，如果指定列名须遵循格式：字段名$列名,字段名$列名,……</para>
        /// </summary>
        /// <param name="source">数据</param>
        /// <param name="colName">列名</param>
        public static byte[] DataToExcel<T>(IList<Dictionary<string, T>> source, string colName = null)
        {
            IList<string> fieldName = new List<string>();
            IList<string> fieldTitle = new List<string>();
            ParseName(source, colName, fieldName, fieldTitle);

            HSSFWorkbook workBook = new HSSFWorkbook();
            HSSFSheet sheet = workBook.CreateSheet();
            HSSFRow header = sheet.CreateRow(0);
            HSSFCellStyle cellStyle = workBook.CreateCellStyle();
            cellStyle.FillBackgroundColor = HSSFColor.BLACK.index;
            cellStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
            HSSFFont font = workBook.CreateFont();
            font.Boldweight = 600;
            font.Color = HSSFColor.WHITE.index;
            cellStyle.SetFont(font);

            //创建列名行
            for (var i = 0; i < fieldTitle.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(fieldTitle[i]);
                cell.CellStyle = cellStyle;
            }

            //创建数据行
            for (var j = 0; j < source.Count; j++)
            {
                HSSFRow row = sheet.CreateRow(j + 1);
                for (var k = 0; k < fieldName.Count; k++)
                {
                    if (source[j].ContainsKey(fieldName[k]))
                    {
                        row.CreateCell(k).SetCellValue(source[j][fieldName[k]].ToString());
                    }
                }
            }

            using (MemoryStream stream = new MemoryStream())
            {
                workBook.Write(stream);
                return stream.GetBuffer();
            }
        }

        private static void ParseName<T>(IList<Dictionary<string, T>> source, string colName, IList<string> fieldName, IList<string> fieldTitle)
        {
            if (string.IsNullOrEmpty(colName)) //如果未指定列名则将字段名作为列名输出
            {
                foreach (var k in source[0].Keys)
                {
                    fieldName.Add(k);
                    fieldTitle.Add(k);
                }
            }
            else //析构列名
            {
                Array.ForEach(colName.Split(','), s =>
                {
                    var t = s.Split('$');
                    if (t.Length == 1)
                    {
                        string t0 = t[0];
                        fieldName.Add(t0);
                        fieldTitle.Add(t0);
                    }
                    else
                    {
                        string t0 = t[0], t1 = t[1];
                        fieldName.Add(t0);
                        fieldTitle.Add(t1);
                    }
                });
            }
        }
    }
}
