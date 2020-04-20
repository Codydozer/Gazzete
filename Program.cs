using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazette
{
	static class Program
	{
		public static Dictionary<Menu, Form> forms;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			CreateMenus();
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				// args[0] is executable name
				if (args[1].ToLower() == "host")
				{
					SwitchMenu(Menu.HostMenu);
					(forms[Menu.HostMenu] as HostMenu).Connect(args.ElementAtOrDefault(2), args.ElementAtOrDefault(3), args.ElementAtOrDefault(4), args.ElementAtOrDefault(5), args.ElementAtOrDefault(6));
				}
			}
			else
			{
				SwitchMenu(Menu.MainMenu);
			}
			Application.Run();
		}

		static void CreateMenus()
		{
			forms = new Dictionary<Menu, Form>()
			{
				[Menu.MainMenu] = new MainMenu(),
				[Menu.HostMenu] = new HostMenu(),
				[Menu.JoinMenu] = new JoinMenu(),
				[Menu.ChatMenu] = new ChatMenu()
			};

			// If we close a menu, exit the application
			foreach (Form form in forms.Values)
				form.FormClosed += (s, e) => Application.Exit();
		}

		public static void SwitchMenu(Menu menu)
		{
			foreach (Form form in forms.Values)
				form.Hide();

			forms[menu].Show();
		}
	}

	enum Menu
	{
		MainMenu,
		HostMenu,
		JoinMenu,
		ChatMenu
	}
}
