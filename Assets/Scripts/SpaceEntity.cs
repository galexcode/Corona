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

	public static Vector3 FiringDirection(Vector3 sp, Vector3 tp, Vector3 tv, float bs) {
		Vector3 D = tp - sp;
		float E = Vector3.Dot( D, D );
		float F = 2 * ( Vector3.Dot( tv, D ) );
		float G = ( bs * bs ) - ( Vector3.Dot( tv, tv ) );
		float t = ( F + Mathf.Sqrt( ( F * F ) + 4 * G * E) ) / ( G * 2 );	
		return (D / t + tv).normalized;
	}

}
