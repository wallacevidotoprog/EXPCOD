using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXPCOD
{
	public partial class Form_ESCOLHAS : Form
	{
		public Form_ESCOLHAS()
		{
			InitializeComponent();
		}
		public string RESULT;

		private void Form_ESCOLHAS_Load(object sender, EventArgs e)
		{

		}

		private void button_BARRA_Click(object sender, EventArgs e)
		{
			RESULT = "BARRA";
			this.Close();
		}

		private void button_COD_Click(object sender, EventArgs e)
		{
			RESULT = "COD";
			this.Close();
		}
	}
}
