using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeController : Photon.MonoBehaviour
{
    public int speed = 3;
    public Rigidbody rb;
    public MeshRenderer renderer;
    Vector3 correctPlayerPos;
    Quaternion correctPlayerRot;


    void Awake()
    {
        correctPlayerPos = transform.position;
        correctPlayerRot = transform.rotation;
    }

    void Update()
    {
        if (photonView.isMine)
        {
            // retrieve user input
            float v = Input.GetAxis("Vertical"); // W/S keys & Up/Down arrows
            float h = Input.GetAxis("Horizontal"); // A/S keys & Left/Right arrows

            // move forwards only
            if (v > 0) transform.position += (transform.forward * (speed * Time.deltaTime));

            // rotate
            transform.RotateAround(Vector3.up, (h * speed) * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("ChangeColour", PhotonTargets.AllBuffered, new object[] { Random.value, Random.value, Random.value });
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
            stream.SendNext(rb.velocity);

        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void ChangeColour(float r, float g, float b)
    {
        // change the colour of the material using the RGB values passed via parameters
        renderer.material.color = new Color(r, g, b);
    }
}
