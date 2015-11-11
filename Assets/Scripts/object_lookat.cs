using UnityEngine;
using System.Collections;

public class object_lookat : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt (target);
	}
}
