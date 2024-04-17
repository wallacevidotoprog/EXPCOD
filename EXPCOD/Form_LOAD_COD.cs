using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPCOD
{
	public partial class Form_LOAD_COD : Form
	{
		public Form_LOAD_COD()
		{
			InitializeComponent();
		}
		public DataTable REMOVE { get; set; }
		public DataTable DADTS { get; set; }

		private void Form_LOAD_COD_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}


		string data; string banco;
		DataTable DT_NotDup;
		DataTable DT_CONF;
		DataTable DT_CONF_ERRO;
		DataTable FULL;

		string ENVIAR; string RECEBER;
		bool BARRA = false; bool CODV = false;
		string CODVERIF; string RetornoCOD;
		bool FL;

		public void SQL()
		{

			INIFile ini = new INIFile(Application.StartupPath + "\\settings.ini");


			data = ini.ReadValue("settings", "DATA");
			banco = ini.ReadValue("settings", "BANCO");
			FL = Convert.ToBoolean(ini.ReadValue("configs", "FULL"));
		}

		public void DTX()
		{
			DT_NotDup = new DataTable();
			DT_NotDup.Columns.Add("COD", typeof(string));
		}
		public void DTXF()
		{
			DT_CONF = new DataTable();
			DT_CONF.Columns.Add("COD", typeof(string));
			DT_CONF.Columns.Add("QTD", typeof(string));

			DT_CONF_ERRO = new DataTable();
			DT_CONF_ERRO.Columns.Add("COD", typeof(string));
			DT_CONF_ERRO.Columns.Add("QTD", typeof(string));
		}

		public void SEC(DataTable dt)
		{

		}

		public void RemoveDuplicates(DataTable dt)
		{
			DT_NotDup = new DataTable();
			DTX();
			FULL = new DataTable();
			FULL = dt;
			if (dt.Rows.Count > 0)
			{
				for (int i = dt.Rows.Count - 1; i >= 0; i--)
				{
					if (i == 0)
					{
						break;
					}
					for (int j = i - 1; j >= 0; j--)
					{
						if (Convert.ToString(dt.Rows[i]["COD"]) == Convert.ToString(dt.Rows[j]["COD"]))
						{
							dt.Rows[i].Delete();
							break;
						}
					}
				}
				dt.AcceptChanges();
			}
			DT_NotDup = dt;

		}

		public void CONTAR(DataTable DT)
		{
			SQL();
			timer1.Stop();


			DT_CONF = new DataTable();
			DTXF();
			DataRow workRow; DataRow workRow_ERRO;

			progressBar_BARRA.Maximum = Convert.ToInt32(DT_NotDup.Rows.Count.ToString());
			progressBar_BARRA.Minimum = 0;

			for (int i = 0; i < DT_NotDup.Rows.Count; i++)
			{
				string COD = null;
				int QTD = 0;

				COD = Convert.ToString(DT_NotDup.Rows[i]["COD"]);


				for (int j = 0; j < DT.Rows.Count; j++)
				{
					string COD2 = null;
					COD2 = Convert.ToString(DT.Rows[j]["COD"]);

					if (COD == COD2)
					{
						QTD = QTD + 1;
					}

				}

				try
				{
					string pat = "[A-Z]";
					Regex r = new Regex(pat, RegexOptions.IgnoreCase);
					Match m = r.Match(COD);
					bool contem = m.Success;

					if (contem == true)
					{
						CODVERIF = COD;
						buscar_VerificarCOD();

						if (CODV == true)
						{
							workRow = DT_CONF.NewRow();
							workRow[0] = COD.Replace(" ", "");
							workRow[1] = QTD;
							DT_CONF.Rows.Add(workRow);
						}
						else
						{
							workRow_ERRO = DT_CONF_ERRO.NewRow();
							workRow_ERRO[0] = COD.Replace(" ", "");
							workRow_ERRO[1] = QTD;
							DT_CONF_ERRO.Rows.Add(workRow_ERRO);
						}
					}
					else
					{
						ENVIAR = COD;
						buscar_BarraProd();

						if (CODV == true)
						{
							workRow = DT_CONF.NewRow();
							workRow[0] = RECEBER.Replace(" ", "");
							workRow[1] = QTD;
							DT_CONF.Rows.Add(workRow);
						}
						else
						{
							workRow_ERRO = DT_CONF_ERRO.NewRow();
							workRow_ERRO[0] = COD.Replace(" ", "");
							workRow_ERRO[1] = QTD;
							DT_CONF_ERRO.Rows.Add(workRow_ERRO);
						}
					}



				}
				catch (Exception)
				{

					throw;
				}


				progressBar_BARRA.Increment(1);


			}

			SAVE();

		}


		public void buscar_BarraProd()
		{


			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT PRODUTO  FROM PRODUTOS_BARRA WHERE CODIGO_BARRA = '" + ENVIAR + "' ", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					RECEBER = RDR["PRODUTO"].ToString();
					CODV = true;
				}
				else
				{
					CODV = false;
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show("SQL > " + ex);
			}
			finally
			{
				conexaoSQL.Close();
			}
		}


		public void buscar_VerificarCOD()
		{


			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '" + CODVERIF + "' ", conexaoSQL);


			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();

				if (RDR.Read())
				{
					CODV = true;
					//RECEBER = RDR["CODIGO_BARRA"].ToString();
				}
				else
				{
					CODV = false;
					//RECEBER = RDR["CODIGO_BARRA"].ToString();
				}

			}
			catch (Exception ex)
			{

				//MessageBox.Show("buscar_VerificarBarra SQL > " + ex);
			}
			finally
			{
				conexaoSQL.Close();
			}
		}




		public void SAVE()
		{
			SaveFileDialog SFD = new SaveFileDialog();
			SFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			SFD.Filter = "arquivos txt (*.txt)|*.txt";
			string DD = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			if (SFD.ShowDialog() == DialogResult.OK)
			{
				StreamWriter x = new StreamWriter(SFD.FileName);

				try
				{
					for (int i = 0; i < DT_CONF.Rows.Count; i++)
					{
						x.WriteLine(DT_CONF.Rows[i]["COD"] + "," + DT_CONF.Rows[i]["QTD"]);
					}
					if (DT_CONF_ERRO.Rows.Count == 0)
					{

					}
					else
					{
						string TT = (SFD.FileName).Replace(".txt", "_ERROS.txt");
						StreamWriter xE = new StreamWriter(TT);

						try
						{
							for (int i = 0; i < DT_CONF_ERRO.Rows.Count; i++)
							{
								xE.WriteLine(DT_CONF_ERRO.Rows[i]["COD"] + "," + DT_CONF_ERRO.Rows[i]["QTD"]);
							}
						}
						catch (Exception)
						{
							throw;
						}
						finally
						{
							xE.Close();
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
				finally
				{
					x.Close();
				}
			}

			MessageBox.Show("Sucesso: " + DT_CONF.Rows.Count + "\nErro: " + DT_CONF_ERRO.Rows.Count, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{

			RemoveDuplicates(REMOVE);
			CONTAR(DADTS);

		}
	}













}

