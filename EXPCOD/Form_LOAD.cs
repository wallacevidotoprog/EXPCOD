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
	public partial class Form_LOAD : Form
	{
		public Form_LOAD()
		{
			InitializeComponent();
		}
		Class_EXP myCLass = new Class_EXP();
		public bool SET { get; set; }
		public DataTable DTEXP { get;set ;}
		public DataTable DTIMP { get; set; }
	//	public DataTable DTEXP2 { get; set; }

		private void Form_LOAD_Load(object sender, EventArgs e)
		{
			 myCLass.TRANS(DTIMP);


			this.Close();
		}

		
		}
}
