namespace EXPCOD
{
	partial class Form_ESCOLHAS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ESCOLHAS));
			this.button_COD = new System.Windows.Forms.Button();
			this.button_BARRA = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_COD
			// 
			this.button_COD.Image = global::EXPCOD.Properties.Resources.codSec;
			this.button_COD.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button_COD.Location = new System.Drawing.Point(220, 12);
			this.button_COD.Name = "button_COD";
			this.button_COD.Size = new System.Drawing.Size(202, 179);
			this.button_COD.TabIndex = 0;
			this.button_COD.Text = "CÓDOGO DO PRODUTO";
			this.button_COD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.button_COD.UseVisualStyleBackColor = true;
			this.button_COD.Click += new System.EventHandler(this.button_COD_Click);
			// 
			// button_BARRA
			// 
			this.button_BARRA.Image = global::EXPCOD.Properties.Resources.codeBarr;
			this.button_BARRA.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button_BARRA.Location = new System.Drawing.Point(12, 12);
			this.button_BARRA.Name = "button_BARRA";
			this.button_BARRA.Size = new System.Drawing.Size(202, 179);
			this.button_BARRA.TabIndex = 0;
			this.button_BARRA.Text = "CÓDOGO DE BARRAS";
			this.button_BARRA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.button_BARRA.UseVisualStyleBackColor = true;
			this.button_BARRA.Click += new System.EventHandler(this.button_BARRA_Click);
			// 
			// Form_ESCOLHAS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(433, 197);
			this.Controls.Add(this.button_COD);
			this.Controls.Add(this.button_BARRA);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_ESCOLHAS";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Formas de exportações";
			this.Load += new System.EventHandler(this.Form_ESCOLHAS_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_BARRA;
		private System.Windows.Forms.Button button_COD;
	}
}