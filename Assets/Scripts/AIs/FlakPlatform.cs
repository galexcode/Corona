using UnityEngine;
using System.Collections;

public class FlakPlatform : TargetableEntity {

	private TargetableEntity targetTop;
	private TargetableEntity targetBottom;
	//private TargetableEntity targetBot;
	
	public float maxAngle; // radians
	public float weaponCooldown; // seconds
	public float weaponTimerRNG; // seconds
	
	public GameObject flakProjectile;
	public GameObject explosion;
	
	public int angleOfAttack;
	
	private float weaponTimerTop;
	private float weaponTimerBottom;
	
	private GameObject topTurret = null;
	private GameObject bottomTurret = null;
	
	public float angle;

	public void Init(int team) {
		this.armor = 100;	
		this.team = team;
		
		this.angleOfAttack = 10; // degrees
		
		this.targetTop = null;
		this.targetBottom = null;
		this.weaponTimerTop = 0;
		this.weaponTimerBottom = 0;
		this.weaponCooldown = 1.0f; // seconds
		this.weaponTimerRNG = 1.0f; // seconds
		//this.targetBot = null;
	}

	// Use this for initialization
	void Start () {
		this.topTurret = GameObject.Find("FlakGunTop");
		this.bottomTurret = GameObject.Find("FlakGunBottom");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.targetTop && !this.targetTop.Alive()) {
			this.targetTop.Untarget();
			this.targetTop = null;
		}
		if (this.targetBottom && !this.targetBottom.Alive()) {
			this.targetBottom.Untarget();
			this.targetBottom = null;
		}
		
		if (this.alive) {
			
			// get a new target
			if (!targetTop && this.CanRetarget() && sp != null) {
				SpaceEntity se = sp.NearestEnemy(this);
				if (se != null) {	
					targetTop = (TargetableEntity)se;
					targetTop.Target();
				}
			}
			if (!this.targetBottom && this.CanRetarget() && sp != null) {
				SpaceEntity se = sp.NearestEnemy(this);
				if (se != null) {	
					this.targetBottom = (TargetableEntity)se;
					this.targetBottom.Target();
				}
			}
			
			// fire
			if (this.targetTop) {
				
				Vector3 dir = this.targetTop.transform.position - transform.position;
				//Vector3 dir = this.targetTop.getObj().transform.position - transform.position;
				if (Mathf.Acos(Vector3.Dot(dir.normalized, Vector3.up)) <= Mathf.PI / 2) {
					this.topTurret.transform.up = dir.normalized;
				//float sqrdist = dir.sqrMagnitude;
				
				//if (Vector3.Angle(this.direction, dir) < this.angleOfAttack) {
					this.FireTop();	
				//}
				}
			}
			if (this.targetBottom) {
				
				Vector3 dir = this.targetBottom.transform.position - transform.position;
				//Vector3 dir = this.targetTop.getObj().transform.position - transform.position;
				if (Mathf.Acos(Vector3.Dot(dir.normalized, Vector3.up)) <= -Mathf.PI / 2) {
					this.bottomTurret.transform.up = dir.normalized;
				//float sqrdist = dir.sqrMagnitude;
				
				//if (Vector3.Angle(this.direction, dir) < this.angleOfAttack) {
					this.FireBottom();	
				//}
				}
			}
			
			//transform.forward = this.direction;
			
			//transform.position += this.direction * this.speed * Time.deltaTime;
			//this.transform.rotation.SetLookRotation(this.direction, this.transform.up);
				
		} else if (this.CanDestroy()) {
			sp.map["targets"][team].Remove(this);
			if (this.targetTop)
				this.targetTop.Untarget();
			Instantiate(this.explosion, transform.position, transform.rotation);
			Destroy(this.obj);
		}
	}
	
	/*
	private void OnProjectileDestroy(Vector3 position) {
		// create smoke cloud
		Instantiate(this.flakExplosion, position, Quaternion.identity);
	}
	
	private void OnProjectileCollide(Vector3[] points, GameObject o, Projectile p) {
		if (o != this.getObj()) {
			Instantiate(this.flakExplosion, points[0], Quaternion.identity);
			Destroy(p.getObj());
		}
	}
	*/
	
	private void FireTop() {
		// check to see if weapons are ready first
		this.weaponTimerTop += Time.deltaTime;
		
		if (this.weaponTimerTop > this.weaponCooldown) {
			GameObject obj = (GameObject)Instantiate(this.flakProjectile, transform.position, Quaternion.identity);
			
			//Projectile.OnDestroy destroy = this.OnDestroy;
			
			Projectile flak = obj.GetComponent<Projectile>();
			flak.SetObj(obj);
			Vector3 dir = SpaceEntity.FiringDirection(this.transform.position, this.targetTop.transform.position, this.targetTop.direction * this.targetTop.speed, flak.speed);
			flak.Init(this.getObj(), transform.position, dir);//this.topTurret.transform.up.normalized);
			//flak.HandleOnDestroy(this.OnProjectileDestroy);
			//flak.HandleOnCollide(this.OnProjectileCollide);
			
			weaponTimerTop = Random.value * this.weaponTimerRNG;
		}
	}
	
	private void FireBottom() {
		// check to see if weapons are ready first
		this.weaponTimerBottom += Time.deltaTime;
		
		if (this.weaponTimerBottom > this.weaponCooldown) {
			GameObject obj = (GameObject)Instantiate(this.flakProjectile, transform.position, Quaternion.identity);
			
			//Projectile.OnDestroy destroy = this.OnDestroy;
			
			Projectile flak = obj.GetComponent<Projectile>();
			flak.SetObj(obj);
			Vector3 dir = SpaceEntity.FiringDirection(this.transform.position, this.targetBottom.transform.position, this.targetBottom.direction * this.targetBottom.speed, flak.speed);
			flak.Init(this.getObj(), transform.position, dir);//this.topTurret.transform.up.normalized);
			//flak.HandleOnDestroy(this.OnProjectileDestroy);
			//flak.HandleOnCollide(this.OnProjectileCollide);
			
			weaponTimerTop = Random.value * this.weaponTimerRNG;
		}
	}
	
	private bool CanRetarget() {
		return true;
		//return (int)(Random.value * (10000 * Time.deltaTime)) == 0;
	}
	
	private void OnCollisionEnter(Collision c) {
		Debug.Log(c.contacts[0].point);
	}
	
}
