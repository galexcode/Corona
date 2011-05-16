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
	}
	
	void OnTriggerEnter(Collider c) {
		RaycastHit rh = new RaycastHit();
		if (Physics.Linecast(lineStart, lineEnd, out rh)) {
			if (this.onCollide != null) {
				Vector3[] points = {rh.point};
				GameObject blast = (GameObject)Instantiate(laserBlast, rh.point, Random.rotation);
				blast.transform.parent = c.gameObject.transform;
				this.onCollide(points, c.gameObject, (Projectile)this);
			}
		}	
	}
	
}
