using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFX : MonoBehaviour
{
    public AudioSource my;
    public AudioClip hoverFx;
    public AudioClip clickFx;


    public void HoverSound()
    {
        my.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        my.PlayOneShot(clickFx);
    }
}
