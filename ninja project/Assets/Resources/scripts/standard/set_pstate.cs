using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_pstate : MonoBehaviour
{
    public int set_mode = 0;
    private player ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps != null && ps.jumpmode != set_mode)
        {
            ps.jumpmode = set_mode;
        }
    }
}
