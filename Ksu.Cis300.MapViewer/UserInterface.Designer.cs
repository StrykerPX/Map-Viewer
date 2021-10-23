
namespace Ksu.Cis300.MapViewer
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.uxButtonBar = new System.Windows.Forms.ToolStrip();
            this.uxOpen = new System.Windows.Forms.ToolStripButton();
            this.uxZoomIn = new System.Windows.Forms.ToolStripButton();
            this.uxZoomOut = new System.Windows.Forms.ToolStripButton();
            this.uxMapContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.uxOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.Map = new Ksu.Cis300.MapViewer.Map();
            this.uxButtonBar.SuspendLayout();
            this.uxMapContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxButtonBar
            // 
            this.uxButtonBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxOpen,
            this.uxZoomIn,
            this.uxZoomOut});
            this.uxButtonBar.Location = new System.Drawing.Point(0, 0);
            this.uxButtonBar.Name = "uxButtonBar";
            this.uxButtonBar.Size = new System.Drawing.Size(800, 25);
            this.uxButtonBar.TabIndex = 0;
            this.uxButtonBar.Text = "toolStrip1";
            // 
            // uxOpen
            // 
            this.uxOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxOpen.Image = ((System.Drawing.Image)(resources.GetObject("uxOpen.Image")));
            this.uxOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxOpen.Name = "uxOpen";
            this.uxOpen.Size = new System.Drawing.Size(67, 22);
            this.uxOpen.Text = "Open Map";
            this.uxOpen.Click += new System.EventHandler(this.uxOpen_Click);
            // 
            // uxZoomIn
            // 
            this.uxZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomIn.Enabled = false;
            this.uxZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomIn.Image")));
            this.uxZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomIn.Name = "uxZoomIn";
            this.uxZoomIn.Size = new System.Drawing.Size(56, 22);
            this.uxZoomIn.Text = "Zoom In";
            this.uxZoomIn.Click += new System.EventHandler(this.uxZoomIn_Click);
            // 
            // uxZoomOut
            // 
            this.uxZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomOut.Enabled = false;
            this.uxZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomOut.Image")));
            this.uxZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomOut.Name = "uxZoomOut";
            this.uxZoomOut.Size = new System.Drawing.Size(66, 22);
            this.uxZoomOut.Text = "Zoom Out";
            this.uxZoomOut.Click += new System.EventHandler(this.uxZoomOut_Click);
            // 
            // uxMapContainer
            // 
            this.uxMapContainer.AutoScroll = true;
            this.uxMapContainer.Controls.Add(this.Map);
            this.uxMapContainer.Location = new System.Drawing.Point(0, 28);
            this.uxMapContainer.Name = "uxMapContainer";
            this.uxMapContainer.Size = new System.Drawing.Size(800, 410);
            this.uxMapContainer.TabIndex = 1;
            // 
            // uxOpenDialog
            // 
            this.uxOpenDialog.FileName = "openFileDialog1";
            this.uxOpenDialog.Filter = "CSV files|*.csv|All files|*.*";
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.Color.White;
            this.Map.Location = new System.Drawing.Point(3, 3);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(150, 150);
            this.Map.TabIndex = 0;
            this.Map.Tree = null;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uxMapContainer);
            this.Controls.Add(this.uxButtonBar);
            this.Name = "UserInterface";
            this.Text = "Form1";
            this.uxButtonBar.ResumeLayout(false);
            this.uxButtonBar.PerformLayout();
            this.uxMapContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip uxButtonBar;
        private System.Windows.Forms.ToolStripButton uxOpen;
        private System.Windows.Forms.ToolStripButton uxZoomIn;
        private System.Windows.Forms.ToolStripButton uxZoomOut;
        private System.Windows.Forms.FlowLayoutPanel uxMapContainer;
        private Map map1;
        private Map Map;
        private System.Windows.Forms.OpenFileDialog uxOpenDialog;
    }
}

