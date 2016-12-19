using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

// Open question:  Should I be using http://www.filehelpers.net/ instead?

namespace ExcelConnect
{

    public class foo : List<Dictionary<String,String>>
    {

        /******************************
         * 
Dictionary<string,string>
A=1
B=2
C=3
D=4
A B C D
1 2 3 4

NAME VALUE
A	1
B	2
C	3
D	4



Transactions
{
["date,"09062016","vendor",vendor],["amount",amount],["category",category]
["date,"09062016","vendor",vendor],["amount",amount],["category",category]
["date,"09062016","vendor",vendor],["amount",amount],["category",category]
}

Transactions
date	vendor	amount	category
--------------------------------
date	vendor	amount	category
date	vendor	amount	category
date	vendor	amount	category
date	vendor	amount	category
date	vendor	amount	category

AutoMap
vendor	category	auto-map-value
----------------------------------------------------------------
vendor	category	auto-map-value
vendor	category	auto-map-value
vendor	category	auto-map-value
vendor	category	auto-map-value

         * ***************************/
    }

    public class WorkSheet : DataTable
    {
        private Boolean schemaDefined = false;
        private static Excel ActiveSheet = null;
        //XYZZY still a bit to do
        // need to figure out if the table still exists
        // get the header if it does
        // create a new table if it doesn't
        // probably add/remove [square brackets] as needed
        // add table to the dataset/activesheet also
        // And make sure 'accept changes' also includes Update()
        // Also need to DEFINE select/update/delete
        // And get Fill() to work correctly 
        //
        // Usage I envision:
        // SPL = New SpreadSheetLink(FILENAME)
        // WSH = New WorkSheet(SPL , WORKSHEETNAME) ==> change the way I deal with the data so it feels more like a list/array/associative array/etc.
        // WSH.AddData ==>  Perhaps ask the object to give me an associative array.  Then manipulate the assocarray as I want, and then feed it back when I'm "saving" it
        // WSH.AcceptChanges  ==> Could also have a static function that says "load an associtive array" and could just instantiate the table if it doesn't exist, and give me a blank dictionary
        //                  ==> Then also have a static function "save" which would of course do the update() and accept changes!
        //
        // then see about encapsulating further to load/save an associative array
        // Or Create an object with a Load() and Save) method instead of SpreadsheetLink() method
        //
        public WorkSheet(String sheetName, String fileName="", String[] header = null, String[] primaryKeys = null)
            : base()
        {
            base.TableName = sheetName.Replace("[", "").Replace("]", "").Replace("$", ""); // Remove Excelisms if they were included
            if (null == WorkSheet.ActiveSheet)
            {
                if (fileName == "")
                {
                    throw new Exception("Error - No Spreadsheet file specified. Unable to proceed");
                }
                else
                {
                    String extension = Path.GetExtension(fileName).ToLower();
                    String pathOnly = Path.GetDirectoryName(fileName);
                    String textFileName = Path.GetFileName(fileName).Replace(".", "#");
                    switch (extension)
                    {
                        case ".xls":
                            WorkSheet.ActiveSheet = new Excel(fileName, Excel.format.Excel8);
                            break;
                        case ".xlsx":
                            WorkSheet.ActiveSheet = new Excel(fileName, Excel.format.Excel12);
                            break;
                        case ".csv":
                            WorkSheet.ActiveSheet = new Excel(pathOnly, Excel.format.CSV);
                            base.TableName = textFileName; //With CSV files, the sheetname has to be the filename
                            break;
                    }
                }
            }

            // If worksheet already exists - grab the schema - CONNECTION.GetOleDbSchemaTable
            // If worksheet doesn't already exist, do a 'CREATE TABLE' based on this: http://forums.devx.com/showthread.php?169318-Excel-OLEDB-Create-Table-Issue
            initTable(loadSheet(), header, primaryKeys);

            if (schemaDefined)
            {
                defineCommands(); // use internal keyname
            }
            else
            {
                throw new Exception("Unable to load table schema - " + base.TableName);
            }
        }

        private Boolean loadSheet()
        {
            Boolean foundSheet = false;
            if (null != WorkSheet.ActiveSheet)
            {
                if (null != WorkSheet.ActiveSheet.DbSchema)
                {
                    if (null != WorkSheet.ActiveSheet.DbSchema.Rows)
                    {
                        foreach (DataRow row in WorkSheet.ActiveSheet.DbSchema.Rows)
                        {
                            if (base.TableName == (string)row["TABLE_NAME"])
                            {
                                foundSheet = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Rows is null");
                    }
                }
                else
                {
                    throw new Exception("DBSchema is null");
                }
            }
            else
            {
                throw new Exception("Activesheet is null");
            }
            WorkSheet.ActiveSheet.SelectSQL("SELECT * FROM " + base.TableName);
            if (foundSheet)
            {
                WorkSheet.ActiveSheet.Fill(this);

                //this.Columns.Add(new DataColumn("foo", System.Type.GetType("System.String")));
                //WorkSheet.ActiveSheet.AcceptChanges();
            }
            return foundSheet;
        }

        private void defineCommands()
        {
            List<String> colList = new List<String>();
            List<String> keyList = new List<String>();
            foreach (DataColumn col in base.Columns)
            {
                colList.Add(col.ColumnName);
            }
            foreach (DataColumn col in base.PrimaryKey)
            {
                keyList.Add(col.ColumnName);
            }
            WorkSheet.ActiveSheet.InsertSQL(base.TableName, colList, keyList);
        }

        /// <summary>
        /// Establish new table or process existing one
        /// </summary>
        /// <param name="loadedSheet">Indicates whether the table already exists</param>
        /// <param name="header">List of columns to use when creating the table</param>
        /// <param name="primaryKeys">List of columns which will form the primary key</param>
        public void initTable(Boolean loadedSheet, String[] header, string[] primaryKeys)
        {
            // this.selectCmd.CommandText = "SELECT * FROM " + base.TableName;
            // DataSet tempData = (DataSet)WorkSheet.ActiveSheet;
            // tempData.
            // dataModel = new DataSet();
            // 
            //https://msdn.microsoft.com/en-us/library/system.data.oledb.oledbdataadapter.updatecommand%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            //
            // http://stackoverflow.com/questions/10535663/how-do-i-update-excel-file-with-oledbdataadapter-updatemydataset
            //
            //
            // dataInterface.Fill(dataModel, "XLData");
            DataColumn column = null;
            List<DataColumn> keys = new List<DataColumn>();
            int[] nullCount = new int[Columns.Count];
            if (loadedSheet)
            {
                int index=0;
                foreach (DataRow row in this.Rows)
                {
                    index=0;
                    foreach (object cell in row.ItemArray) {
                        if (cell == (object) DBNull.Value) {
                            nullCount[index]++;
                        }
                        index++;
                    }
                }
                for (index = 0; index < Columns.Count; index++)
                {
                    if (nullCount[index] == 0)
                    {
                        Columns[index].AllowDBNull = false;
                        keys.Add(Columns[index]);
                    }
                }
            }
            else
            {
                if (null != header)
                {
                    String createCMD = "CREATE TABLE [" + TableName + "] (" + String.Join(" char(200),",header) + " char(200) )";
                    Boolean OK = WorkSheet.ActiveSheet.RunSQL(createCMD);

                    foreach (string colName in header)
                    {
                        column = new DataColumn(colName, System.Type.GetType("System.String"));
                        if ( (null != primaryKeys ) && (primaryKeys.Contains<String>(colName)) )
                        {
                            // This column needs to be a primary key
                            column.AllowDBNull = false;
                            keys.Add(column);
                        }
                        else
                        {
                            // Not a primary key
                            column.AllowDBNull = true;
                        }
                        Columns.Add(column);
                    }
                }
            }
            PrimaryKey = keys.ToArray();
            schemaDefined = (keys.Count >= 0);
        }

        public new void AcceptChanges()
        {
            // Save to Excel
            base.AcceptChanges();
        }

    }

    public class Excel : DataSet
    {
        private OleDbConnection conn = new OleDbConnection();
        private OleDbDataAdapter excelLink = new OleDbDataAdapter();
        private DataTable dbSchema;
        private String fileName;
        private OleDbCommand selectCommand = null;
        private OleDbCommand updateCommand = null;
        private OleDbCommand insertCommand = null;
        private OleDbCommand deleteCommand = null;

        public enum format
        {
            Excel8 = 0,
            Excel12,
            CSV
        }

        public Excel(String fileName, format FileType)
            : base(fileName)
        {
            /*
             $cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\";
            $select = "SELECT * FROM D:\test.csv"
            $conn= New-Object System.Data.OleDb.OleDbConnection($cs)
            $conn.open()
            $cmd=new-object system.data.oledb.oledbcommand($select,$conn)
            $da = New-Object system.Data.OleDb.OleDbDataAdapter($cmd)
            $dt = New-Object system.Data.datatable
            [void]$da.fill($dt)
            $conn.Close()
*/
            this.fileName = fileName;

            String[] csHeader = new String[] {
                "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=",
                "Provider=Microsoft.ACE.OLEDB.12.0; Data Source="
            };

            String[] csSource = new String[] {
                fileName,
                fileName,
                fileName
            };

            String[] csFooter = new String[]{
                ";Mode=ReadWrite;Extended Properties='Excel 12.0 XML;HDR=YES'",
                ";Extended Properties='Excel 8.0'",
                ";Extended Properties='Text;HDR=YES;IMEX=1'"};

            int csIndex = (int)FileType;
            conn = new OleDbConnection(csHeader[csIndex] + csSource[csIndex] + csFooter[csIndex]);
            conn.Open();
            this.DataSetName = fileName;
            dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, null });
        }

        public DataTable DbSchema
        {
            get
            {
                return this.dbSchema;
            }
        }

        public String FileName
        {
            get
            {
                return this.fileName;
            }
        }
        public OleDbCommand SelectCommand
        {
            get
            {
                return selectCommand;
            }
        }

        public OleDbCommand UpdateCommand
        {
            get
            {
                return updateCommand;
            }
        }

        public OleDbCommand InsertCommand
        {
            get
            {
                return insertCommand;
            }
        }

        public OleDbCommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }

        /// <summary>
        /// Store the command required to select one or more rows from the table
        /// </summary>
        /// <param name="sql">Command text</param>
        public OleDbCommand SelectSQL(String sql)
        {
            selectCommand = conn.CreateCommand();
            selectCommand.CommandText = sql;
            excelLink.SelectCommand = selectCommand;
            return selectCommand;
        }

        /// <summary>
        /// Store the command required to update one or more rows from the table
        /// </summary>
        /// <param name="sql">Command text</param>
        public OleDbCommand UpdateSQL(String sql)
        {
            updateCommand = conn.CreateCommand();
            updateCommand.CommandText = sql;
            excelLink.UpdateCommand = updateCommand;
            return updateCommand;
        }

        /// <summary>
        /// Store the command required to insert one or more rows into the table
        /// </summary>
        /// <param name="sql">Command text</param>
        public OleDbCommand InsertSQL(String tableName, List<String> header, List<String> keys)
        {
            List<String> qList = new List<String>();
            for (int i = 0; i < header.Count; i++)
            {
                qList.Add("?");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT [" + tableName + "] (");
            sql.Append(String.Join(",", header.ToArray<String>()));
            sql.Append(") VALUES (");
            sql.Append(String.Join(",", qList.ToArray<String>()));
            sql.Append(")");
            
            insertCommand = conn.CreateCommand();
            insertCommand.CommandText = sql.ToString();

            foreach (String col in header)
            {
                insertCommand.Parameters.Add(col, OleDbType.VarChar, 200).SourceColumn = col;
            }
            excelLink.InsertCommand = insertCommand;
            return insertCommand;
        }
        //XYZZY
        //TODO
        // Now fill in Update
        // Then fill in delete
        // But also add logic to NEVER DELETE if you're interacting with Excel!
        // *TEST* these components
        // Finally, give an interface where you are manipulating a data structure that looks like an array of dictionaries: dictionary<Strng,String>[]
        // If a sheet looks like this:
        // A    B   C
        // 1    2   3
        // 3    4   5
        // 7    8   9
        // Then you can represent this with:
        // [  {"A",1},{"B",2},{"C",3},
        //    {"A",3},{"B",4},{"C",5},
        //    {"A",7},{"B",8},{"C",9}
        // ]
        // Now we need to convert a dataset into an array of string/string hashes
        // And convert array of string/string hashes into a dataset
        //
        // Then use this to UPDATE/INSERT into the dataset and use the native functionality to then save
        //
        // And finally, we should be able to instantiate a new data structure and have all this happen behind the scenes, including filling in current values, knowing when something has changed, and saving
        // List<Dictionary<String,String>>


        /// <summary>
        /// Store the command required to delete one or more rows from the table
        /// </summary>
        /// <param name="sql">Command text</param>
        /// <returns></returns>
        public OleDbCommand DeleteSQL(String sql)
        {
            deleteCommand = conn.CreateCommand();
            deleteCommand.CommandText = sql;
            excelLink.DeleteCommand = deleteCommand;
            return deleteCommand;
        }

        /// <summary>
        /// Take a SQL statement and execute it against the connected database
        /// </summary>
        /// <param name="sql">statement to execute</param>
        /// <returns>True if execution was successful</returns>
        public Boolean RunSQL(String sql)
        {
            Boolean OK = false;
            try
            {
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                int rows = cmd.ExecuteNonQuery();
                OK = true;
            }
            catch
            {
                OK = false;
            }
            return OK;
        }

        public int Fill(WorkSheet destination)
        {
            int result = excelLink.Fill(destination);
            return result;
        }

        public int Update()
        {
            return excelLink.Update(this);
        }

        public int Update(WorkSheet singleSheet)
        {
            return excelLink.Update((DataTable)singleSheet);
        }

        public bool closeLink()
        {
            bool closedOK = false;
            try
            {
                conn.Close();
                closedOK = true;
            }
            catch
            {
                closedOK = false;
            }
            return closedOK;
        }

    }
    // Establish a grid with named columns

    // Update grid by modifying rows
    // Write to Excel

    // Set values
    // Write to Excel

    // Update grid by inserting rows
    // Write to Excel

    // Update grid by deleting rows
    // Write to Excel    

    // Establish a grid with named columns
    // Load values from Excel



}
/*
namespace BudgetCal
{
    public partial class Form1 : Form
    {
        //Newer version of Excel - Future
        //       connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Clients.xlsx;Extended Properties=""Excel 12.0 Xml;HDR=YES"";";
        //             "UPDATE [Clients$] " + 
 
        String backendConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\\temp\\cuteness.xls; Extended Properties=Excel 8.0;";
        OleDbConnection backendConnection;
        DataSet dataModel;
        OleDbDataAdapter dataInterface;
        public Form1()
        {
            backendConnection = new OleDbConnection(backendConnectionString); 
            chart1.ChartAreas[0].BackColor = Form1.DefaultBackColor;
            chart1.BackColor = Form1.DefaultBackColor;

            dataInterface = new OleDbDataAdapter();
            dataInterface.SelectCommand = new OleDbCommand("SELECT * FROM NAME", backendConnection);

            dataModel = new DataSet();

            //https://msdn.microsoft.com/en-us/library/system.data.oledb.oledbdataadapter.updatecommand%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            //
            // http://stackoverflow.com/questions/10535663/how-do-i-update-excel-file-with-oledbdataadapter-updatemydataset
            //
            //
            dataInterface.Fill(dataModel, "XLData");

            OleDbCommand updateCmd = new OleDbCommand("UPDATE NAME Set name = ? WHERE animal = ?", backendConnection);
            updateCmd.Parameters.Add("name",OleDbType.Char,20,"name");
            updateCmd.Parameters.Add("animal",OleDbType.Char,20,"animal");
            dataInterface.UpdateCommand = updateCmd;
            DataRow drCurrent = dataModel.Tables["XLData"].Rows[0];
//            drCurrent.BeginEdit();
            drCurrent["name"] = "Harmony";
 //           drCurrent.EndEdit();
//            drCurrent.AcceptChanges();
//            dataModel.Tables["XLData"].Rows[0] = drCurrent;

   //         dataModel.Tables["XLData"].Rows[0].BeginEdit();
 //           dataModel.Tables["XLData"].Rows[0]["name"] = "Harmony";
   //         dataModel.Tables["XLData"].Rows[0].EndEdit();
     //       dataModel.Tables["XLData"].AcceptChanges();
//            dataModel.AcceptChanges();
            dataInterface.Update(dataModel, "XLData"); //daAuthors.Update(dsPubs, "Authors");

            backendConnection.Close();
            // externalSheet.DataSource = dataSet1.Tables[0].DefaultView;

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
                processDate();
        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            processDate();
        }

        private void processDate()
        {
            if (monthCalendar1.SelectionStart.ToShortDateString() == monthCalendar1.SelectionEnd.ToShortDateString())
            {
                showExpenses();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            hideExpenses();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideExpenses();
        }

        private void showExpenses()
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            panel1.Visible = true;
            comboBox1.Visible = true;
            label1.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void showInstructions()
        {
            label1.Text = "Please select a date to view its transactions";
        }

        private void bigLabel()
        {
            label1.Font = new Font(label1.Font.FontFamily, label1.Font.Size * 2);
        }

        private void hideExpenses()
        {
            showInstructions();
            monthCalendar1.Visible = true;
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            panel1.Visible = false;
            comboBox1.Visible = false;
            monthCalendar1.Height = 1;
            monthCalendar1.Width = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            trackBar1.Visible = checkBox1.Checked;
            label4.Visible = checkBox1.Checked;
        }


    }
}

*/





//Newer version of Excel - Future
//       connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Clients.xlsx;Extended Properties=""Excel 12.0 Xml;HDR=YES"";";
//             "UPDATE [Clients$] " + 
/* 
To enable read write on the file you can use the attribute : Mode

Provider=Microsoft.ACE.OLEDB.12.0;Data Source=EXCELFILENAME;Mode=ReadWrite;Extended Properties='Excel 12.0 Macro;HDR=YES;'

 * 
 * 
string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Mode=ReadWrite;Extended Properties=\"Excel 12.0 XML;HDR=\"";

using (OleDbConnection conn = new OleDbConnection(connectionString))
{
conn.Open();

using (OleDbCommand cmd = new OleDbCommand())
{
cmd.Connection = conn;
cmd.CommandText = "CREATE TABLE [MySheet] (a string)";  // Doesn't matter what the field is called
cmd.ExecuteNonQuery();

cmd.CommandText = "UPDATE [MySheet$] SET F1 = \"\"";
cmd.ExecuteNonQuery();
}

conn.Close();
}
 *Brilliant THREAD WITH EXAMPLES OF HOW TO MANIPULATE EXCEL 
 * http://forums.devx.com/showthread.php?169318-Excel-OLEDB-Create-Table-Issue
String backendConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\\temp\\cuteness.xls; Extended Properties=Excel 8.0;";
OleDbConnection backendConnection;
DataSet dataModel;
OleDbDataAdapter dataInterface;
 */


/*
 *             backendConnection = new OleDbConnection(backendConnectionString);
            dataInterface = new OleDbDataAdapter();
            dataInterface.SelectCommand = new OleDbCommand("SELECT * FROM NAME", backendConnection);

            dataModel = new DataSet();

            //https://msdn.microsoft.com/en-us/library/system.data.oledb.oledbdataadapter.updatecommand%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            //
            // http://stackoverflow.com/questions/10535663/how-do-i-update-excel-file-with-oledbdataadapter-updatemydataset
            //
            //
            dataInterface.Fill(dataModel, "XLData");

            OleDbCommand updateCmd = new OleDbCommand("UPDATE NAME Set name = ? WHERE animal = ?", backendConnection);
            updateCmd.Parameters.Add("name",OleDbType.Char,20,"name");
            updateCmd.Parameters.Add("animal",OleDbType.Char,20,"animal");
            dataInterface.UpdateCommand = updateCmd;
            DataRow drCurrent = dataModel.Tables["XLData"].Rows[0];
//            drCurrent.BeginEdit();
            drCurrent["name"] = "Harmony";
 //           drCurrent.EndEdit();
//            drCurrent.AcceptChanges();
//            dataModel.Tables["XLData"].Rows[0] = drCurrent;

   //         dataModel.Tables["XLData"].Rows[0].BeginEdit();
 //           dataModel.Tables["XLData"].Rows[0]["name"] = "Harmony";
   //         dataModel.Tables["XLData"].Rows[0].EndEdit();
     //       dataModel.Tables["XLData"].AcceptChanges();
//            dataModel.AcceptChanges();
            dataInterface.Update(dataModel, "XLData"); //daAuthors.Update(dsPubs, "Authors");

            backendConnection.Close();
            // externalSheet.DataSource = dataSet1.Tables[0].DefaultView;

*/