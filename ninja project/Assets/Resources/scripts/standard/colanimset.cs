using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colanimset : MonoBehaviour
{
    Animator anim;
    public string variname = "Anumber";
    public string tagname = "player";
    public GameObject lightrayobj;
    // Start is called before the first frame update
    void Start()
    {
        anim = lightrayobj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (anim.GetInteger(variname) != 0)
        {
            anim.SetInteger(variname, 0);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (anim.GetInteger(variname) != 1)
        {
            anim.SetInteger(variname, 1);
        }
    }
}
