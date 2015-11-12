using UnityEngine;
using System.Collections;

/* Small Script that Makes UI Elements to ALWAYS face the main camera */

public class UI_Billboard_Script : MonoBehaviour 
{
	public Camera my_camera; //The main camera is dragged into this billboard script drop down

	// Use this for initialization
	void Start () 
	{
		transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.back,
		                 my_camera.transform.rotation * Vector3.up);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
