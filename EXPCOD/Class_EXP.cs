using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPCOD
{
	public class Class_EXP
	{
		//Form1 GM;

		DataTable DTTXT; DataTable DTTXT_ERROS;

		string ENVIAR; string RECEBER;
		string data;		string banco;
		bool BARRA =false; bool COD = false;
		string BARRAVERIF;
		bool FULL;
		bool CONTEM;

		public DataTable TabelaTXT(string CAMINHO)
		{
			SQL();

			DataTable DTTXT = new DataTable();

			DTTXT.Columns.Add("COD", typeof(string));

			DTTXT.Clear();

			string[] lines = File.ReadAllLines(CAMINHO);
			string[] values;


			for (int i = 0; i < lines.Length; i++)
			{
				values = lines[i].ToString().Split('"');
				string[] row = new string[values.Length];

				for (int j = 0; j < values.Length; j++)
				{
					row[j] = values[j].Trim();
				}
				DTTXT.Rows.Add(row);
			}

			return DTTXT;
		}	

		public void TRANS(DataTable X)
		{
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
							buscar_ProdBarra();
						}
						else
						{
							RECEBER = codIM;//verificar
						}


						if (RECEBER == null || RECEBER == "")
						{
							RECEBER = ENVIAR;
						}


						Match m3 = r.Match(RECEBER);
						bool contem2 = m3.Success;

						if (contem2 == true || RECEBER == "")
						{


							if (CONTEM == true)
							{
								ENVIAR = RECEBER;

								buscar_ProdBarra2();

								if (RECEBER!=null || RECEBER != "")
								{
									workRow = DTTXT.NewRow();
									workRow[0] = RECEBER.Replace(" ", "");
									DTTXT.Rows.Add(workRow);
								}
								else
								{
									workRow_ERRO = DTTXT_ERROS.NewRow();
									workRow_ERRO[0] = RECEBER.Replace(" ", "");
									DTTXT_ERROS.Rows.Add(workRow_ERRO);
								}
								

							}
							else
							{
								workRow_ERRO = DTTXT_ERROS.NewRow();
								workRow_ERRO[0] = RECEBER.Replace(" ", "");
								DTTXT_ERROS.Rows.Add(workRow_ERRO);
							}

						}
						else
						{
							if (BARRA == true)
							{
									workRow = DTTXT.NewRow();
									workRow[0] = RECEBER.Replace(" ", "");
									DTTXT.Rows.Add(workRow);
							}
								
							if (BARRA == false)
							{
						
								workRow_ERRO = DTTXT_ERROS.NewRow();
									workRow_ERRO[0] = RECEBER.Replace(" ", "");
									DTTXT_ERROS.Rows.Add(workRow_ERRO);
							}					
													
						}
					}

					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" +ex);
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


			string strConn = (@"Server="+data+";Database="+banco+";Integrated Security=SSPI;Persist Security Info=True;");

			SqlConnection conexaoSQL = new SqlConnection(strConn);




			SqlCommand cmd = new SqlCommand("SELECT CODIGO_BARRA ,PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '"+ ENVIAR + "' AND CODIGO_BARRA LIKE '7%'", conexaoSQL);
			SqlCommand cmd2 = new SqlCommand("SELECT PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '" + ENVIAR + "'", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd.ExecuteReader();
				if (RDR.Read())
				{
					RECEBER = RDR["CODIGO_BARRA"].ToString().Replace(" ", "");
					BARRA = true;
					CONTEM = true;
				}
				else
				{
					conexaoSQL.Close(); conexaoSQL.Open();

					SqlDataReader RDR2 = cmd2.ExecuteReader();
					if (RDR2.Read())
					{
						BARRA = false;
						CONTEM = true;
					}
					else
					{
						BARRA = false;
						CONTEM = false;
					}
					
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
			SqlCommand cmd2 = new SqlCommand("SELECT TOP 1  CODIGO_BARRA ,PRODUTO  FROM PRODUTOS_BARRA WHERE PRODUTO = '" + ENVIAR + "' ORDER BY   CODIGO_BARRA asc", conexaoSQL);

			try
			{
				conexaoSQL.Open();
				SqlDataReader RDR = cmd2.ExecuteReader();
				if (RDR.Read())
				{
					RECEBER = RDR["CODIGO_BARRA"].ToString().Replace(" ", "");
					BARRA = true;
				}
				else
				{
					BARRA = false;
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
			
			MessageBox.Show("Sucesso: "+ DTTXT.Rows.Count+"\nErro: "+ DTTXT_ERROS.Rows.Count, "Relatório",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

















	}
}
