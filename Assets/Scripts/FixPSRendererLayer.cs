using UnityEngine;
using System.Collections;

public class FixPSRendererLayer : MonoBehaviour {

	/** The sorting layer that the object should be. */
	public string m_sortingLayer = "Default";
	// Use this for initialization
	void Start ()
	{
		try {
			// Tries to change the sorting layer
			particleSystem.renderer.sortingLayerName = m_sortingLayer;
		} catch( UnityException ex )
		{
			Debug.LogError( string.Format( "Sorting Layer {0} does not exist.", m_sortingLayer ) );
			Debug.LogError( ex.Message );
			Debug.Break();
		}

	}
}
