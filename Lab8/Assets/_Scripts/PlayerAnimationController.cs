using UnityEngine;
using System.Collections;

public class PlayerAnimationController : Photon.MonoBehaviour 
{
	public bool IsGrabbing { get; private set; }
	public bool IsFallenOver { get; private set; }
	
	private PlayerMovementController playerController;
	private CameraController cameraController;
    private GameObject model;
	
	private string[] grabList;
	private string[] bailList;
	private string recoverAnim = "up";


	public bool MakeRandomlyFallOverInMenu = false;
	public AudioSource audio;

    Vector3 correctPlayerPos;
    Quaternion correctPlayerRot;

    void Awake ()
	{
        correctPlayerPos = transform.position;
        correctPlayerRot = transform.rotation;

        playerController = GetComponent<PlayerMovementController>();
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
        model = transform.Find("model").gameObject;
		
		grabList = new string[3];
		grabList[0] = "nosegrab";
		grabList[1] = "indy";
		grabList[2] = "mute";
		
		bailList = new string[2];
		bailList[0] = "fall1";
		bailList[1] = "fall2";

        if (photonView.isMine)
        {
            cameraController.target = transform;
        }
    }

    IEnumerator Start() {
		if (MakeRandomlyFallOverInMenu) { 
			while (true) {
				yield return new WaitForSeconds (Random.Range (5, 15));
                photonView.RPC("PlayFalloverAnimationRPC", PhotonTargets.All);
			}
		} else yield return null; // IEnumerator needs something to return so delay by one frame
	}
	
	void Update () 
	{
        if (photonView.isMine)
        {
            if (playerController.grounded)
            {
                // land whilst bailing or attempt grab on floor
                if (IsGrabbing || (Input.GetKeyDown(KeyCode.G) && !IsFallenOver))
                {
                    IsGrabbing = false;
                    StopAllCoroutines();
                    photonView.RPC("PlayFalloverAnimationRPC", PhotonTargets.All);
                }
            }
            else
            {
                // if player presses the G key whilst they're not grabbing
                if (!IsGrabbing && Input.GetKeyDown(KeyCode.G))
                {
                    photonView.RPC("PlayGrabAnimationRPC", PhotonTargets.All);
                }
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 10f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void PlayFalloverAnimationRPC()
    {
        StartCoroutine(PlayFalloverAnimation());
    }

    [PunRPC]
    public void PlayGrabAnimationRPC()
    {
        StartCoroutine(PlayGrabAnimation());
    }

    IEnumerator PlayGrabAnimation ()
	{
		IsGrabbing = true;
		
		// choose random animation
		string animationName = grabList[Random.Range(0, grabList.Length)];
		
		// increase the speed of the animation to 2
		model.GetComponent<Animation>()[animationName].speed = 2;
		
		// calculate length of the animation, multiply by 0.5 to get actual time since we're playing at double speed
		float aniLength = model.GetComponent<Animation>()[animationName].length * 0.5F;
		
		// play the animation passed via parameter immediately
		model.GetComponent<Animation>().Play(animationName);
		
		// and queue up the idle animation to play once it's completed
		model.GetComponent<Animation>().CrossFadeQueued("idle", 0.5F, QueueMode.CompleteOthers);
		
		// wait until animation is complete before setting grabbing back to false
		yield return new WaitForSeconds(aniLength);
		
		IsGrabbing = false;
	}
	
	
	IEnumerator PlayFalloverAnimation ()
	{
		audio.Play ();

		IsFallenOver = true;
		
		// disable the players movement
		playerController.movementEnabled = false;
		
		// choose random animation
		string animationName = bailList[Random.Range(0, bailList.Length)];
		
		// calculate the length of the animations
		float aniLength = model.GetComponent<Animation>()[animationName].length + (model.GetComponent<Animation>()[recoverAnim].length * 0.8F);
		
		// queue up and cross fade the animations in, one after another...
		model.GetComponent<Animation>().Play(animationName);
		model.GetComponent<Animation>().CrossFadeQueued("up", 0.3F, QueueMode.CompleteOthers);
		model.GetComponent<Animation>().CrossFadeQueued("idle", 0.3F, QueueMode.CompleteOthers);
		
		// wait until the animation is complete before toggling animateOnlyIfVisible back on
		yield return new WaitForSeconds(aniLength);
		
		// enable movement again
		if (!MakeRandomlyFallOverInMenu) playerController.movementEnabled = true;
		
		// set bailed to false
		IsFallenOver = false;
	}
	
}
