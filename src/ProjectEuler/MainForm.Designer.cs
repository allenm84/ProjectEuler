namespace ProjectEuler
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.gridProblems = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.progStatus = new System.Windows.Forms.ProgressBar();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.btnSolve = new System.Windows.Forms.Button();
      this.txtResult = new System.Windows.Forms.TextBox();
      this.gridMessages = new System.Windows.Forms.DataGridView();
      this.clmnText = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label2 = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridMessages)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.gridProblems, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.progStatus, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.gridMessages, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 6;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(364, 480);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // gridProblems
      // 
      this.gridProblems.AllowUserToAddRows = false;
      this.gridProblems.AllowUserToDeleteRows = false;
      this.gridProblems.AllowUserToResizeColumns = false;
      this.gridProblems.AllowUserToResizeRows = false;
      this.gridProblems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.gridProblems.BackgroundColor = System.Drawing.SystemColors.Window;
      this.gridProblems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.gridProblems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.gridProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridProblems.ColumnHeadersVisible = false;
      this.gridProblems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridProblems.Location = new System.Drawing.Point(3, 23);
      this.gridProblems.MultiSelect = false;
      this.gridProblems.Name = "gridProblems";
      this.gridProblems.ReadOnly = true;
      this.gridProblems.RowHeadersVisible = false;
      this.gridProblems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gridProblems.Size = new System.Drawing.Size(358, 154);
      this.gridProblems.TabIndex = 0;
      this.gridProblems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProblems_CellDoubleClick);
      this.gridProblems.SelectionChanged += new System.EventHandler(this.gridProblems_SelectionChanged);
      // 
      // label1
      // 
      this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Problems:";
      // 
      // progStatus
      // 
      this.progStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.progStatus.Location = new System.Drawing.Point(3, 452);
      this.progStatus.Name = "progStatus";
      this.progStatus.Size = new System.Drawing.Size(358, 25);
      this.progStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progStatus.TabIndex = 7;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.btnSolve, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.txtResult, 1, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 180);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(364, 30);
      this.tableLayoutPanel2.TabIndex = 11;
      // 
      // btnSolve
      // 
      this.btnSolve.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnSolve.Location = new System.Drawing.Point(3, 3);
      this.btnSolve.Name = "btnSolve";
      this.btnSolve.Size = new System.Drawing.Size(74, 23);
      this.btnSolve.TabIndex = 1;
      this.btnSolve.Text = "Solve";
      this.btnSolve.UseVisualStyleBackColor = true;
      this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
      // 
      // txtResult
      // 
      this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResult.Location = new System.Drawing.Point(83, 4);
      this.txtResult.Name = "txtResult";
      this.txtResult.Size = new System.Drawing.Size(278, 21);
      this.txtResult.TabIndex = 2;
      // 
      // gridMessages
      // 
      this.gridMessages.AllowUserToAddRows = false;
      this.gridMessages.AllowUserToDeleteRows = false;
      this.gridMessages.AllowUserToResizeColumns = false;
      this.gridMessages.AllowUserToResizeRows = false;
      this.gridMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.gridMessages.BackgroundColor = System.Drawing.SystemColors.Window;
      this.gridMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.gridMessages.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.gridMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridMessages.ColumnHeadersVisible = false;
      this.gridMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnText});
      this.gridMessages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gridMessages.Location = new System.Drawing.Point(3, 233);
      this.gridMessages.MultiSelect = false;
      this.gridMessages.Name = "gridMessages";
      this.gridMessages.ReadOnly = true;
      this.gridMessages.RowHeadersVisible = false;
      this.gridMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gridMessages.Size = new System.Drawing.Size(358, 213);
      this.gridMessages.TabIndex = 10;
      // 
      // clmnText
      // 
      this.clmnText.HeaderText = "Text";
      this.clmnText.Name = "clmnText";
      this.clmnText.ReadOnly = true;
      // 
      // label2
      // 
      this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 213);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(58, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "Messages:";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(364, 480);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Project Euler";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gridMessages)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.DataGridView gridProblems;
    private System.Windows.Forms.Button btnSolve;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ProgressBar progStatus;
    private System.Windows.Forms.DataGridView gridMessages;
    private System.Windows.Forms.DataGridViewTextBoxColumn clmnText;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtResult;
  }
}

