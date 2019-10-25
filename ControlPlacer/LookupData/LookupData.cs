using System;
using System.Collections;
using System.Windows.Forms;

namespace Afni.ControlPlacer.Lookups
{
	/// <summary>
	/// Holds information related to possible 
	/// answers to questions.  Applicable to
	/// question types such as multi-selects,
	/// radio buttons, and combo boxes.
	/// </summary>
	public class LookupData
	{
		private long _lookup_id;
		private string _text;
		private bool _selected;

		public LookupData()
		{
		}

		public LookupData(long LookupID, string Text)
		{
			_lookup_id = LookupID;
			_text = Text;
		}

		/// <summary>
		/// Gets or sets the ID of the Lookup item
		/// </summary>
		public long LookupID
		{
			get { return _lookup_id; }
			set { _lookup_id = value; }
		}
		
		/// <summary>
		/// Gets or sets the text value of the lookup data
		/// </summary>
		public string Text
		{
			get { return _text; }
			set { _text = value; }	
		}

		/// <summary>
		/// Gets or sets a value indicating if this option
		/// was chosen
		/// </summary>
		public bool Selected
		{
			get { return _selected; }
			set { _selected = value; }
		}

		/// <summary>
		/// Returns a string representation of the lookup data
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return _text; 
		}

	}

	/// <summary>
	/// Represents a type of lookup data specific
	/// to multi list lookup questions.  
	/// </summary>
	public class MultiListLookup : LookupData
	{
		CheckBox _cb = null;

		public MultiListLookup(long LookupID, string text)
		{
			LookupID = LookupID;
			this.Text = text;
			_cb = new CheckBox();
			_cb.Text = text;
		}

		/// <summary>
		/// Gets or sets the CheckBox object
		/// corresponding to the lookup.
		/// </summary>
		public CheckBox ListCheckBox
		{
			get { return _cb; }
			set { _cb = value; }
		}
	}

	/// <summary>
	/// Represents a type of lookup data specific
	/// to Radio Questions.
	/// </summary>
	public class RadioLookup : LookupData
	{
		RadioButton _radio = null;

		public RadioLookup(long LookupID, string text)
		{
			this.LookupID = LookupID;
			this.Text = text;
			_radio = new RadioButton();
			_radio.Text = text;
		}

		/// <summary>
		/// Gets or sets the Radio object
		/// corresponding to the lookup
		/// </summary>
		public RadioButton Radio
		{
			get { return _radio; }
			set { _radio = value; }
		}
	}
}
