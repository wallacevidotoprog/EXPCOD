namespace EXPCOD
{
	partial class Form1
	{
		/// <summary>
		/// Variável de designer necessária.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpar os recursos que estão sendo usados.
		/// </summary>
		/// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código gerado pelo Windows Form Designer

		/// <summary>
		/// Método necessário para suporte ao Designer - não modifique 
		/// o conteúdo deste método com o editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.pictureBox_BD = new System.Windows.Forms.PictureBox();
			this.pictureBox_EXPORT = new System.Windows.Forms.PictureBox();
			this.pictureBox_IMPORT = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_BD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_EXPORT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_IMPORT)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox_BD
			// 
			this.pictureBox_BD.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox_BD.Image = global::EXPCOD.Properties.Resources.dbNOT;
			this.pictureBox_BD.Location = new System.Drawing.Point(489, 12);
			this.pictureBox_BD.Name = "pictureBox_BD";
			this.pictureBox_BD.Size = new System.Drawing.Size(46, 40);
			this.pictureBox_BD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_BD.TabIndex = 1;
			this.pictureBox_BD.TabStop = false;
			this.pictureBox_BD.Click += new System.EventHandler(this.pictureBox_BD_Click);
			// 
			// pictureBox_EXPORT
			// 
			this.pictureBox_EXPORT.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox_EXPORT.Image = global::EXPCOD.Properties.Resources.exp;
			this.pictureBox_EXPORT.Location = new System.Drawing.Point(258, 12);
			this.pictureBox_EXPORT.Name = "pictureBox_EXPORT";
			this.pictureBox_EXPORT.Size = new System.Drawing.Size(225, 225);
			this.pictureBox_EXPORT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox_EXPORT.TabIndex = 1;
			this.pictureBox_EXPORT.TabStop = false;
			this.pictureBox_EXPORT.Click += new System.EventHandler(this.pictureBox_EXPORT_Click);
			this.pictureBox_EXPORT.MouseLeave += new System.EventHandler(this.pictureBox_EXPORT_MouseLeave);
			this.pictureBox_EXPORT.MouseHover += new System.EventHandler(this.pictureBox_EXPORT_MouseHover);
			// 
			// pictureBox_IMPORT
			// 
			this.pictureBox_IMPORT.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox_IMPORT.Image = global::EXPCOD.Properties.Resources.imp;
			this.pictureBox_IMPORT.Location = new System.Drawing.Point(12, 12);
			this.pictureBox_IMPORT.Name = "pictureBox_IMPORT";
			this.pictureBox_IMPORT.Size = new System.Drawing.Size(225, 225);
			this.pictureBox_IMPORT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox_IMPORT.TabIndex = 1;
			this.pictureBox_IMPORT.TabStop = false;
			this.pictureBox_IMPORT.Click += new System.EventHandler(this.pictureBox_IMPORT_Click);
			this.pictureBox_IMPORT.MouseLeave += new System.EventHandler(this.pictureBox_IMPORT_MouseLeave);
			this.pictureBox_IMPORT.MouseHover += new System.EventHandler(this.pictureBox_IMPORT_MouseHover);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(546, 243);
			this.Controls.Add(this.pictureBox_BD);
			this.Controls.Add(this.pictureBox_EXPORT);
			this.Controls.Add(this.pictureBox_IMPORT);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CONVERTER CÓDIGO COMUM EM BARRA";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_BD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_EXPORT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_IMPORT)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox_IMPORT;
		private System.Windows.Forms.PictureBox pictureBox_EXPORT;
		private System.Windows.Forms.PictureBox pictureBox_BD;
	}
}

