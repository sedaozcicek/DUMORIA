using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    AudioSource obj;
    bool obj_Play = false;
    public AudioClip clip;
    public float volume = 0.5f;
    void Start()
    {
        if (isActiveAndEnabled)
        {
            obj = GetComponent<AudioSource>();
            obj.PlayOneShot(clip, volume);
            obj_Play = true;
        }

    }
}
