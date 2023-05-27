using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_modechange : MonoBehaviour
{
    private Animator anim;
    private AudioSource audiosource;
    public string animbool_name = "checkbool";
    public float change_maxvoice = 0.9f;
    private bool isTitle = true;
    public AudioClip[] se;
    public float siya_camera = 20;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        audiosource = this.GetComponent<AudioSource>();
        if(GManager.instance.title)
        {
            isTitle = false;
            GManager.instance.audioMax += 0.05f;
            anim.SetBool(animbool_name, false);
            Camera.main.fieldOfView = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.fieldOfView != siya_camera)
        {
            Camera.main.fieldOfView = siya_camera;
        }
        if (isTitle && !GManager.instance.title && GManager.instance.live_volume >= change_maxvoice)
        {
            isTitle = false;
            GManager.instance.audioMax += 0.05f;
            GManager.instance.title = true;
            audiosource.PlayOneShot(se[0]);
            anim.SetBool(animbool_name, false);
        }
    }
}
