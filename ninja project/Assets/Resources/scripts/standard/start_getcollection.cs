using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_getcollection : MonoBehaviour
{
    public int get_collectid;
    // Start is called before the first frame update
    void Start()
    {
        GManager.instance.all_frog[get_collectid].check_trg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
