using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehaviour {

	public override System.Action GetClickAction ()
	{
		return delegate() {
			Debug.Log("Create Command Base Attempt");
		};
	}
}
