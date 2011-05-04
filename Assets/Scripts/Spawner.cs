using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Spawner : MonoBehaviour {

	private SpacePartition sp;

	public GameObject fighter;

	// Use this for initialization
	void Start () {
		sp = new SpacePartition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SpawnFighter(int team) {
		GameObject obj; 
		
		if (team == 0) {
			obj = (GameObject)Instantiate(fighter, Vector3.zero, Quaternion.identity);
		} else {
			obj = (GameObject)Instantiate(fighter, Vector3.one, Quaternion.identity);	
		}
		
		// init new ship
		Fighter ai = obj.GetComponent<Fighter>();
		ai.team = team;
		ai.SetObj(obj);
		ai.SetSpace(sp);
		
		// update space partition
		sp.targets[team].Add(ai);
		
	}
}
