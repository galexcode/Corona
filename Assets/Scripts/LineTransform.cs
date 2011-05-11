using UnityEngine;
using System.Collections;

public class LineTransform : MonoBehaviour {

	private LineRenderer line;

	// Use this for initialization
	void Start () {
		this.line = this.GetComponent<LineRenderer>();
		
		Vector3 start = transform.position;
		Vector3 end = start + transform.forward;
		this.line.SetPosition(0, start);
		this.line.SetPosition(1, end);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 start = transform.position;
		Vector3 end = start + transform.forward;
		this.line.SetPosition(0, start);
		this.line.SetPosition(1, end);
	}
}
