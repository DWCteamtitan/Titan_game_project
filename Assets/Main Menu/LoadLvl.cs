using UnityEngine;
using System.Collections;

public class LoadLvl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load () {
		Application.LoadLevel(1); //1 was the level 1 design
		//Application.LoadLevel(3); //3 is the invidual scene for movie
		Time.timeScale = 1f;
		
	}
	public void Quit(){
		Application.Quit ();
	}

	public void Menu(){
		Application.LoadLevel(0);
	}

	public void Credits()
	{
		Application.LoadLevel (2);}

	public void Movie()
	{
		Application.LoadLevel(3); //3 is the invidual scene for movie
		Time.timeScale = 1f;
	}
}
