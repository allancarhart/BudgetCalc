using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
// Don't need this if we don't need Forceclickusing System.Runtime.InteropServices;


// The way this code is emerging is ridiculous. I should not be struggling with this.
// Just grab the data, shove it into some easy to manipulate data structure, and do all the manipulation necessary.
// Then cross the boundary from that data strucutre back (*SAVE*) exactly once, where all the data is pulled out and pushed back into the rowset.
// Is that practical?  Or some OTHER way to do it so I'm not manipulating rows all over the place!?!?!?
// Because this is just wasting my time, and it's not okay.
//
namespace BudgetCal
{
    public partial class Form1 : Form
    {
        /*
         *      *PRETTY* sure I don't need this any more.  I just wanted to make an item be selected by left-clicking if I got right click.
         *      However, there is a way to do a selected-item-from-point which seems a lot cleaner.
         *      http://stackoverflow.com/questions/9220501/right-click-to-select-items-in-a-listbox
         *              [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
                public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
                //Mouse actions
                private const int MOUSEEVENTF_LEFTDOWN = 0x02;
                private const int MOUSEEVENTF_LEFTUP = 0x04;
        public void ForceClick(uint X, uint Y)
        {
            //Call the imported function with the cursor's current position
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        */

        //        private List<Dictionary<String, String>> transactionList = new List<Dictionary<string, string>>();
        //        private HashSet<Int32> TransactionIDs = new HashSet<Int32>(); // All transactions represented here
        private DataTable transactionSet = new DataTable("transactions");

        private HashSet<String> foobar = new HashSet<String>();
        private Int32 lastTransactionID = 0;  // This should be TransactionIds.Length or something like that - but then don't ever delete a transaction - just disassociate it with its date
        private Dictionary<DateTime, HashSet<Int32>> DateTransactions = new Dictionary<DateTime, HashSet<Int32>>(); // DateList = DateTransactions.Keys
        private Dictionary<Int32, Tuple<DateTime?, String, Int32, String, Boolean>> AllTransactions = new Dictionary<Int32, Tuple<DateTime?, String, Int32, String, Boolean>>(); //Map ID to vendor/amount/category/category-mapped
        private readonly string newMerchant = "Enter merchant name";
        private readonly string newAmount = "Enter amount";

        /*
         * When I click a transaction:
- Check that transaction's cateogry
Does it have one?  Then select it in the category list and also check 'Map'
Does it not have one?  Then uncheck 'map'

         * */
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox.ObjectCollection items = ((System.Windows.Forms.ListBox)sender).Items;
            int selection = ((System.Windows.Forms.ListBox)sender).SelectedIndex;


        }
























        private void AddDate(DateTime dateVal)
        {
            if (DateTransactions.ContainsKey(dateVal))
            {
                // nothing to do
            }
            else
            {
                HashSet<Int32> NewTransactions = new HashSet<Int32>();
                DateTransactions.Add(dateVal, NewTransactions);
            }
        }

        private HashSet<Int32> GetTransactions(DateTime dateVal)
        {
            if (!DateTransactions.ContainsKey(dateVal))
            {
                AddDate(dateVal);
            }
            return DateTransactions[dateVal];
        }

        private void SetTransaction()
        {
            // I think I need this?
        }
        private void DelTransaction(DateTime dateVal, Int32 transactionID)
        {
            HashSet<Int32> transactions = GetTransactions(dateVal);
            transactions.Remove(transactionID);
            AllTransactions[transactionID] = UpdateTransaction(AllTransactions[transactionID], null);
            //HALLOWEEN TODO XYZZY:  Keep implementing these methods, and then use them down below.  
            // Later I can wire up the use of these to the persistent storage needs
        }

        Tuple<DateTime?, string, int, string, bool> UpdateTransaction(Tuple<DateTime?, string, int, string, bool> Transaction, DateTime? Item1, string Item2 = null, int? Item3 = null, string Item4 = null, bool? Item5 = null)
        {
            DateTime? NewItem1 = Item1;
            string NewItem2 = (null == Item2 ? Transaction.Item2 : Item2);
            int NewItem3 = (null == Item3 ? Transaction.Item3 : Item3.Value);
            string NewItem4 = (null == Item4 ? Transaction.Item4 : Item4);
            bool NewItem5 = (null == Item5 ? Transaction.Item5 : Item5.Value);
            Transaction = new Tuple<DateTime?, string, int, string, bool>(NewItem1, NewItem2, NewItem3, NewItem4, NewItem5);
            return Transaction;
        }

        private void GetCategories()
        {

        }

        private void MapCategory()
        {

        }

        private void UnMapCategory()
        {

        }

        public Form1()
        {
            InitializeComponent();
            bigLabel();
            hideExpenses();
            chart1.ChartAreas[0].BackColor = Form1.DefaultBackColor;
            chart1.BackColor = Form1.DefaultBackColor;
            transactionSet.Columns.Add("date", System.Type.GetType("System.DateTime"));
            transactionSet.Columns.Add("vendor", System.Type.GetType("System.String"));
            transactionSet.Columns.Add("amount", System.Type.GetType("System.Double"));
            transactionSet.Columns.Add("category", System.Type.GetType("System.String"));
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/5/2016"), "HRC", "150", "WTF" });
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/5/2016"), "lunch", "50", "WTF" });
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/4/2016"), "Safeway", "30", "groceries" });
            transactionSet.AcceptChanges();

            //foobar.

            IEnumerable<DataRow> dayTransactions =
                from row in transactionSet.AsEnumerable()
                where row.Field<DateTime>("date") == DateTime.Parse("9/5/2016")
                select row;
            foreach (DataRow row in dayTransactions)
            {
                row["category"] = "WTF?";
            }
            transactionSet.AcceptChanges();

            //            IQueryable<DataRow> rowResults = transactionSet.Rows.AsQueryable();
            //            rowResults = from row in rowResults where 
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


        //December, 2016
        private void showExpenses()
        {
            currentDate.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button1.Visible = true;
            transactionAdd.Visible = true;
            categoryAdd.Visible = true;
            categoryAdd.Enabled = false;
            dayView.Visible = true;
            currentDate.Text = monthCalendar1.SelectionStart.ToShortDateString();


            //Dictionary<String, short> categoryList = new Dictionary<string, short>();
            //WTF?  What is this?  Why is it commended out?  What's the next step?
            // step through set of datarows, taking one datarow at a time
            /*IEnumerable<DataRow> dayTransactions =
            from row in transactionSet.AsEnumerable()
            where row.Field<DateTime>("date") == monthCalendar1.SelectionStart
            select row;*/
            /*
            foreach (
                DataRow row in
                from row in transactionSet.AsEnumerable()
                where row.Field<DateTime>("date").ToShortDateString() == monthCalendar1.SelectionStart.ToShortDateString()
                select row
            )
            {
                String vendor = row.Field<String>("vendor");
                Double amount = row.Field<Double>("amount");
                String category = row.Field<String>("category");
                //Also need to pull out category and fill it in!
                transactions.Items.Add(vendor + " - " + amount);
                addCategory(this.categoryList, category);

        /*
                if (!categoryList.ContainsKey(category))
                {
                    this.categoryList.Items.Add(category);
                    categoryList.Add(category,1);
                }
            }
                */

            ///December 16th 2016, this is where the relevant code starts.  Stuff earlier may not be actually used
            transactions.Visible = false;
            categoryList.Visible = false;
            loadCategories(categoryList);
            loadTransactions(transactions, currentDate.Text);
            transactions.Visible = true;
            categoryList.Visible = true;

            newTransactionDesc.Text = newMerchant;
            newTransactionAmount.Text = newAmount;
            transactionAdd.Enabled = false;
        }

        private void showInstructions()
        {
            currentDate.Text = "Please select a date to view its transactions";
        }

        private void bigLabel()
        {
            currentDate.Font = new Font(currentDate.Font.FontFamily, currentDate.Font.Size * 2);
        }

        private void hideExpenses()
        {
            showInstructions();
            monthCalendar1.Visible = true;
            currentDate.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            transactionAdd.Visible = false;
            categoryAdd.Enabled = false;
            categoryAdd.Visible = false;
            dayView.Visible = false;
            categoryList.Visible = false;
            monthCalendar1.Height = 1;
            monthCalendar1.Width = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            trackBar1.Visible = autoMap.Checked;
            label4.Visible = autoMap.Checked;
        }

        private void detectNewCategory(System.Windows.Forms.ComboBox sender, EventArgs e)
        {
            if (categoryList.Items.Contains(sender.Text))
            {
                categoryAdd.Enabled = false;
            }
            else
            {
                categoryAdd.Enabled = true;
            }
        }
        /*
         * 
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/5/2016"), "HRC", "150", "WTF" });
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/5/2016"), "lunch", "50", "WTF" });
            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/4/2016"), "Safeway", "30", "groceries" });
            transactionSet.AcceptChanges();

            IEnumerable<DataRow> dayTransactions =
                from row in transactionSet.AsEnumerable()
                where row.Field<DateTime>("date") == DateTime.Parse("9/5/2016")
                select row;
            foreach (DataRow row in dayTransactions)
            {
                row["category"] = "WTF?";
            }
            transactionSet.AcceptChanges();

         //xyzzy - Next steps:
                // when you click add, take the content and add it to the list similar to this:
                //            transactionSet.Rows.Add(new Object[] { DateTime.Parse("9/5/2016"), "HRC", "150", "WTF" });
                // Except where we've already selected for date
                // This should add the transaction, but then add 
                */


        private List<DataRow> getRowsByDate(String Date)
        {
            IEnumerable<DataRow> dayTransactions =
                from row in transactionSet.AsEnumerable()
                where row.Field<DateTime>("date") == DateTime.Parse("9/5/2016")
                select row;
            return dayTransactions.ToList<DataRow>();
        }

        private List<DataRow> AddTransaction(List<DataRow> dayTransactions, String Vendor, Double Amount, String Category)
        {
            DateTime currentDay = dayTransactions[0].Field<DateTime>("date");
            DataRow row = transactionSet.NewRow();
            row.ItemArray = new Object[] { currentDay, Vendor, Amount, Category };
            dayTransactions.Add(row);
            return dayTransactions;
        }

        private List<DataRow> DelTransaction(List<DataRow> dayTransactions, String Vendor, Double Amount)
        {
            DateTime currentDay = dayTransactions[0].Field<DateTime>("date");
            // WHAT THE FREAKING HELL?  Find an example that shows me how to do a "SELECT ... WHERE (BOOLEAN) AND (BOOLEAN)" in Linq, 'cause I'm just not getting it
            //            dayTransactions.AsEnumerable<DataRow>().Where<DataRow>(((dayTransactions,row.Field<String>("vendor") != Vendor) => row); //(row ==> row.Field<String("vendor") != Vendor || row.Field<Double>("amount") != Amount)
            /*            IEnumerable<DataRow> removed =
                            from row in dayTransactions.AsEnumerable<DataRow>()
                            .Where(emp => emp.Field<int>("EmpID")
                        == 1 || emp.Field<int>("EmpID") == 2);

                            where row.Field<String>("vendor") != Vendor || row.Field<String>("amount") != Amount
                            select row;
             */
            return dayTransactions.ToList<DataRow>();

            //            dayTransactions.
            //            DataRow row = transactionSet.NewRow();
            //            row.ItemArray = new Object[] { currentDay, Vendor, Amount, Category };
            //            IEnumerable<DataRow> newRow = Enumerable.Repeat<DataRow>(row, 1);
            //            return dayTransactions.Concat<DataRow>(newRow).ToList<DataRow>();
        }

        private void CategoryMap_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void categoryAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(categoryList.Text))
            {
                addCategory(categoryList, categoryList.Text);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            hideExpenses();
        }

        private void categoryList_TextChanged(object sender, EventArgs e)
        {
            detectNewCategory((System.Windows.Forms.ComboBox)sender, e);
        }

        private void autoMap_CheckedChanged(object sender, EventArgs e)
        {
        }


        //User clicked into the 'New Transaction' textbox
        private void newTransactionDesc_MouseClick(object sender, MouseEventArgs e)
        {
            if (newTransactionDesc.Text == newMerchant)
            {
                newTransactionDesc.Text = "";
            }
            newTransactionDesc.SelectAll();
        }

        //User clicked into the 'New Amount' textbox
        private void newTransactionAmount_MouseClick(object sender, MouseEventArgs e)
        {
            if (newTransactionAmount.Text == newAmount)
            {
                newTransactionAmount.Text = "";
            }
            newTransactionAmount.SelectAll();
        }

        // User TABbed into the 'New Transaction textbox
        private void newTransactionDesc_Enter(object sender, EventArgs e)
        {
            newTransactionDesc_MouseClick(sender, null);
        }

        // User TABbed into the 'Transaction Amount' textbox
        private void newTransactionAmount_Enter(object sender, EventArgs e)
        {
            newTransactionAmount_MouseClick(sender, null);
        }

        private void newTransaction_entered(object sender, EventArgs e)
        {
            float numAmount = 0;
            if (newTransactionDesc.Text != "" && float.TryParse(newTransactionAmount.Text.Replace("$", ""), out numAmount))
            {
                transactionAdd.Enabled = true;
            }
            else
            {
                transactionAdd.Enabled = false;
            }
        }

        //This is because I wanted to call 'MouseClick' from other elements that are not related to mice
        // I could probably clean this up by having a legitimate event be used here, and then call mouse click

        private void AddCategory()
        {

        }

        //Todo: Actually add to the listbox and some data structure
        // Then make categorization work
        private void transactionAdd_Enter(object sender, EventArgs e)
        {
            String newItem = newTransactionDesc.Text;
            float newPrice = float.Parse(newTransactionAmount.Text);
            addTransaction(transactions, newItem, newPrice);
            newTransactionDesc.Focus();
        }
        /*********************************************
         * NEW METHODS
         * THESE WILL NEED TO BE FULLY IMPLEMENTED
         * BUT NOW JUST ASSUME THEY WORK
         *********************************************/
        //December, 2016
        void loadCategories(ComboBox listDestination)
        {
            object[] categories = new Object[] { "" };
            categories[0] = (object)"Drug Store";
            listDestination.Items.Clear();
            listDestination.Items.AddRange(categories);
        }

        void loadTransactions(ListBox listDestination, String currentDate)
        {
            // This should actually pull out store and price as two different things. 
            object[] transactions = new Object[] { "" };
            transactions[0] = (object)"Item - 123";
            listDestination.Items.Clear();
            listDestination.Items.AddRange(transactions);
        }

        void addCategory(ComboBox listDestination, String newItem)
        {
            // Add it to a master list

            // Also add it to a visual list
            if (!listDestination.Items.Contains(newItem))
            {
                listDestination.Items.Add(newItem);
            }

        }//December, 2016
        private void addTransaction(ListBox destination, String item, float amount)
        {

            // Add to the master list
            string label = item + " - " + amount.ToString();
            destination.Items.Add((object)label);
        }
        /*********************************************/

        private void transactions_MouseDown(object sender, MouseEventArgs e)
        {
            Point controlClick = new Point(e.X, e.Y); // This is where the user clicked relative to the control only
            Point formClick = new Point(dayView.Location.X + transactions.Location.X + e.X, dayView.Location.Y + transactions.Location.Y + e.Y); // Relative to the entire form

            deleteTransaction.Visible = false;  // Hide Make the "delete" button

            if (e.Button == MouseButtons.Right)
            {
                // User right-clicked
                // 
                int clickedIndex = transactions.IndexFromPoint(controlClick); // Take the coordinates of the click and locate the item being clicked
                if (clickedIndex >= 0) // The user clicked a valid area in the control - we can use the index
                {
                    transactions.SelectedIndex = clickedIndex; // Select just as though the user had left-clicked
                    deleteTransaction.Text = "Delete: " + transactions.Items[clickedIndex];
                    deleteTransaction.Location = formClick; // Give the user a "delete" button right where the clicked
                    deleteTransaction.Visible = !deleteTransaction.Visible; // Make the "delete" button visible :)
                }
            }
        }

        private void deleteTransaction_Click(object sender, EventArgs e)
        {
            deleteTransaction.Visible = false;
            int deleteIndex = transactions.SelectedIndex;
            String deleteTransactionItem = (string) transactions.Items[deleteIndex];
            String[] deleteTransactionText = Regex.Split(deleteTransactionItem, " - ");
            newTransactionDesc.Text = deleteTransactionText[0];
            newTransactionAmount.Text = deleteTransactionText[1];
            transactions.Items.RemoveAt(deleteIndex);
            //TODO: ALSO DELETE IN THE DEFINITIVE DATA STRUCTURE WHICH DOES NOT YET EXIST
        }
    }
}