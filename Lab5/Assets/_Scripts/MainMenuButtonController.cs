using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour {

	public void ButtonControllerStartGame() {
		SceneManager.LoadSceneAsync (Globals.GAME_SCENE);
	}

}
