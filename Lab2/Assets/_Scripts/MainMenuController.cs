using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public AudioSource playSound;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonHandlerPlay()
    {
        StartCoroutine(PlayButtonSoundCR());
    }

    public void ButtonHandlerQuit()
    {
        Application.Quit();
    }

    private IEnumerator PlayButtonSoundCR()
    {
        playSound.Play();
        yield return new WaitForSeconds(playSound.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
}
