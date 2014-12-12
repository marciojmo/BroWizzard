using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class CountController : MonoBehaviour {

	/** The default error message to display when input have invalid values. */
	private const string DEFAULT_ERROR_MESSAGE = "Quantidade inválida";
	/** The input data field. */
	public InputField m_input;
	/** The response text (If input was invalid). */
	public Text m_txtResponse;



	public void onOKPressed()
	{
		string nMembersTxt = m_input.text;
		m_txtResponse.text = "";

		// Checks if input is valid.
		Match match = Regex.Match( nMembersTxt, @"\d{1,2}" );
		if ( match.Success )
		{
			int nMembers = int.Parse( nMembersTxt );
			if ( nMembers <= 0 )
			{
				m_txtResponse.text = DEFAULT_ERROR_MESSAGE;
				return;
			}
			// Calculates the counters to the houses
			Persistence.sm_nDevelopers = nMembers;
			Persistence.calculateHouseCounters();
			// Loads next scene.
			ScenesController.loadScene( ScenesController.EScenes.evSelect );
		}
		else
			m_txtResponse.text = DEFAULT_ERROR_MESSAGE;
	}





	void Awake()
	{
		if ( m_txtResponse == null )
		{
			Debug.LogError( string.Format( "Text not found on {0}", typeof( CountController ).Name ) );
			Debug.Break();
		}
		if ( m_input == null )
		{
			Debug.LogError( string.Format( "InputField not found on {0}", typeof( CountController ).Name ) );
			Debug.Break();
		}
	}
	
}
