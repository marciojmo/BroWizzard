using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {


	/** The time to wait until load next scene. */
	private const float LOAD_NEXT_SCENE_DELAY = 3f;
	/** The current timestamp. */
	private float timestamp = 0f;


	// Use this for initialization
	void Start ()
	{
		// Just to be sure.
		this.timestamp = 0f;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Increments the timestamp.
		this.timestamp += Time.deltaTime;

		// The waiting is over?
		if ( this.timestamp > LOAD_NEXT_SCENE_DELAY )
		{
			// Loads the Count scene.
			ScenesController.loadScene( ScenesController.EScenes.evCount );
		}
	
	}
}
