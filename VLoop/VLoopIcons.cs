using System;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// This class contains the static functions for 
	/// getting all the Icon resources in VLoop.
	/// </summary>
	public class VLoopIcons
	{
		public static Icon VLoopHome
		{
			get { return GetIcon("home_h.ico"); }
		}

		public static Icon Customer
		{
			get { return GetIcon("people.ico"); }
		}

		public static Icon CallHistory
		{
			get { return GetIcon("time.ico"); }
		}

		public static Icon Globe
		{
			get { return GetIcon("world.ico"); }
		}

		public static Icon Help
		{
			get { return GetIcon("help.ico"); }
		}

		public static Icon Account
		{
			get { return GetIcon("acct.ico"); }
		}

		public static Icon NewItem
		{
			get { return GetIcon("new.ico"); }
		}

		public static Icon Delete
		{
			get { return GetIcon("del.ico"); }
		}

		public static Icon Save
		{
			get { return GetIcon("save_g.ico"); }
		}

		public static Icon Undo
		{
			get { return GetIcon("undo_h.ico"); }
		}

		public static Icon Edit
		{
			get { return GetIcon("edit.ico"); }
		}

		public static Icon WTN
		{
			get { return GetIcon("main.ico"); }
		}

		public static Icon Next
		{
			get { return GetIcon("next.ico"); }
		}

		public static Icon Search
		{
			get { return GetIcon("search48.ico"); }
		}

		public static Icon EndCall
		{
			get { return GetIcon("endcall.ico"); }
		}

		public static Icon OpenFolder
		{
			get { return GetIcon("folder_open.ico"); }
		}

		public static Icon ClosedFolder
		{
			get { return GetIcon("folder_closed.ico"); }
		}

		public static Icon ChevronDown_XPBlue
		{
			get { return GetIcon("chevron_down.ico"); }
		}

		public static Icon ChevronDown_Win2K
		{
			get { return GetIcon("chevron_down_2000.ico"); }
		}

		public static Icon ChevronDownHover_XPBlue
		{
			get { return GetIcon("chevron_down_hover.ico"); }
		}

		public static Icon ChevronDownHover_Win2K
		{
			get { return GetIcon("chevron_down_hover_2000.ico"); }
		}

		public static Icon ChevronUp_XPBlue
		{
			get { return GetIcon("chevron_up.ico"); }
		}

		public static Icon ChevronUp_Win2K
		{
			get { return GetIcon("chevron_up_2000.ico"); }
		}

		public static Icon ChevronUpHover_XPBlue
		{
			get { return GetIcon("chevron_up_hover.ico"); }
		}

		public static Icon ChevronUpHover_Win2K
		{
			get { return GetIcon("chevron_up_hover_2000.ico"); }
		}

		private static Icon GetIcon(string name)
		{
			string qname;
			
			qname = "Afni.Applications.VLoop.Resources." + name;
			//qname = this.GetType().Namespace + ".Resources." + name;
			//Assembly assembly = this.GetType().Assembly;
			Assembly assembly = Assembly.GetCallingAssembly();
			
			Stream iconStream =
			assembly.GetManifestResourceStream(qname);
			Icon resourceIcon = new Icon(iconStream);
			return resourceIcon;
		}
	}
}
