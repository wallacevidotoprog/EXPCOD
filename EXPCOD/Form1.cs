using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPCOD
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		

		Class_EXP myCLass = new Class_EXP();Class_X2 X2 = new Class_X2();
		DataTable DTIMP; DataTable DTIMP2;
		string CAMINHO = null;

		private void pictureBox_IMPORT_Click(object sender, EventArgs e)
		{
			DTIMP = new DataTable(); DTIMP2 = new DataTable();

			OpenFileDialog OPEM = new OpenFileDialog();
			OPEM.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			OPEM.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			if (OPEM.ShowDialog() == DialogResult.OK)
			{
				CAMINHO = OPEM.FileName;
				DTIMP = myCLass.TabelaTXT(CAMINHO); DTIMP2 = myCLass.TabelaTXT(CAMINHO);

				int VAZIO = 0;

				string VAZIO_teste = null;

				for (int i = 0; i < DTIMP.Rows.Count; i++)
				{
					VAZIO_teste = Convert.ToString(DTIMP.Rows[i]["COD"]);
					if (VAZIO_teste == "")
					{
						VAZIO = VAZIO + 1;
					}
				}

				
				int TA = Convert.ToInt32(DTIMP.Rows.Count) - VAZIO;

				MessageBox.Show("Você importou "+DTIMP.Rows.Count+ " linhas.\n" + TA + " códigos validos.\n" + VAZIO+" linhas em branco.", "ALERTA",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		private void pictureBox_EXPORT_Click(object sender, EventArgs e)
		{

			if (DTIMP==null || DTIMP.Rows.Count <= 0)
			{
				MessageBox.Show("Importe algum arquivo .txt");
				pictureBox_IMPORT_Click( sender,  e);
			}
			else
			{
				string EXC=null;


				Form_ESCOLHAS FM = new Form_ESCOLHAS();
				FM.ShowDialog();

				EXC = FM.RESULT;

				if (EXC == "BARRA")
				{
					myCLass.TRANS(DTIMP);
				}
				if (EXC == "COD")
				{
					X2.RemoveDuplicates(DTIMP);
					X2.CONTAR(DTIMP2);
				}


			}
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			INIFile ini = new INIFile(Application.StartupPath + "\\settings.ini");


			string data = ini.ReadValue("settings", "DATA");
			string banco = ini.ReadValue("settings", "BANCO");

			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);
			try
			{
				
				conexaoSQL.Open();
				this.pictureBox_BD.Image = global::EXPCOD.Properties.Resources.dbOK;
			}
			catch (Exception)
			{

				this.pictureBox_BD.Image = global::EXPCOD.Properties.Resources.dbNOT;
			}
			finally
			{
				conexaoSQL.Close();
			}
		}

		private void pictureBox_BD_Click(object sender, EventArgs e)
		{
			Form_DB form = new Form_DB();
			form.ShowDialog();
			Form1_Load(sender, e);
		}

		private void pictureBox_IMPORT_MouseHover(object sender, EventArgs e)
		{
			this.pictureBox_IMPORT.Image = global::EXPCOD.Properties.Resources.impC;
		}

		private void pictureBox_IMPORT_MouseLeave(object sender, EventArgs e)
		{

			this.pictureBox_IMPORT.Image = global::EXPCOD.Properties.Resources.imp;
		}

		private void pictureBox_EXPORT_MouseHover(object sender, EventArgs e)
		{
			this.pictureBox_EXPORT.Image = global::EXPCOD.Properties.Resources.expC;
		}

		private void pictureBox_EXPORT_MouseLeave(object sender, EventArgs e)
		{
			this.pictureBox_EXPORT.Image = global::EXPCOD.Properties.Resources.exp;
		}

		private void label1_Click(object sender, EventArgs e)
		{

			
		}
	}
}
