namespace EXPCOD
{
	partial class Form_DB
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
			this.button_GRAVAR = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_DATA = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_BANCO = new System.Windows.Forms.TextBox();
			this.textBox_USER = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_SENHA = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button_TESTE = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox_MODOFULL = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_GRAVAR
			// 
			this.button_GRAVAR.Location = new System.Drawing.Point(162, 286);
			this.button_GRAVAR.Margin = new System.Windows.Forms.Padding(4);
			this.button_GRAVAR.Name = "button_GRAVAR";
			this.button_GRAVAR.Size = new System.Drawing.Size(100, 28);
			this.button_GRAVAR.TabIndex = 0;
			this.button_GRAVAR.Text = "GRAVAR";
			this.button_GRAVAR.UseVisualStyleBackColor = true;
			this.button_GRAVAR.Click += new System.EventHandler(this.button_GRAVAR_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "DATA:";
			// 
			// textBox_DATA
			// 
			this.textBox_DATA.Location = new System.Drawing.Point(16, 30);
			this.textBox_DATA.Margin = new System.Windows.Forms.Padding(4);
			this.textBox_DATA.Name = "textBox_DATA";
			this.textBox_DATA.Size = new System.Drawing.Size(246, 23);
			this.textBox_DATA.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 153);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "BANCO:";
			// 
			// textBox_BANCO
			// 
			this.textBox_BANCO.Location = new System.Drawing.Point(16, 174);
			this.textBox_BANCO.Margin = new System.Windows.Forms.Padding(4);
			this.textBox_BANCO.Name = "textBox_BANCO";
			this.textBox_BANCO.Size = new System.Drawing.Size(246, 23);
			this.textBox_BANCO.TabIndex = 2;
			// 
			// textBox_USER
			// 
			this.textBox_USER.Location = new System.Drawing.Point(16, 78);
			this.textBox_USER.Margin = new System.Windows.Forms.Padding(4);
			this.textBox_USER.Name = "textBox_USER";
			this.textBox_USER.Size = new System.Drawing.Size(246, 23);
			this.textBox_USER.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 57);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "USUÁRIO:";
			// 
			// textBox_SENHA
			// 
			this.textBox_SENHA.Location = new System.Drawing.Point(16, 126);
			this.textBox_SENHA.Margin = new System.Windows.Forms.Padding(4);
			this.textBox_SENHA.Name = "textBox_SENHA";
			this.textBox_SENHA.Size = new System.Drawing.Size(246, 23);
			this.textBox_SENHA.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 105);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 17);
			this.label4.TabIndex = 5;
			this.label4.Text = "SENHA:";
			// 
			// button_TESTE
			// 
			this.button_TESTE.Location = new System.Drawing.Point(16, 286);
			this.button_TESTE.Margin = new System.Windows.Forms.Padding(4);
			this.button_TESTE.Name = "button_TESTE";
			this.button_TESTE.Size = new System.Drawing.Size(126, 28);
			this.button_TESTE.TabIndex = 0;
			this.button_TESTE.Text = "Testar conexão";
			this.button_TESTE.UseVisualStyleBackColor = true;
			this.button_TESTE.Click += new System.EventHandler(this.button_TESTE_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox_MODOFULL);
			this.groupBox1.Location = new System.Drawing.Point(16, 204);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 51);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Código não sequenciais";
			// 
			// checkBox_MODOFULL
			// 
			this.checkBox_MODOFULL.AutoSize = true;
			this.checkBox_MODOFULL.Location = new System.Drawing.Point(17, 22);
			this.checkBox_MODOFULL.Name = "checkBox_MODOFULL";
			this.checkBox_MODOFULL.Size = new System.Drawing.Size(215, 21);
			this.checkBox_MODOFULL.TabIndex = 0;
			this.checkBox_MODOFULL.Text = "Modo qualquer série de barra";
			this.checkBox_MODOFULL.UseVisualStyleBackColor = true;
			// 
			// Form_DB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(275, 327);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox_SENHA);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox_USER);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox_BANCO);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox_DATA);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_TESTE);
			this.Controls.Add(this.button_GRAVAR);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form_DB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form_DB";
			this.Load += new System.EventHandler(this.Form_DB_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_GRAVAR;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_DATA;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_BANCO;
		private System.Windows.Forms.TextBox textBox_USER;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_SENHA;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button_TESTE;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox_MODOFULL;
	}
}