using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child_setactive : MonoBehaviour
{
    public GameObject[] child_set;
    public bool set_mode = false;
    private bool old_mode = true;
    public Animator anim;
    public string anim_boolname = "check";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void ChildSet()
    {
        for (int i = 0; i < child_set.Length;)
        {
            child_set[0].SetActive(false);
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(old_mode != set_mode )
        {
            old_mode = set_mode;
            if (anim_boolname == "")
            {
                ChildSet();
            }
            else
            {
                anim.SetBool(anim_boolname, set_mode);
            }
        }
    }
}
