using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScenesController
{
	/** Maps each EScenes values to its corresponding scene name. */
	private static Dictionary<EScenes, string> scenesNames = new Dictionary<EScenes, string>
	{
		{ EScenes.evSplash, "Splash" },
		{ EScenes.evCount, "Count" },
		{ EScenes.evSelect, "Select" },

	};

	/** Enum that stores the scenes of the app. */
	public enum EScenes
	{
		// Splash screen.
		evSplash,
		// Gets the number of participants
		evCount,
		// Selects each participant.
		evSelect
	}


	/** Loads a scene. 
	 * @param scene The scene to be loaded. */
	public static void loadScene( EScenes scene )
	{
		// Loads the next scene. Yeah, there's no fade.
		Application.LoadLevel( ScenesController.scenesNames[ scene ]  );
	}
}
