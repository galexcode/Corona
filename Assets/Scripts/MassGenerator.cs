using UnityEngine;
using System.Collections;

public class MassGenerator : MonoBehaviour {

	public GameObject meteor1;
	
	public int bounds;
	
	public int nearBounds;

	// Use this for initialization
	void Start () {
		
		// parent obj
		GameObject masses = GameObject.Find("Masses");
		
		for (int i = 0; i < 100; ++i) {
			int scale = Random.Range(500, 4000); 
			Vector3 pos = new Vector3(Random.value * bounds - bounds/2, 
				Random.value * bounds - bounds/2, 
				Random.value * bounds - bounds/2);
			GameObject newObj = ((GameObject)Instantiate(meteor1, pos, Random.rotation));
			newObj.transform.localScale = Vector3.one * scale;
			newObj.transform.parent = masses.transform;
		}
		
		for (int i = 0; i < 100; ++i) {
			int scale = Random.Range(2, 100); 
			Vector3 pos = new Vector3(Random.value * nearBounds - nearBounds/2, 
				Random.value * nearBounds - nearBounds/2, 
				Random.value * nearBounds - nearBounds/2);
			GameObject newObj = ((GameObject)Instantiate(meteor1, pos, Random.rotation));
			newObj.transform.localScale = Vector3.one * scale;
			newObj.transform.parent = masses.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
