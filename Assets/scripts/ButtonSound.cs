using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour {
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;

    public void onhover()
    {

        source.PlayOneShot(hover);

    }

    public void OnClick()
    {

        source.PlayOneShot(click);

    }




}
