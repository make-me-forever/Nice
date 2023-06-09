﻿namespace Nice
{
    partial class MainPage
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.PathEditArea = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.LogButton = new System.Windows.Forms.Button();
            this.MenuTransform = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PathEditArea
            // 
            this.PathEditArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PathEditArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PathEditArea.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.PathEditArea.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PathEditArea.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.PathEditArea.Location = new System.Drawing.Point(44, 30);
            this.PathEditArea.Margin = new System.Windows.Forms.Padding(2);
            this.PathEditArea.Multiline = false;
            this.PathEditArea.Name = "PathEditArea";
            this.PathEditArea.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PathEditArea.Size = new System.Drawing.Size(369, 31);
            this.PathEditArea.TabIndex = 4;
            this.PathEditArea.TabStop = false;
            this.PathEditArea.Text = "";
            this.PathEditArea.UseWaitCursor = true;
            this.PathEditArea.MouseLeave += new System.EventHandler(this.PathEditArea_MouseLeave);
            this.PathEditArea.MouseHover += new System.EventHandler(this.PathEditArea_MouseHover);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.StartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.StartButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.StartButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.StartButton.Location = new System.Drawing.Point(375, 241);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(57, 33);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "退出";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.Start_Click);
            // 
            // LogButton
            // 
            this.LogButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.LogButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LogButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.LogButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LogButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LogButton.Location = new System.Drawing.Point(31, 241);
            this.LogButton.Margin = new System.Windows.Forms.Padding(2);
            this.LogButton.Name = "LogButton";
            this.LogButton.Size = new System.Drawing.Size(57, 33);
            this.LogButton.TabIndex = 13;
            this.LogButton.Text = "日志";
            this.LogButton.UseVisualStyleBackColor = false;
            this.LogButton.Click += new System.EventHandler(this.Log_Click);
            // 
            // MenuTransform
            // 
            this.MenuTransform.BackColor = System.Drawing.Color.Honeydew;
            this.MenuTransform.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MenuTransform.BackgroundImage")));
            this.MenuTransform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MenuTransform.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.MenuTransform.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MenuTransform.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MenuTransform.Location = new System.Drawing.Point(202, 215);
            this.MenuTransform.Margin = new System.Windows.Forms.Padding(2);
            this.MenuTransform.Name = "MenuTransform";
            this.MenuTransform.Size = new System.Drawing.Size(40, 40);
            this.MenuTransform.TabIndex = 15;
            this.MenuTransform.UseVisualStyleBackColor = false;
            this.MenuTransform.Click += new System.EventHandler(this.Transform_Click);
            // 
            // MainPage
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(463, 307);
            this.Controls.Add(this.MenuTransform);
            this.Controls.Add(this.LogButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PathEditArea);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainPage";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "昆易工具";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainPage_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainPage_DragEnter);
            this.DragLeave += new System.EventHandler(this.MainPage_DragLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox PathEditArea;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button LogButton;
        private System.Windows.Forms.Button MenuTransform;
    }
}

