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

	public static Vector3 FiringDirection(Vector3 pos1, Vector3 pos2, Vector3 vel2, float weaponSpeed) {
		return 	
	}

}
