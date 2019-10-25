using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	
	public interface IBusinessObject
	{
		ArrayList Validate();

	}
}
