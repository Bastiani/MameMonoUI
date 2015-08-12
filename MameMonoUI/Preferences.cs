using System;
using MadMilkman.Ini;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MameMonoUI
{
	public partial class Preferences : Gtk.Window
	{
		public Preferences () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			IniFile file = new IniFile(new IniOptions());
			if (File.Exists ("preferences.ini")) {
				file.Load("preferences.ini");

				foreach (var section in file.Sections)
				{
					foreach (var key in section.Keys)
					{
						if (key.Name == "MameExe")
							edtMameAppLocation.Text = key.Value;
						else if (key.Name == "Snap")
							edtSnapLocation.Text = key.Value;
						else if (key.Name == "Roms")
							edtRomsLocation.Text = key.Value;
						else if (key.Name == "Flyer")
							edtFlyerLocation.Text = key.Value;
					}
				}
			} else {
				file.Sections.Add(
					new IniSection(file, "MameUI Config",
						new IniKey(file, "MameExe", ""),
						new IniKey(file, "Snap", ""),
						new IniKey(file, "Roms", ""),
						new IniKey(file, "Flyer", "")
					));
				file.Save("preferences.ini");
			}
		}

		private void SavePreferences()
		{
			IniFile file = new IniFile(new IniOptions());
			file.Sections.Add(
				new IniSection(file, "MameUI Config",
					new IniKey(file, "MameExe", edtMameAppLocation.Text),
					new IniKey(file, "Snap", edtSnapLocation.Text),
					new IniKey(file, "Roms", edtRomsLocation.Text),
					new IniKey(file, "Flyer", edtFlyerLocation.Text)
				));
			
			file.Save("preferences.ini");
		}

		protected void OnBtnSaveClicked (object sender, EventArgs e)
		{
			SavePreferences ();
			this.Destroy ();
		}

		protected void OnBtnCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
			
	}
}

