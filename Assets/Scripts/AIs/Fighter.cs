using UnityEngine;
using System.Collections;

public class Fighter : TargetableEntity {

	// specs
	public float acceleration = 0.1f;
	public float maxSpeed = 0.5f;
	public float maxAngle = 0.01f; // radians
	public float weaponCooldown = 1.0f; // seconds
	
	public TargetableEntity target = null;
	
	public GameObject laser;
	
	
	private float weaponTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// get a new target
		if (!target && this.CanRetarget() && sp != null) {
			ArrayList enemyTeam = sp.map["targets"][(team + 1) % 2];
			if (enemyTeam.Count != 0)
				target = (TargetableEntity)enemyTeam[0];
		}
		
		// chase and fire
		if (target) {
			this.UpdateVelocity();
			
			this.Fire();
			
			transform.Translate(this.velocity * Time.deltaTime);	
		}
	}
	
	private void UpdateVelocity() {
		Vector3 dir = target.getObj().transform.position - transform.position;

		this.velocity = Vector3.RotateTowards(this.velocity, dir, this.maxAngle, 1);
		
		this.velocity.Normalize();
		this.velocity *= maxSpeed;
	}
	
	private void Fire() {
		// check to see if weapons are ready first
		this.weaponTimer += Time.deltaTime;
		
		if (this.weaponTimer > this.weaponCooldown) {
			GameObject obj = (GameObject)Instantiate(this.laser, transform.position, transform.rotation);
			
			Laser laser = obj.GetComponent<Laser>();
			laser.ttl = 5;
			laser.dmg = 1;
			laser.velocity = this.velocity;
			laser.velocity.Normalize();
			laser.SetObj(obj);
		}
		
	}
	
	private bool CanRetarget() {
		return (int)(Random.value * (10000 * Time.deltaTime)) == 0;
	}

}
