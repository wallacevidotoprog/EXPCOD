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
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}
		public DataTable DT { get; set; }
		Class_EXP myCLass = new Class_EXP();
		private void Form2_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			myCLass.TRANS(DT);
			this.Close();
		}
	}
}
