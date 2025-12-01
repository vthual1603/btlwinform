namespace QLKhachSan
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.QuanlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dichvuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClickCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.đặtPhòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThanhtoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QuanlyToolStripMenuItem,
            this.ClickCustomer,
            this.đặtPhòngToolStripMenuItem,
            this.ServiceToolStripMenuItem,
            this.ThanhtoanToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1382, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // QuanlyToolStripMenuItem
            // 
            this.QuanlyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phongToolStripMenuItem,
            this.dichvuToolStripMenuItem});
            this.QuanlyToolStripMenuItem.Name = "QuanlyToolStripMenuItem";
            this.QuanlyToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.QuanlyToolStripMenuItem.Text = "Quản Lý";
            // 
            // phongToolStripMenuItem
            // 
            this.phongToolStripMenuItem.Name = "phongToolStripMenuItem";
            this.phongToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.phongToolStripMenuItem.Text = "Phòng";
            this.phongToolStripMenuItem.Click += new System.EventHandler(this.phongToolStripMenuItem_Click);
            // 
            // dichvuToolStripMenuItem
            // 
            this.dichvuToolStripMenuItem.Name = "dichvuToolStripMenuItem";
            this.dichvuToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.dichvuToolStripMenuItem.Text = "Dịch vụ";
            this.dichvuToolStripMenuItem.Click += new System.EventHandler(this.dichvuToolStripMenuItem_Click);
            // 
            // ClickCustomer
            // 
            this.ClickCustomer.Name = "ClickCustomer";
            this.ClickCustomer.Size = new System.Drawing.Size(103, 24);
            this.ClickCustomer.Text = "Khách Hàng";
            this.ClickCustomer.Click += new System.EventHandler(this.ClickCustomer_Click);
            // 
            // đặtPhòngToolStripMenuItem
            // 
            this.đặtPhòngToolStripMenuItem.Name = "đặtPhòngToolStripMenuItem";
            this.đặtPhòngToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.đặtPhòngToolStripMenuItem.Text = "Đặt Phòng";
            this.đặtPhòngToolStripMenuItem.Click += new System.EventHandler(this.đặtPhòngToolStripMenuItem_Click);
            // 
            // ServiceToolStripMenuItem
            // 
            this.ServiceToolStripMenuItem.Name = "ServiceToolStripMenuItem";
            this.ServiceToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.ServiceToolStripMenuItem.Text = "Đặt Dịch Vụ";
            this.ServiceToolStripMenuItem.Click += new System.EventHandler(this.ServiceToolStripMenuItem_Click);
            // 
            // ThanhtoanToolStripMenuItem
            // 
            this.ThanhtoanToolStripMenuItem.Name = "ThanhtoanToolStripMenuItem";
            this.ThanhtoanToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.ThanhtoanToolStripMenuItem.Text = "Hóa Đơn";
            this.ThanhtoanToolStripMenuItem.Click += new System.EventHandler(this.ThanhtoanToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 28);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1382, 1025);
            this.pnlContent.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1382, 1053);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ClickCustomer;
        private System.Windows.Forms.ToolStripMenuItem đặtPhòngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ThanhtoanToolStripMenuItem;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripMenuItem QuanlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dichvuToolStripMenuItem;
    }
}