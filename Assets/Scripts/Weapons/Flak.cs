using UnityEngine;
using System.Collections;

public class Flak : Projectile {
	
	public GameObject flakCloud;
	
	public float explodeRadius;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	new void Update () {
		
		base.Update();
		
		if ((this.transform.position - this.startingPosition).sqrMagnitude > this.sqrRange) {
			this.Explode(transform.position);
		}
	}
	
	void OnTriggerEnter(Collider c) {
		// dont collide with creator
		if (c.gameObject != this.creator) {
			this.Explode(transform.position);
		}	
	}
	
	void Explode(Vector3 pos) {
		
		// explode 
		Instantiate(flakCloud, pos, Random.rotation);
		Destroy(this.obj);
		
		RaycastHit[] rha = Physics.SphereCastAll(pos, explodeRadius, Vector3.one, 1);
		foreach (RaycastHit rh in rha) {
			// cause damage
			TargetableEntity e = rh.collider.gameObject.GetComponent<TargetableEntity>();
			if (e != null) {
				e.TakeHit(this.dmg);	
			}
		}
	}
	
}
