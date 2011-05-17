using UnityEngine;
using System.Collections;

public class HitDetect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Physics.OverlapSphere(transform.position, radius);
	}
	
	void OnCollisionEnter() {
		Debug.Log("Collide");
	}
	
	void OnTriggerExit() {
		SphereCollider sc = this.GetComponent<SphereCollider>();
		sc.isTrigger = false;
	}
	
	void OnMyCollisionEnter() {
		Debug.Log("My collision");	
	}
	
}
