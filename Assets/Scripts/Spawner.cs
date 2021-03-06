using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Spawner : MonoBehaviour {

	public SpacePartition sp;

	public GameObject fighter;
	public GameObject flakPlatform;
	
	//private float elapsed = 0;
	//private bool exploded = false

	// Use this for initialization
	void Start () {
		
		sp = new SpacePartition();
		
		// throw down a single platform for testing
		
		GameObject obj = (GameObject)Instantiate(flakPlatform);
		FlakPlatform fp = obj.GetComponent<FlakPlatform>();
		fp.Init(0);
		fp.SetObj(obj);
		fp.SetSpace(sp);
		fp.AttachToTargettingSystem();
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		elapsed += Time.deltaTime;
		if ((int)elapsed % 2 == 0 && !exploded) {
			Instantiate(explosion, Vector3.zero, Quaternion.identity);
			exploded = true;
		} else if ((int)elapsed % 2 != 0) {
			exploded = false;	
		}
		*/
	}
	
	public void SpawnFighter(int team) {
		GameObject obj; 
		
		Vector3 pos = new Vector3(Random.value * 20 - 10, Random.value * 20 - 10, Random.value * 20 - 10);
		
		if (team == 0) {
			obj = (GameObject)Instantiate(fighter, pos, Quaternion.identity);
		} else {
			obj = (GameObject)Instantiate(fighter, pos, Quaternion.identity);	
		}
		
		// init new ship
		Fighter ai = obj.GetComponent<Fighter>();
		float x = Random.value * 2 - 1;
		float y = Random.value * 2 - 1;
		float z = Random.value * 2 - 1;
		Vector3 dir = new Vector3(x, y, z);
		
		ai.Init(team, dir.normalized, 50);
		ai.SetObj(obj);
		ai.SetSpace(sp);
		ai.AttachToTargettingSystem();
		
	}
}
