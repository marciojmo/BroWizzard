using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Persistence : MonoBehaviour
{
	/** Keep a reference to this object. This object should be unique. */
	public static Persistence instance;

	/** Stores the Number of players to be sorted. */
	public static int sm_nDevelopers;

	/** Houses. */
	public enum EHouses
	{
		evCinos,
		evHsarc,
		evOiram,
		evNamagem,
		evNarasumas,
	}


	/** The houses names */
	public static Dictionary<EHouses, string> sm_housesNames = new Dictionary<EHouses, string>
	{
		{ EHouses.evCinos, "Cinos" },
		{ EHouses.evHsarc, "HSarc" },
		{ EHouses.evOiram, "Oiram" },
		{ EHouses.evNamagem, "Namagem" },
		{ EHouses.evNarasumas, "Narasumas" },
	};

	/** Informs how many developers can be sorted to the house. */
	public static Dictionary<EHouses, int> sm_housesCounter = new Dictionary<EHouses, int>
	{
		{ EHouses.evCinos, 0 },
		{ EHouses.evHsarc, 0 },
		{ EHouses.evOiram, 0 },
		{ EHouses.evNamagem, 0 },
		{ EHouses.evNarasumas, 0 },
	};



	public static void calculateHouseCounters()
	{
		int nHouses = Enum.GetValues( typeof( EHouses ) ).Length;
		int nSlots = Persistence.sm_nDevelopers / nHouses;
		int nRemaining = Persistence.sm_nDevelopers % nHouses ;

		List<EHouses> houses = new List<EHouses>();
		foreach ( EHouses h in Enum.GetValues( typeof( EHouses ) ) )
		{
			Persistence.sm_housesCounter[ h ] = nSlots;
			// Stores the current house to use on the next step.
			houses.Add( h );
		}


		while( nRemaining > 0 && houses.Count > 0 )
		{
			// Selects a random house to increment the number of 
			// available developers
			int rand = UnityEngine.Random.Range( 0, houses.Count );
			Persistence.sm_housesCounter[ houses[ rand ] ]++;

			// Removes the house from sorting
			houses.RemoveAt( rand );

			// Decrements the number of remaining developers.
			nRemaining--;
		}

	}

	void Awake ()
	{
		// If no instance exists
		if ( instance == null )
		{
			DontDestroyOnLoad( gameObject );
			instance = this;
		}
		// If another instance already exists, I should destroy myself.
		else if ( instance != this )
			Destroy( gameObject );
	}

	/*
	void OnGUI()
	{
		int top = 10;
		foreach ( EHouses h in Enum.GetValues( typeof( EHouses ) ) )
		{
			GUI.Label( new Rect( 10, top, 100, 20 ), Persistence.sm_housesNames[ h ] + ": " );
			GUI.Label( new Rect( 110, top, 100, 20 ), Persistence.sm_housesCounter[ h ].ToString() );

			top += 25;
		}
	}
	*/
}
