using UnityEngine;
using System.Collections;

public class Projectile : SpaceEntity {

	public float ttl;
	public int dmg;
	
	private float length = 0.5f;
	
	private LineRenderer line;
	private Vector3 lineStart;
	
	// dir should already be normalized
	public void Init(Vector3 pos, Vector3 dir, int dmg, float ttl) {
		this.lineStart = pos;
		this.ttl = ttl;
		this.dmg = dmg;
		this.speed = 10;
		this.direction = dir * this.speed;
		
		line = this.GetComponent<LineRenderer>();
		
		line.SetWidth(0.05f, 0.05f);
		
		Vector3 end = lineStart + dir * this.length;
		this.line.SetPosition(0, lineStart);
		this.line.SetPosition(1, end);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newStart = this.lineStart + this.direction * Time.deltaTime;
		Vector3 end = newStart + this.direction.normalized * this.length;
		
		this.line.SetPosition(0, newStart);
		this.line.SetPosition(1, end);
		this.lineStart = newStart;
		
		ttl -= Time.deltaTime;
		if (ttl < 0) {
			Destroy(this.obj);	
		}
	}
}
