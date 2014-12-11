using UnityEngine;
using System.Collections;

public class CountController : MonoBehaviour {

	/** The number of members on the sort. */
	private string m_nMembers = "";
	/** A reference to the oK button. */
	private bool m_okButton;
	/** A flag indicating to display or not the error message. */
	private bool m_displayError = false;



	private void fixInput()
	{
		string formatted = "";
		for ( int i = 0; i < m_nMembers.Length && i < 2; i++ )
		{
			char c = m_nMembers[i];

			if ( c >= '0' && c <= '9' )
				formatted += c;
		}
		m_nMembers = formatted;
	}



	void Update()
	{
		fixInput();

		// OK BUTTON PRESSED?
		if( m_okButton )
		{
			fixInput();

			if ( m_nMembers.Length > 0 && m_nMembers != "0" && m_nMembers != "00" )
			{
				Persistence.sm_nDevelopers = int.Parse( m_nMembers );
				Persistence.calculateHouseCounters();
				ScenesController.loadScene( ScenesController.EScenes.evSelect );
			}
			else
			{
				m_displayError = true;
			}
		}
	}



	void OnGUI() {
		GUI.Label( new Rect( 10, 10, 200, 20 ), "Quantos membros?" );
		m_nMembers = GUI.TextField( new Rect(10, 40, 50, 20), m_nMembers, 25 );
		m_okButton = GUI.Button( new Rect( 10, 70, 50, 20 ), "OK" );

		if ( m_displayError )
		{
			GUI.contentColor = Color.red;
			GUI.Label( new Rect( 70, 40, 200, 20 ), "Quantidade invalida!" );
		}
	}
}
