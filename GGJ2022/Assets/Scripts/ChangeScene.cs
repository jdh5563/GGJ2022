using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	/// <summary>
	/// Load the game scene
	/// </summary>
	public void LoadGame()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void EndGame()
	{
		GameManager.obstacles.Clear();
		SceneManager.LoadScene("EndScene");
	}

	/// <summary>
	/// Quit the game
	/// </summary>
	public void QuitGame()
	{
		Application.Quit();
	}
}
