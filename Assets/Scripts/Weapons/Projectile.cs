using UnityEngine;
using System.Collections;

public class Projectile : SpaceEntity {

	public delegate void OnDestroyDelegate(bool hit, Vector3 position);

	private Vector3 startingPosition;
	public int sqrRange;
	public int dmg;
	
	private OnDestroyDelegate onDestroy;
	
	// dir should already be normalized
	public void Init(Vector3 pos, Vector3 dir, int dmg, int sqrRange) {
		this.startingPosition = pos;
		this.sqrRange = sqrRange;
		this.dmg = dmg;
		this.speed = 15;
		this.direction = dir;
		
		this.onDestroy = null;
		
		this.transform.forward = dir;
		this.transform.position = pos;
	}

	public void HandleOnDestroy(OnDestroyDelegate onDestroy) {
		this.onDestroy = onDestroy;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
		
		if ((this.transform.position - this.startingPosition).sqrMagnitude > this.sqrRange) {
			if (this.onDestroy != null) {
				bool hit = true;
				this.onDestroy(hit, this.transform.position);
			}
			
			Destroy(this.obj);	
		}
	}
}
