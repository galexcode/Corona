using UnityEngine;
using System.Collections;

public class Fighter : TargetableEntity {

	public float acceleration;
	public float maxSpeed;
	public float maxAngle; // radians
	public float weaponCooldown; // seconds
	public float weaponTimerRNG; // seconds
	
	public float sqrFlyoutRange;
	public float sqrFlyinRange;
	private Vector3 flyoutTarget;
	
	private bool flyingOut;
	
	public TargetableEntity target;
	
	public GameObject laser;
	public GameObject explosion;
	public int angleOfAttack;
	public int dmg;
	
	private float weaponTimer;
	
	
	// direction should be normalized
	public void Init(int team, Vector3 direction, int armor) {
		this.acceleration = 0.1f;
		this.maxSpeed = 2.0f;
		this.maxAngle = 2; // radians
		this.weaponCooldown = 1.0f; // seconds
		this.weaponTimerRNG = 1.0f; // seconds
		this.direction = direction;
		this.speed = this.maxSpeed;
		
		this.sqrFlyoutRange = 10.0f;
		this.sqrFlyinRange  = 100.0f;
		
		this.flyingOut = false;
		this.flyoutTarget = Vector3.up;
		
		this.target = null;
		
		this.weaponTimer = 0.0f;
		this.angleOfAttack = 10; // degrees
		this.dmg = 5;
		
		this.shields = 0;
		this.armor = armor;
		this.team = team;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.target && !this.target.Alive()) {
			this.target.Untarget();
			this.target = null;
		}
		
		if (this.alive) {
			
			// get a new target
			if (!target && this.CanRetarget() && sp != null) {
				ArrayList enemyTeam = sp.map["targets"][(team + 1) % 2];
				if (enemyTeam.Count != 0) {
					target = (TargetableEntity)enemyTeam[0];
					target.Target();
				}
			}
			
			// chase and fire
			if (target) {
				this.ChaseAndFire();
			} else {
				// stop for now
				this.speed = Mathf.Max(this.speed - this.acceleration * Time.deltaTime, 0);	
			}
			
			transform.Translate(this.direction * this.speed * Time.deltaTime);
				
		} else if (this.CanDestroy()) {
			sp.map["targets"][team].Remove(this);
			if (this.target)
				this.target.Untarget();
			Instantiate(this.explosion, transform.position, transform.rotation);
			Destroy(this.obj);
		}
	}
	
	private void ChaseAndFire() {
		Vector3 dir = target.getObj().transform.position - transform.position;
		float sqrdist = dir.sqrMagnitude;
		
		if (Vector3.Angle(this.direction, dir) < this.angleOfAttack) {
			this.Fire();	
		}
		
		if (sqrdist < this.sqrFlyoutRange && !this.flyingOut) {
			this.flyingOut = true;
			this.flyoutTarget = this.Orthogonal(this.direction);
		} else if (sqrdist > this.sqrFlyinRange) {
			this.flyingOut = false;
		}
		if (this.flyingOut) {
			
			this.direction = Vector3.RotateTowards(this.direction, this.flyoutTarget, this.maxAngle * Time.deltaTime, 1);
			this.direction.Normalize();
			
			this.speed = Mathf.Min(this.speed + this.acceleration * Time.deltaTime, this.maxSpeed);	
		} else {
			this.direction = Vector3.RotateTowards(this.direction, dir, this.maxAngle * Time.deltaTime, 1);
			this.direction.Normalize();
			
			this.speed = Mathf.Min(this.speed + this.acceleration * Time.deltaTime, this.maxSpeed);	
		}
	}
	
	// returns an arbitrary normal to the given vector
	private Vector3 Orthogonal(Vector3 v) {
		v.Normalize();
		if (v.x == 1) {
			return Vector3.up;
		}
		return new Vector3(v.z, v.y * v.z / (v.x - 1), 1 + Mathf.Pow(v.z, 2) / (v.x - 1));
	}
	
	private void Fire() {
		// check to see if weapons are ready first
		this.weaponTimer += Time.deltaTime;
		
		if (this.weaponTimer > this.weaponCooldown) {
			GameObject obj = (GameObject)Instantiate(this.laser, transform.position, transform.rotation);
			
			Laser laser = obj.GetComponent<Laser>();
			laser.SetObj(obj);
			laser.Init(transform.position, this.direction.normalized, 1, this.dmg);
			
			// lame assumption that all weapons hit
			this.target.TakeHit(this.dmg);
			if (!this.target.Alive()) {
				this.target.Untarget();
				this.target = null;
			}
			
			weaponTimer = Random.value * this.weaponTimerRNG;
		}
	}
	
	private bool CanRetarget() {
		return (int)(Random.value * (10000 * Time.deltaTime)) == 0;
	}

}
