/******************************************************************************
 * 
 * Afni.FormData
 * 
 * The IForm interfaces are a generic interfaces to be implemented by windows forms,
 * usercontrols, and other GUI elements that contain a group or groups of data 
 * coming from an external source, such as a database.  By having groups of forms
 * implement the same interface(s), the forms can present common functions to 
 * external classes and object without having to reveal the internals of the
 * form or control.  
 * 
 * Depending on the type of data being presented to the user, there are only
 * a handful of common functions that need to be externally exposed through 
 * interfaces.  These interfaces support:
 * 
 *		-Showing (refreshing) data
 *		-Saving data
 *		-Editing data
 *		-Deleting data
 *		-Adding new data
 *		-Getting help on the form or usercontrol
 *		-Retrivieng the "state" of the form
 * 
 * ***************************************************************************/
using System;

namespace Afni.FormData
{
	
	public interface IForm
	{
		bool Refresh();
		bool ShowHelp();
		string ToString();
		FormStates FormState { get; }
		string Name {get; set; }
	}

	public interface ISaveable : IForm
	{
		bool Save();
		bool Undo();
		string SaveButtonText{get; set;}
		event EventHandler Dirtied;
		event EventHandler SaveSucceeded;
		event EventHandler SaveFailed;
		event EventHandler Undone;
	}

	public interface IEditable : IForm
	{
		bool Edit();
		string EditButtonText{get; set;}
	}

	public interface IAddable : IForm
	{
		bool AddNew();
		string AddButtonText{get; set;}
	}

	public interface IDeletable : IForm
	{
		bool Delete();
		string DeleteButtonText{get; set;}
		event EventHandler DeleteSucceeded;
		event EventHandler DeleteFailed;
	}

	public enum FormStates
	{
		Idle,
		Busy,
		EditInProgress,
		EditPendingChange,
		AddInProgress,
		AddPendingChange
	}

	public enum FormTypes
	{
		MdiParent,
		SingleItemEditable,
		SingleItemViewOnly,
		MultiItemEditable,
		MultiItemViewOnly,
		WizardStep,
		Informational
	}
}
