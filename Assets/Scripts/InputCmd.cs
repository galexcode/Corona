using UnityEngine;
using System.Collections;

public class InputCmd : MonoBehaviour {

	private GameObject cam;

	// Use this for initialization
	void Start () {
		this.cam = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			this.cam.GetComponent<FreeCamera>().ToggleLook();
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			this.cam.GetComponent<FreeCamera>().UnlockLook();
		}
	}
}
