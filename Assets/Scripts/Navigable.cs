using UnityEngine;
using System.Collections;

public class Navigable : MonoBehaviour {

	private GameObject cam;
	private FreeCamera cam_e;
	
	private GameObject ship;
	private TargetableEntity ship_e;
	

	// Use this for initialization
	void Start () {
		this.cam = GameObject.Find("Camera");
		this.cam_e = cam.GetComponent<FreeCamera>();
		
		this.ship_e = null;
		this.ship = null;
		
	}
	
	// Update is called once per frame
	void Update() {
		
		// RTS Mode
		if (ship == null) {
			if (Input.GetMouseButton(1)) {
				Debug.Log("asd");
				this.ship = GameObject.Find("Fighter(Clone)");
				// got a ship to command
				if (this.ship != null) {
					this.ship_e = this.ship.GetComponent<TargetableEntity>();
					transform.parent = this.ship.transform;
					transform.position = Vector3.zero;
					//transform.forward = this.ship.transform.forward;
					this.cam_e.LockLook();
				}
			}
		}
		
		// FPS Mode
		else {
			if (Input.GetMouseButton(0)) {
				Debug.Log("fighter");
			}
		}
		
	}
	
	void Fire() {
		/*
		GameObject obj = (GameObject)Instantiate(this.laser, transform.position, transform.rotation);
		
		Projectile laser = obj.GetComponent<Projectile>();
		laser.SetObj(obj);
		laser.Init(this.getObj(), transform.position, transform.forward);
		*/
		
	}
		
}
