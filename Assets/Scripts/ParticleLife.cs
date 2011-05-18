using UnityEngine;
using System.Collections;

public class ParticleLife : MonoBehaviour {

	public float lifetime;
	private float time = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > lifetime) {
			Destroy(this);	
		}
	}
}
