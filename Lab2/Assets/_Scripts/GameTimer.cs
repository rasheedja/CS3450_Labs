using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    private float totalTime;
    public float timeLeft = 60;
    public Text timerText;
    public Image radialTimerImage;

    void Awake()
    { 
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        totalTime = timeLeft;
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;
        bool changedColor = false;

        if (timeLeft <= 10 && !changedColor)
        {
            timerText.color = new Color(255, 0, 0);
            changedColor = true;
        }

        if (timeLeft >= 0)
        {
            UpdateUI();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }

    void UpdateUI()
    {
        // Update timer
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0}", seconds.ToString("00"));

        // Update radial image
        radialTimerImage.fillAmount = timeLeft / totalTime;
    }

}
