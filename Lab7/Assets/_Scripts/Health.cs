using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip pain;

    public void Hit()
    {
        audioSource.PlayOneShot(pain);
    }
}
