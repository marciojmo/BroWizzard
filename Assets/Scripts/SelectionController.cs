using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent( typeof( AudioSource ) )]
public class SelectionController : MonoBehaviour
{

	#region ENUMS
	/** The states of this controller. */
	private enum EState 
	{
		evWaitingForInput,
		evSortingHouse,
		evWaitingForTextDisapears,
	}
	#endregion

	#region SETTINGS
	/** The time that users should hold the vortex to 
	 * call the select action. */
	public float m_selectionDelay = 3f;
	/** The delay until hide feedback text. */
	public float m_hideTextDelay = 4f;
	/** A reference to the result message. */
	public Text m_message;
	/** A reference to Wizzard image. */
	public WizzardSpriteController m_wizzard;
	/** The audio clips that will be used. For GAMBI reasons
	 * (lazy to create an editor) you should declare them MANUALLY. Sorry. */
	public AudioClip m_cinosAudioClip;
	public AudioClip m_hsarcAudioClip;
	public AudioClip m_oiramAudioClip;
	public AudioClip m_namagemAudioClip;
	public AudioClip m_narasumasAudioClip;
	public AudioClip m_sortingIsOverAudioClip;
	public AudioClip m_vortexAudioClip;
	public AudioClip m_bgAudioClip;
	#endregion




	#region PRIVATE MEMBERS
	/** Maps the house values to an audio clip. */
	private Dictionary<Persistence.EHouses, AudioClip> m_houseToClip;
	/** Flag indicating that the vortex is pressed. */
	private bool m_isVortexPressed = false;
	/** The time that the vortex was pressed. */
	private float m_vortexPressedTimestamp = 0f;
	/** The time when the feedback text was displayed. */
	private float m_textDisplayedTimestamp = 0f;
	/** The state of the controller. */
	private EState m_state;
	/** The AudioSource Component. */
	private AudioSource m_audio;
	#endregion




	#region PRIVATE METHODS
	/** Displays a message on screen.
	 * @param msg The message to display. */
	private void displayMsg( string msg )
	{
		m_message.text = msg;
	}

	/** Plays a sound.
	 * @param clip The clip to be played. */
	private void playSound( AudioClip clip )
	{
		m_audio.clip = clip;
		m_audio.Play();
	}


	/** Sorts the house that the user should go. */
	private void sort()
	{
		List<Persistence.EHouses> available = new List<Persistence.EHouses>();
		
		// Put available houses into an array.
		foreach ( KeyValuePair<Persistence.EHouses, int> house in Persistence.sm_housesCounter )
			if ( house.Value > 0 )
				available.Add( house.Key );

		string msg = "";
		AudioClip msgClip = null;

		if ( available.Count == 0 )
		{
			msg = "Sorting is over";
			msgClip = m_sortingIsOverAudioClip;
		}
		else
		{
		
			int rand = UnityEngine.Random.Range( 0, available.Count );
			// Sort the house of the user
			Persistence.EHouses selectedHouse = available[ rand ];
			// Decrements the counter of the sorted house
			Persistence.sm_housesCounter[ selectedHouse ]--;
			// Plays house sound and displays the sorted house
			msg = Persistence.sm_housesNames[ selectedHouse ];
			msgClip = m_houseToClip[ selectedHouse ];
		}

		displayMsg( msg );
		playSound( msgClip );

		// Sets the display timestamp and changes the state.
		m_textDisplayedTimestamp = Time.time;
		m_state = EState.evWaitingForTextDisapears;
		m_wizzard.wait();
		onVortexReleased();
	}
	#endregion


	/** Callback method to handle vortex pressed event. */
	public void onVortexPressed()
	{
		// Guarantees that the vortex pressed will only 
		// be processed when I was on the Waiting For Input State.
		if ( m_state == EState.evWaitingForInput )
		{
			m_wizzard.cast();
			playSound( m_vortexAudioClip );
			m_isVortexPressed = true;
			m_vortexPressedTimestamp = Time.time;
		}
	}


	/** Callback method to handle vortex released event. */
	public void onVortexReleased()
	{
		m_wizzard.wait();
		m_isVortexPressed = false;

		// Stops the "casting" sound.
		if ( m_state == EState.evWaitingForInput )
			m_audio.Stop();
	}


	public void loadPreviousScene()
	{
		ScenesController.loadScene( ScenesController.EScenes.evCount );
	}





	#region UNITY CALLBACKS
	void Awake()
	{
		m_audio = GetComponent<AudioSource>();
		// Checks if audio object was set.
		if ( m_audio == null )
		{
			Debug.LogError( "AudioSource not found on " + typeof( SelectionController ).Name );
			Debug.Break();
		}

		// Checks if message object was set.
		if ( m_message == null )
		{
			Debug.LogError( "Message not set on " + typeof( SelectionController ).Name );
			Debug.Break();
		}
		if ( m_wizzard == null )
		{
			Debug.LogError( "Wizzard Image not set on " + typeof( SelectionController ).Name );
			Debug.Break();
		}

		// Initializes the dict that maps EHouses to AudioClips
		m_houseToClip = new Dictionary<Persistence.EHouses, AudioClip>
		{
			{ Persistence.EHouses.evCinos, m_cinosAudioClip },
			{ Persistence.EHouses.evHsarc, m_hsarcAudioClip },
			{ Persistence.EHouses.evOiram, m_oiramAudioClip },
			{ Persistence.EHouses.evNamagem, m_namagemAudioClip },
			{ Persistence.EHouses.evNarasumas, m_narasumasAudioClip },
		};
	}

	
	void Start()
	{
		// Erases feedback message.
		displayMsg( "" );
		// Sets initial state.
		m_state = EState.evWaitingForInput;
	}


	void Update()
	{
		float elapsedTime;

		// Process state machine
		switch ( m_state )
		{
		case EState.evWaitingForInput:
			if ( m_isVortexPressed )
			{
				// Gets the elapsed timestamp since pressed event.
				elapsedTime = Time.time - m_vortexPressedTimestamp;
				if ( elapsedTime > m_selectionDelay )
				{
					m_state = EState.evSortingHouse;
					sort();
					return;
				}
			}
			break;
		
		case EState.evWaitingForTextDisapears:
			// Gets the elapsed timestamp since the text was displayed.
			elapsedTime = Time.time - m_textDisplayedTimestamp;
			if ( elapsedTime > m_hideTextDelay )
			{
				// Hides the text and changes state.
				displayMsg( "" );
				m_state = EState.evWaitingForInput;
			}
			break;
		}


	}
	#endregion




}
