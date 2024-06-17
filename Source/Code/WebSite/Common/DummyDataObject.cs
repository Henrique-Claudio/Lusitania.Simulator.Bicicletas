using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

[DataObject]
public class DummyDataObject
{
    public static DataTable GetEmptyDataTable()
    {
        return new DataTable();
    }

    public static DataTable GetMultipleRowsDataTable(int count)
    {
        DataTable xTable = new DataTable();
        DataColumn xIDColumn = xTable.Columns.Add("ID", typeof(int));
        DataColumn xNameColumn = xTable.Columns.Add("Name", typeof(string));

        for (int idx = 0; idx < count; ++idx)
        {
            DataRow xRow = xTable.NewRow();
            xRow[xIDColumn] = idx;
            xRow[xNameColumn] = Guid.NewGuid().ToString();
            xTable.Rows.Add(xRow);
        }

        xTable.AcceptChanges();

        return xTable;
    }

    public static DataTable GetSingleRowDataTable()
    {
        DataTable xTable = new DataTable();
        DataColumn xColumn = xTable.Columns.Add("Dummy", typeof(string));

        DataRow xRow = xTable.NewRow();
        xRow[xColumn] = Guid.NewGuid().ToString().Substring(0, 9);
        xTable.Rows.Add(xRow);

        xTable.AcceptChanges();

        return xTable;
    }

    public static DataTable Get100RowsDataTable()
    {
        DataTable xTable = new DataTable();
        DataColumn xColumn = xTable.Columns.Add("Dummy", typeof(string));

        for (int counter = 0; counter < 100; ++counter)
        {
            DataRow xRow = xTable.NewRow();
            xRow[xColumn] = Guid.NewGuid().ToString();
            xTable.Rows.Add(xRow);
        }

        xTable.AcceptChanges();

        return xTable;
    }
}
