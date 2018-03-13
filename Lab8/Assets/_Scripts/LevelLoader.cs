using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour 
{
	
	public void ButtonHandlerLoadLevel (int sceneIndex)
	{
		SceneManager.LoadSceneAsync (sceneIndex);
	}


}
