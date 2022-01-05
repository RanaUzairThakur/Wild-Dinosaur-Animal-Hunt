using UnityEngine.SceneManagement;
using UnityEngine;

public class Options : MonoBehaviour {

	public void GoToTiltScene()
	{
		SceneManager.LoadScene ("tilt");
	}
	public void GoToButtonScene()
	{
		SceneManager.LoadScene ("button");
	}
	public void GoToMenuScene()
	{
		SceneManager.LoadScene ("Menu");
	}
		
}
