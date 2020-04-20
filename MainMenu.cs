using MySql.Data.MySqlClient;
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

namespace Gazette
{
	public partial class MainMenu : Form
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{

		}

		private void ButtonHost_Click(object sender, EventArgs e)
		{
			Program.SwitchMenu(Gazette.Menu.HostMenu);
		}

		private void ButtonJoin_Click(object sender, EventArgs e)
		{
			Program.SwitchMenu(Gazette.Menu.JoinMenu);
		}

		private void ButtonQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
