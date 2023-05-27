using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundM : MonoBehaviour
{
    public AudioClip[] se;
    AudioSource audioS;
    
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        GManager.instance.setmenu = 0;
        GManager.instance.over = false;
        GManager.instance.walktrg = true;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown (KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        if( GManager.instance.ase != null)
        {
            audioS.PlayOneShot(GManager.instance.ase);
            GManager.instance.ase = null;
        }
        else if( GManager.instance.setrg != -1 && GManager.instance.setrg != 99)
        {
            audioS.PlayOneShot(se[GManager.instance.setrg]);
            GManager.instance.setrg = -1;
        }
    }

}
