using UnityEngine;
using System.Collections;

public class Projectile : SpaceEntity {

	//public delegate void OnDestroyDelegate(Vector3 position);
	//public delegate void OnCollideDelegate(Vector3[] points, GameObject o, Projectile p);

	protected Vector3 startingPosition;
	
	//protected OnDestroyDelegate onDestroy;
	//protected OnCollideDelegate onCollide;
	
	public int sqrRange;
	public int dmg;
	
	protected GameObject creator;
	
	// dir should already be normalized
	public void Init(GameObject creator, Vector3 pos, Vector3 dir) {
		this.startingPosition = pos;
		this.direction = dir;
		
		this.creator = creator;
		
		//this.onDestroy = null;
		//this.onCollide = null;
		
		this.transform.forward = dir;
		this.transform.position = pos;
	}

	/*
	public void HandleOnDestroy(OnDestroyDelegate onDestroy) {
		this.onDestroy = onDestroy;
	}
	
	public void HandleOnCollide(OnCollideDelegate onCollide) {
		this.onCollide = onCollide;	
	}
	*/
	
	// Update is called once per frame
	protected void Update () {
		
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
		
	}
	
}
