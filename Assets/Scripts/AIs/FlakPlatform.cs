using UnityEngine;
using System.Collections;

public class FlakPlatform : TargetableEntity {

	private TargetableEntity targetTop;
	//private TargetableEntity targetBot;
	
	public float maxAngle; // radians
	public float weaponCooldown; // seconds
	public float weaponTimerRNG; // seconds
	
	public GameObject flakProjectile;
	public GameObject flakExplosion;
	public GameObject explosion;
	
	public int angleOfAttack;
	public int dmg;
	
	private float weaponTimer;
	
	private GameObject topTurret = null;

	public void Init(int team) {
		this.armor = 100;	
		this.team = team;
		
		this.weaponTimer = 0.0f;
		this.angleOfAttack = 10; // degrees
		this.dmg = 1;
		
		this.targetTop = null;
		this.weaponTimer = 0;
		this.weaponCooldown = 1.0f; // seconds
		this.weaponTimerRNG = 1.0f; // seconds
		//this.targetBot = null;
	}

	// Use this for initialization
	void Start () {
		this.topTurret = GameObject.Find("FlakGunTop");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.targetTop && !this.targetTop.Alive()) {
			this.targetTop.Untarget();
			this.targetTop = null;
		}
		
		if (this.alive) {
			
			// get a new target
			if (!targetTop && this.CanRetarget() && sp != null) {
				ArrayList enemyTeam = sp.map["targets"][(team + 1) % 2];
				if (enemyTeam.Count != 0) {
					targetTop = (TargetableEntity)enemyTeam[0];
					targetTop.Target();
				}
			}
			
			// fire
			if (this.targetTop) {
				Vector3 dir = this.targetTop.transform.position - transform.position;
				//Vector3 dir = this.targetTop.getObj().transform.position - transform.position;
				this.topTurret.transform.up = dir.normalized;
				//float sqrdist = dir.sqrMagnitude;
				
				//if (Vector3.Angle(this.direction, dir) < this.angleOfAttack) {
					this.Fire();	
				//}
			}
			
			//transform.forward = this.direction;
			
			//transform.position += this.direction * this.speed * Time.deltaTime;
			//this.transform.rotation.SetLookRotation(this.direction, this.transform.up);
				
		}
	}
	
	private void Fire() {
		// check to see if weapons are ready first
		this.weaponTimer += Time.deltaTime;
		
		if (this.weaponTimer > this.weaponCooldown) {
			GameObject obj = (GameObject)Instantiate(this.flakProjectile, transform.position, Quaternion.identity);
			
			
			Projectile flak = obj.GetComponent<Projectile>();
			flak.SetObj(obj);
			flak.Init(transform.position, this.topTurret.transform.up.normalized, this.dmg, 3.0f);
			
			
			// lame assumption that all weapons hit
			this.targetTop.TakeHit(this.dmg);
			if (!this.targetTop.Alive()) {
				this.targetTop.Untarget();
				this.targetTop = null;
			}
			
			weaponTimer = Random.value * this.weaponTimerRNG;
		}
	}
	
	private bool CanRetarget() {
		return true;
		//return (int)(Random.value * (10000 * Time.deltaTime)) == 0;
	}
	
}
