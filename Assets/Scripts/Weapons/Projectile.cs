using UnityEngine;
using System.Collections;

public class Projectile : SpaceEntity {

	public delegate void OnDestroyDelegate(Vector3 position);
	public delegate void OnCollideDelegate(Vector3[] points, GameObject o, Projectile p);

	private Vector3 startingPosition;
	public int sqrRange;
	public int dmg;
	
	protected OnDestroyDelegate onDestroy;
	protected OnCollideDelegate onCollide;
	
	// dir should already be normalized
	public void Init(Vector3 pos, Vector3 dir, int dmg, int sqrRange) {
		this.startingPosition = pos;
		this.sqrRange = sqrRange;
		this.dmg = dmg;
		this.speed = 15;
		this.direction = dir;
		
		this.onDestroy = null;
		this.onCollide = null;
		
		this.transform.forward = dir;
		this.transform.position = pos;
	}

	public void HandleOnDestroy(OnDestroyDelegate onDestroy) {
		this.onDestroy = onDestroy;
	}
	
	public void HandleOnCollide(OnCollideDelegate onCollide) {
		this.onCollide = onCollide;	
	}
	
	// Update is called once per frame
	protected void Update () {
		
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
		
		if ((this.transform.position - this.startingPosition).sqrMagnitude > this.sqrRange) {
			if (this.onDestroy != null) {
				this.onDestroy(this.transform.position);
			}
			Destroy(this.obj);	
		}
	}
	
}
