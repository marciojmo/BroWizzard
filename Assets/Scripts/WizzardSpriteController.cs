using System;
using UnityEngine;
using UnityEngine.UI;

/** Controls the Wizzard's Sprite. */
public class WizzardSpriteController : MonoBehaviour
{
	/** The sprite of the wizzard when Idle. */
	public Sprite m_idle;
	/** The sprite of the wizzard when casting. */
	public Sprite m_casting;

	/** The image component of the wizzard. */
	private Image m_image;


	void Awake()
	{
		if ( m_idle == null )
		{
			Debug.LogError( "Idle Sprite not set on " + typeof( SelectionController ).Name );
			Debug.Break();
		}
		// Checks if casting sprite was set.
		if ( m_casting == null )
		{
			Debug.LogError( "Casting Sprite not set on " + typeof( SelectionController ).Name );
			Debug.Break();
		}

		m_image = GetComponent<Image>();
		// Checks if casting sprite was set.
		if ( m_image == null )
		{
			Debug.LogError( "Image Component not found on " + typeof( SelectionController ).Name );
			Debug.Break();
		}
	}

	/** Changes the Wizzard sprite to Idle. */
	public void wait()
	{
		m_image.sprite = m_idle;
	}


	/** Changes the Wizzard sprite to Casting. */
	public void cast()
	{
		m_image.sprite = m_casting;
	}

}



