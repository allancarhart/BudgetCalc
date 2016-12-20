namespace BudgetCal
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 100D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, -100D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 10D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, -10D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, -20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint21 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint22 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint23 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint24 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.dayView = new System.Windows.Forms.Panel();
            this.CategoryMap = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.newTransactionAmount = new System.Windows.Forms.TextBox();
            this.categoryAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.autoMap = new System.Windows.Forms.CheckBox();
            this.transactions = new System.Windows.Forms.ListBox();
            this.transactionAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.categoryList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newTransactionDesc = new System.Windows.Forms.TextBox();
            this.currentDate = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button4 = new System.Windows.Forms.Button();
            this.deleteTransaction = new System.Windows.Forms.Button();
            this.dayView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(15, 50);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 121;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // dayView
            // 
            this.dayView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dayView.Controls.Add(this.CategoryMap);
            this.dayView.Controls.Add(this.label6);
            this.dayView.Controls.Add(this.label5);
            this.dayView.Controls.Add(this.newTransactionAmount);
            this.dayView.Controls.Add(this.categoryAdd);
            this.dayView.Controls.Add(this.label4);
            this.dayView.Controls.Add(this.trackBar1);
            this.dayView.Controls.Add(this.autoMap);
            this.dayView.Controls.Add(this.transactions);
            this.dayView.Controls.Add(this.transactionAdd);
            this.dayView.Controls.Add(this.label3);
            this.dayView.Controls.Add(this.categoryList);
            this.dayView.Controls.Add(this.label2);
            this.dayView.Controls.Add(this.newTransactionDesc);
            this.dayView.Location = new System.Drawing.Point(15, 224);
            this.dayView.Name = "dayView";
            this.dayView.Size = new System.Drawing.Size(707, 502);
            this.dayView.TabIndex = 101;
            this.dayView.Visible = false;
            // 
            // CategoryMap
            // 
            this.CategoryMap.AutoSize = true;
            this.CategoryMap.Location = new System.Drawing.Point(220, 60);
            this.CategoryMap.Name = "CategoryMap";
            this.CategoryMap.Size = new System.Drawing.Size(47, 17);
            this.CategoryMap.TabIndex = 113;
            this.CategoryMap.Text = "Map";
            this.CategoryMap.UseVisualStyleBackColor = true;
            this.CategoryMap.CheckedChanged += new System.EventHandler(this.CategoryMap_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 376);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 112;
            this.label6.Text = "New Transaction - Amount";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(4, 376);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "New Transaction - Vendor Name";
            this.label5.Visible = false;
            // 
            // newTransactionAmount
            // 
            this.newTransactionAmount.Location = new System.Drawing.Point(220, 399);
            this.newTransactionAmount.Name = "newTransactionAmount";
            this.newTransactionAmount.Size = new System.Drawing.Size(204, 20);
            this.newTransactionAmount.TabIndex = 1;
            this.newTransactionAmount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.newTransactionAmount_MouseClick);
            this.newTransactionAmount.TextChanged += new System.EventHandler(this.newTransaction_entered);
            this.newTransactionAmount.Enter += new System.EventHandler(this.newTransactionAmount_Enter);
            this.newTransactionAmount.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.newTransactionAmount_MouseClick);
            this.newTransactionAmount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newTransactionAmount_MouseClick);
            this.newTransactionAmount.MouseLeave += new System.EventHandler(this.newTransaction_entered);
            // 
            // categoryAdd
            // 
            this.categoryAdd.Location = new System.Drawing.Point(477, 33);
            this.categoryAdd.Name = "categoryAdd";
            this.categoryAdd.Size = new System.Drawing.Size(75, 23);
            this.categoryAdd.TabIndex = 109;
            this.categoryAdd.Text = "Add";
            this.categoryAdd.UseVisualStyleBackColor = true;
            this.categoryAdd.Click += new System.EventHandler(this.categoryAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(415, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "Transaction Name Goes here";
            this.label4.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(220, 117);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(172, 45);
            this.trackBar1.TabIndex = 107;
            this.trackBar1.Value = 5;
            this.trackBar1.Visible = false;
            // 
            // autoMap
            // 
            this.autoMap.AutoSize = true;
            this.autoMap.Location = new System.Drawing.Point(220, 80);
            this.autoMap.Name = "autoMap";
            this.autoMap.Size = new System.Drawing.Size(72, 17);
            this.autoMap.TabIndex = 106;
            this.autoMap.Text = "Auto-Map";
            this.autoMap.UseVisualStyleBackColor = true;
            this.autoMap.CheckedChanged += new System.EventHandler(this.autoMap_CheckedChanged);
            // 
            // transactions
            // 
            this.transactions.FormattingEnabled = true;
            this.transactions.Location = new System.Drawing.Point(7, 31);
            this.transactions.Name = "transactions";
            this.transactions.Size = new System.Drawing.Size(204, 342);
            this.transactions.TabIndex = 105;
            this.transactions.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.transactions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transactions_MouseDown);
            // 
            // transactionAdd
            // 
            this.transactionAdd.Location = new System.Drawing.Point(439, 396);
            this.transactionAdd.Name = "transactionAdd";
            this.transactionAdd.Size = new System.Drawing.Size(75, 23);
            this.transactionAdd.TabIndex = 2;
            this.transactionAdd.Text = "Add";
            this.transactionAdd.UseVisualStyleBackColor = true;
            this.transactionAdd.Enter += new System.EventHandler(this.transactionAdd_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 103;
            this.label3.Text = "Category";
            // 
            // categoryList
            // 
            this.categoryList.FormattingEnabled = true;
            this.categoryList.Items.AddRange(new object[] {
            "Groceries",
            "Clothes for Kids",
            "Emergencies",
            "Other Necessities",
            "Lincoln and Other Vacation",
            "Holidays",
            "Mad Money (A&A)",
            "DL Vacation"});
            this.categoryList.Location = new System.Drawing.Point(220, 33);
            this.categoryList.Name = "categoryList";
            this.categoryList.Size = new System.Drawing.Size(230, 21);
            this.categoryList.TabIndex = 102;
            this.categoryList.TextUpdate += new System.EventHandler(this.categoryList_TextChanged);
            this.categoryList.TextChanged += new System.EventHandler(this.categoryList_TextChanged);
            this.categoryList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.categoryList_TextChanged);
            this.categoryList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.categoryList_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Transaction";
            // 
            // newTransactionDesc
            // 
            this.newTransactionDesc.Location = new System.Drawing.Point(7, 399);
            this.newTransactionDesc.Name = "newTransactionDesc";
            this.newTransactionDesc.Size = new System.Drawing.Size(204, 20);
            this.newTransactionDesc.TabIndex = 0;
            this.newTransactionDesc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.newTransactionDesc_MouseClick);
            this.newTransactionDesc.TextChanged += new System.EventHandler(this.newTransaction_entered);
            this.newTransactionDesc.Enter += new System.EventHandler(this.newTransactionDesc_Enter);
            this.newTransactionDesc.Leave += new System.EventHandler(this.newTransaction_entered);
            this.newTransactionDesc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.newTransactionDesc_MouseClick);
            this.newTransactionDesc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newTransactionDesc_MouseClick);
            this.newTransactionDesc.MouseLeave += new System.EventHandler(this.newTransaction_entered);
            // 
            // currentDate
            // 
            this.currentDate.AutoSize = true;
            this.currentDate.Location = new System.Drawing.Point(12, 9);
            this.currentDate.Name = "currentDate";
            this.currentDate.Size = new System.Drawing.Size(0, 13);
            this.currentDate.TabIndex = 124;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 732);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 102;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.close_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(719, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))))};
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series2.IsVisibleInLegend = false;
            series2.Name = "Series1";
            dataPoint13.Color = System.Drawing.Color.White;
            dataPoint13.IsEmpty = true;
            dataPoint13.Label = "";
            dataPoint14.Color = System.Drawing.Color.White;
            dataPoint14.IsEmpty = true;
            dataPoint14.Label = "";
            dataPoint15.Color = System.Drawing.Color.Green;
            dataPoint15.Label = "xyzzy";
            dataPoint16.Color = System.Drawing.Color.Green;
            dataPoint16.Label = "foofoo";
            dataPoint17.Color = System.Drawing.Color.Red;
            dataPoint17.Label = "under";
            dataPoint18.Color = System.Drawing.Color.Red;
            dataPoint18.Label = "water";
            dataPoint19.Label = "abc";
            dataPoint20.Label = "def";
            dataPoint21.Label = "ghi";
            dataPoint22.Label = "jkl";
            dataPoint23.Label = "mno";
            dataPoint24.Label = "prs";
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.Points.Add(dataPoint15);
            series2.Points.Add(dataPoint16);
            series2.Points.Add(dataPoint17);
            series2.Points.Add(dataPoint18);
            series2.Points.Add(dataPoint19);
            series2.Points.Add(dataPoint20);
            series2.Points.Add(dataPoint21);
            series2.Points.Add(dataPoint22);
            series2.Points.Add(dataPoint23);
            series2.Points.Add(dataPoint24);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(580, 747);
            this.chart1.TabIndex = 118;
            this.chart1.Text = "chart1";
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(1239, 736);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 120;
            this.button4.Text = "Debug";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // deleteTransaction
            // 
            this.deleteTransaction.AutoSize = true;
            this.deleteTransaction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteTransaction.BackColor = System.Drawing.SystemColors.Control;
            this.deleteTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteTransaction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteTransaction.Location = new System.Drawing.Point(494, 117);
            this.deleteTransaction.Name = "deleteTransaction";
            this.deleteTransaction.Size = new System.Drawing.Size(54, 23);
            this.deleteTransaction.TabIndex = 125;
            this.deleteTransaction.Text = "Delete";
            this.deleteTransaction.UseVisualStyleBackColor = false;
            this.deleteTransaction.Visible = false;
            this.deleteTransaction.Click += new System.EventHandler(this.deleteTransaction_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 763);
            this.Controls.Add(this.deleteTransaction);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.currentDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dayView);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.dayView.ResumeLayout(false);
            this.dayView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel dayView;
        private System.Windows.Forms.Label currentDate;
        private System.Windows.Forms.Button transactionAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox categoryList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newTransactionDesc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListBox transactions;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox autoMap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button categoryAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox newTransactionAmount;
        private System.Windows.Forms.CheckBox CategoryMap;
        private System.Windows.Forms.Button deleteTransaction;
    }
}

