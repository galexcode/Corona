using UnityEngine;
using System.Collections;

public class WeakLaser : Projectile {

	private LineRenderer line;
	
	public GameObject laserBlast;
	
	private Vector3 lineStart;
	private Vector3 lineEnd;

	// Use this for initialization
	void Start () {
		this.line = this.GetComponent<LineRenderer>();
		
		lineStart = transform.position;
		lineEnd = lineStart + transform.forward;
		this.line.SetPosition(0, lineStart);
		this.line.SetPosition(1, lineEnd);
	}
	
	// Update is called once per frame
	new void Update () {
		
		base.Update();
		
		lineStart = transform.position;
		lineEnd = lineStart + transform.forward;
		this.line.SetPosition(0, lineStart);
		this.line.SetPosition(1, lineEnd);
		
		if ((this.transform.position - this.startingPosition).sqrMagnitude > this.sqrRange) {
			Destroy(this.obj);	
		}
	}
	
	void OnTriggerEnter(Collider c) {
		
		// dont collide with creator
		if (c.gameObject != this.creator) {
			
			RaycastHit rh = new RaycastHit();
			if (Physics.Linecast(lineStart, lineEnd, out rh)) {
				
				// cause damage
				TargetableEntity e = rh.collider.gameObject.GetComponent<TargetableEntity>();
				if (e != null) {
					e.TakeHit(this.dmg);	
				}
				
				// create sparks
				GameObject blast = (GameObject)Instantiate(laserBlast, rh.point, Random.rotation);
				blast.transform.parent = c.gameObject.transform;
				Destroy(this.obj);
			}
		}	
	}
	
}
