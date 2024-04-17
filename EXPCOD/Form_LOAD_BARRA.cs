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
	public partial class Form_LOAD_BARRA : Form
	{
		public Form_LOAD_BARRA()
		{
			InitializeComponent();
		}
		public DataTable BAD { get; set; }
		private void Form_LOAD_BARRA_Load(object sender, EventArgs e)
		{
			//TRANS(BAD);
			SQL();
			timer1.Start();
			//this.Close();
		}



		DataTable DTTXT; DataTable DTTXT_ERROS;

		string ENVIAR; string RECEBER;
		string data; string banco;
		bool BARRA = false; bool COD = false;
		string BARRAVERIF;
		bool FULL;


		public void TRANS(DataTable X)
		{
			timer1.Stop();
			

			
			progressBar_BARRA.Maximum = Convert.ToInt32(X.Rows.Count.ToString());
			progressBar_BARRA.Minimum = 0;
			try
			{
				DTTXT = new DataTable();

				DTTXT_ERROS = new DataTable();

				DTTXT.Columns.Add("COD", typeof(string));
				DTTXT_ERROS.Columns.Add("COD", typeof(string));

				DTTXT.Clear();
				DTTXT_ERROS.Clear();

				DataRow workRow; DataRow workRow_ERRO;

				string codIM;

				for (int i = 0; i < X.Rows.Count; i++)
				{
					ENVIAR = null; RECEBER = null;

					string pat = "[A-Z]";

					codIM = Convert.ToString(X.Rows[i]["COD"]);


					Regex r = new Regex(pat, RegexOptions.IgnoreCase);

					if (codIM == "")
					{

					}
					else
					{
						Match m = r.Match(codIM);

						bool contem = m.Success;

						if (contem == true)
						{
							ENVIAR = codIM;

							//if (FULL == true)
							//{
							//	buscar_ProdBarra2();
							//}
							//else
							//{
							buscar_ProdBarra();
							//}

						}
						else
						{
							RECEBER = codIM;
						}


						if (RECEBER == null || RECEBER == "")
						{
							RECEBER = ENVIAR;
						}


						Match m3 = r.Match(RECEBER);
						bool contem2 = m3.Success;

						if (contem2 == true || RECEBER == "")
						{




							if (FULL == true)
							{
								buscar_ProdBarra2();
								BARRAVERIF = RECEBER;
								buscar_VerificarBarra();
							}
							else
							{
								BARRAVERIF = RECEBER;
								buscar_VerificarBarra();

							}



							if (BARRA == true)
							{
								workRow = DTTXT.NewRow();
								workRow[0] = RECEBER.Replace(" ", "");
								DTTXT.Rows.Add(workRow);
							}

							if (BARRA == false)
							{
								buscar_ProdBarra();

								workRow_ERRO = DTTXT_ERROS.NewRow();
								workRow_ERRO[0] = RECEBER.Replace(" ", "");
								DTTXT_ERROS.Rows.Add(workRow_ERRO);
							}




							//workRow_ERRO = DTTXT_ERROS.NewRow();
							//workRow_ERRO[0] = RECEBER.Replace(" ", "");
							//DTTXT_ERROS.Rows.Add(workRow_ERRO);
						}
						else
						{
							Match m4 = r.Match(RECEBER);
							bool contem4 = m4.Success;

							if (contem4 == false)
							{
								BARRAVERIF = RECEBER;
								buscar_VerificarBarra();



								if (FULL == true)
								{
									buscar_ProdBarra2();
									BARRAVERIF = RECEBER;
									buscar_VerificarBarra();
								}
								else
								{
									BARRAVERIF = RECEBER;
									buscar_VerificarBarra();

								}
							}


							if (BARRA == true)
							{
								workRow = DTTXT.NewRow();
								workRow[0] = RECEBER.Replace(" ", "");
								DTTXT.Rows.Add(workRow);
							}

							if (BARRA == false)
							{

								//if (FULL == true)
								//{
								//	buscar_ProdBarra2();
								//}
								//else
								//{
								buscar_ProdBarra();
								//}
															   								 

								workRow_ERRO = DTTXT_ERROS.NewRow();
								workRow_ERRO[0] = RECEBER.Replace(" ", "");
								DTTXT_ERROS.Rows.Add(workRow_ERRO);
							}

						}
					}

					progressBar_BARRA.Increment(1);
					






				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex);
			}
			SAVE();
		}

		public void SQL()
		{

			INIFile ini = new INIFile(Application.StartupPath + "\\settings.ini");


			data = ini.ReadValue("settings", "DATA");
			banco = ini.ReadValue("settings", "BANCO");
			FULL = Convert.ToBoolean(ini.ReadValue("configs", "FULL"));
		}

		public void buscar_ProdBarra()
		{


			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT CODIGO_BARRA ,PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '" + ENVIAR + "' AND CODIGO_BARRA LIKE '7%'", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					RECEBER = RDR["CODIGO_BARRA"].ToString().Replace(" ", "");
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

		public void buscar_ProdBarra2()
		{


			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT TOP 1  CODIGO_BARRA ,PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '" + ENVIAR + "' ORDER BY DATA_PARA_TRANSFERENCIA desc", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					RECEBER = RDR["CODIGO_BARRA"].ToString().Replace(" ", "");
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


		public void buscar_VerificarBarra()
		{


			string strConn = (@"Server=" + data + ";Database=" + banco + ";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT CODIGO_BARRA  FROM PRODUTOS_BARRA WHERE CODIGO_BARRA = '" + BARRAVERIF + "' ", conexaoSQL);


			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();

				if (RDR.Read())
				{
					BARRA = true;
					//RECEBER = RDR["CODIGO_BARRA"].ToString();
				}
				else
				{
					BARRA = false;
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
					for (int i = 0; i < DTTXT.Rows.Count; i++)
					{
						x.WriteLine(DTTXT.Rows[i]["COD"]);
					}
					if (DTTXT_ERROS.Rows.Count == 0)
					{

					}
					else
					{
						string TT = (SFD.FileName).Replace(".txt", "_ERROS.txt");
						StreamWriter xE = new StreamWriter(TT);

						try
						{
							for (int i = 0; i < DTTXT_ERROS.Rows.Count; i++)
							{
								xE.WriteLine(DTTXT_ERROS.Rows[i]["COD"]);
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

			MessageBox.Show("Sucesso: " + DTTXT.Rows.Count + "\nErro: " + DTTXT_ERROS.Rows.Count, "Relatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			TRANS(BAD);
		}
	}

}

