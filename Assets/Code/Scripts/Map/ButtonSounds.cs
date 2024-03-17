using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource ButtonSound;
    public AudioClip hoverSound;
    public AudioClip pressedSound;
    public AudioClip openPDA;
    public AudioClip openUserMenu;

    public Vector3 defaultScale;
    public GameObject childImage;

    public bool ignoreChild;

    private void Start()
    {
        if (!ignoreChild) 
        {
            defaultScale = childImage.transform.localScale;
            childImage.transform.localScale = defaultScale;
        }
    }



    public void playHover()
    { ButtonSound.PlayOneShot(hoverSound); }

    public void playClick()
    { ButtonSound.PlayOneShot(pressedSound); }

    public void playPDA()
    { ButtonSound.PlayOneShot(openPDA); }

    public void playUserMenu()
    { ButtonSound.PlayOneShot(openUserMenu); }

    public void EnlargeImage()
    {
        //childImage.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        LeanTween.scale(childImage, new Vector3(defaultScale.x * 1.2f, defaultScale.y * 1.2f, 1), 0.2f);
    }

    public void ShrinkImage()
    {
        //childImage.transform.localScale = new Vector3(1, 1, 1);
        LeanTween.scale(childImage, defaultScale, 0.2f);
    }
}
