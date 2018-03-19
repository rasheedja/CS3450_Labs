using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CutsceneController : MonoBehaviour {
    public Image imageToFade;
    public Transform cameraEndPosition;
    public Transform cam;
    public AudioSource clownAudio;
    public Animator clownController;
    public RagdollController ragdoll;
    private int danceHashID;

	// Use this for initialization
	void Start () {
        danceHashID = Animator.StringToHash("dance");
        StartCoroutine("Cutscene");
	}

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        imageToFade.DOFade(0, 2);
        cam.DOMove(cameraEndPosition.position, 10);
        yield return new WaitForSeconds(8);
        clownAudio.Play();
        clownController.SetTrigger(danceHashID);
        yield return new WaitForSeconds(10);
        clownAudio.Stop();
        ragdoll.EnableRagdoll();
        yield return new WaitForSeconds(5);
        imageToFade.DOFade(1, 2);
    }
}
