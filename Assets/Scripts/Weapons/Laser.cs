using UnityEngine;
using System.Collections;

public class Laser : SpaceEntity {

	public float ttl;
	public float dmg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.obj.transform.Translate(this.velocity * Time.deltaTime);
		
		ttl -= Time.deltaTime;
		if (ttl < 0) {
			Destroy(this);	
		}
	}
}
