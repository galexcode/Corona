using UnityEngine;
using System.Collections;

public class SpaceEntity : MonoBehaviour {

	protected GameObject obj;
	
	public Vector3 direction;
	public float speed;
	
	protected SpacePartition sp;
	
	public int team;

	public void SetSpace(SpacePartition sp) {
		this.sp = sp;
	}
	
	public GameObject getObj() {
		return this.obj;	
	}
	
	public void SetObj(GameObject obj) {
		this.obj = obj;	
	}

}
