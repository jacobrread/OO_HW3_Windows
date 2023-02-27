namespace Forests
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.fileToolStrip = new System.Windows.Forms.ToolStrip();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.drawingToolStrip = new System.Windows.Forms.ToolStrip();
            this.pointerButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.scaleLabel = new System.Windows.Forms.ToolStripLabel();
            this.scale = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.fileToolStrip.SuspendLayout();
            this.drawingToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.BackColor = System.Drawing.Color.White;
            this.drawingPanel.Location = new System.Drawing.Point(204, 129);
            this.drawingPanel.Margin = new System.Windows.Forms.Padding(6);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(1624, 1292);
            this.drawingPanel.TabIndex = 1;
            this.drawingPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingPanel_MouseUp);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // fileToolStrip
            // 
            this.fileToolStrip.AutoSize = false;
            this.fileToolStrip.BackColor = System.Drawing.Color.CadetBlue;
            this.fileToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.fileToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.fileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.saveButton,
            this.deleteButton,
            this.undoButton,
            this.redoButton});
            this.fileToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fileToolStrip.Name = "fileToolStrip";
            this.fileToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.fileToolStrip.Size = new System.Drawing.Size(1828, 123);
            this.fileToolStrip.TabIndex = 2;
            this.fileToolStrip.Text = "toolStrip1";
            // 
            // newButton
            // 
            this.newButton.AutoSize = false;
            this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(61, 61);
            this.newButton.Text = "New";
            this.newButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // openButton
            // 
            this.openButton.AutoSize = false;
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(61, 61);
            this.openButton.Text = "Open Drawing";
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = false;
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(61, 61);
            this.saveButton.Text = "Save Drawing";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(36, 120);
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // drawingToolStrip
            // 
            this.drawingToolStrip.BackColor = System.Drawing.Color.PowderBlue;
            this.drawingToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.drawingToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.drawingToolStrip.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.drawingToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointerButton,
            this.toolStripSeparator2,
            this.scaleLabel,
            this.scale,
            this.toolStripSeparator1});
            this.drawingToolStrip.Location = new System.Drawing.Point(0, 123);
            this.drawingToolStrip.Name = "drawingToolStrip";
            this.drawingToolStrip.Padding = new System.Windows.Forms.Padding(0, 15, 2, 0);
            this.drawingToolStrip.Size = new System.Drawing.Size(196, 1298);
            this.drawingToolStrip.TabIndex = 3;
            this.drawingToolStrip.Text = "toolStrip2";
            // 
            // pointerButton
            // 
            this.pointerButton.AutoSize = false;
            this.pointerButton.CheckOnClick = true;
            this.pointerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pointerButton.Image = ((System.Drawing.Image)(resources.GetObject("pointerButton.Image")));
            this.pointerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointerButton.Name = "pointerButton";
            this.pointerButton.Size = new System.Drawing.Size(61, 50);
            this.pointerButton.Text = "pointerButton";
            this.pointerButton.Click += new System.EventHandler(this.PointerButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // scaleLabel
            // 
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(191, 35);
            this.scaleLabel.Text = "Scale (.01 to 99):";
            this.scaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scale
            // 
            this.scale.AutoSize = false;
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(136, 39);
            this.scale.Text = "1";
            this.scale.Leave += new System.EventHandler(this.Scale_Leave);
            this.scale.TextChanged += new System.EventHandler(this.Scale_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(36, 120);
            this.undoButton.Text = "undoButton";
            this.undoButton.ToolTipText = "Undo";
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(36, 120);
            this.redoButton.Text = "Redo";
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1828, 1421);
            this.Controls.Add(this.drawingToolStrip);
            this.Controls.Add(this.fileToolStrip);
            this.Controls.Add(this.drawingPanel);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "Forest Drawing";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.fileToolStrip.ResumeLayout(false);
            this.fileToolStrip.PerformLayout();
            this.drawingToolStrip.ResumeLayout(false);
            this.drawingToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStrip fileToolStrip;
        private System.Windows.Forms.ToolStripButton newButton;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStrip drawingToolStrip;
        private System.Windows.Forms.ToolStripButton pointerButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel scaleLabel;
        private System.Windows.Forms.ToolStripTextBox scale;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripButton undoButton;
    }
}

