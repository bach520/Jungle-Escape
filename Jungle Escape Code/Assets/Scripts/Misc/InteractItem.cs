using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    public Item item;
    public AudioClip clip;

    public void PlayAudio(AudioSource aoudio)
    {
        if(aoudio)
        {
            aoudio.clip = clip;
            aoudio.Play();
        }
    }
}
