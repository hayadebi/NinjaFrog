using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sayTrigger : MonoBehaviour
{
    public bool saystop = false;
    private float saytime = 0;
    public float maxtime = 5;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        //if(saystop && GManager.instance.walktrg )
        //{
        //    saytime += Time.deltaTime;
        //    if(saytime >maxtime)
        //    {
        //        saytime = 0;
        //        saystop = false;
        //    }
        //}
    }
}
