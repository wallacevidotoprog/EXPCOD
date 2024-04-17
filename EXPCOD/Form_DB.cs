using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPCOD
{
	public partial class Form_DB : Form
	{
		public Form_DB()
		{
			InitializeComponent();
		}

		INIFile ini = new INIFile(Application.StartupPath + "\\settings.ini");

		private void button_TESTE_Click(object sender, EventArgs e)
		{
			
			ini.Write("settings", "DATA", textBox_DATA.Text);
			ini.Write("settings", "BANCO", textBox_BANCO.Text);
			ini.Write("settings", "USER", textBox_USER.Text);
			ini.Write("settings", "PWD", textBox_SENHA.Text);

			string DATA = ini.ReadValue("settings", "DATA");
			string BANCO = ini.ReadValue("settings", "BANCO");
			string USER = ini.ReadValue("settings", "USER");
			string PWD = ini.ReadValue("settings", "PWD");

			string strConn2 = (@"Server=" + DATA + ";Database=" + BANCO + ";Integrated Security=SSPI;Persist Security Info=True;");
			string strConn = (@"Server=" + DATA + ";Database=" + BANCO + ";Uid="+ USER + "	Pwd="+ PWD + ";");


			SqlConnection conexaoSQL = new SqlConnection(strConn);
			try
			{

				conexaoSQL.Open();
				MessageBox.Show("CONEXÃO OK");
				conexaoSQL.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("ERRO NA CONEXÃO");
			}
		}

		private void Form_DB_Load(object sender, EventArgs e)
		{
			INIFile ini = new INIFile(Application.StartupPath + "\\settings.ini");


			string DATA = ini.ReadValue("settings", "DATA");
			string BANCO = ini.ReadValue("settings", "BANCO");
			string USER = ini.ReadValue("settings", "USER");
			string PWD = ini.ReadValue("settings", "PWD");
			bool FULL = Convert.ToBoolean(ini.ReadValue("configs", "FULL"));

			textBox_DATA.Text = DATA;
			textBox_BANCO.Text = BANCO;
			textBox_USER.Text = USER;
			textBox_SENHA.Text = PWD;
			checkBox_MODOFULL.Checked = FULL;
		}

		private void button_GRAVAR_Click(object sender, EventArgs e)
		{
			
			ini.Write("settings", "DATA", textBox_DATA.Text);
			ini.Write("settings", "BANCO", textBox_BANCO.Text);
			ini.Write("settings", "USER", textBox_USER.Text);
			ini.Write("settings", "PWD", textBox_SENHA.Text);
			ini.Write("configs", "FULL", Convert.ToString(checkBox_MODOFULL.Checked));

			MessageBox.Show("Dados salvos.","ALERTA",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
	}
}
