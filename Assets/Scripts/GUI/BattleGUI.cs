using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Random.seed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10, 10, 100, 90), "Spawn Menu");
		
		if (GUI.Button(new Rect(20, 40, 80, 20), "Fighter 1")) {
			Spawner sp = GetComponent<Spawner>();
			sp.SpawnFighter(0);
		}
		
		if (GUI.Button(new Rect(20, 70, 80, 20), "Fighter 2")) {
			Spawner sp = GetComponent<Spawner>();
			sp.SpawnFighter(1);
		}
	}
}
