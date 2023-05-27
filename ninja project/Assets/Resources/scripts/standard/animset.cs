using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animset : MonoBehaviour
{
    public bool startTrg = false;
    Animator anim;
    public int inputanim;
    public string variname = "Anumber";
    int oldanim = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        if(startTrg == true)
        {
            oldanim = inputanim;
            anim.SetInteger(variname, inputanim);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inputanim != oldanim && startTrg == false)
        {
            oldanim = inputanim;
            anim.SetInteger(variname, inputanim);
        }
    }
}
