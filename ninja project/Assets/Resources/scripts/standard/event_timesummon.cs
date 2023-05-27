using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_timesummon : MonoBehaviour
{
    public int input_eventid = -1;
    public int input_eventnum = 1;
    public int add_event = 0;
    public float summon_time = 4f;
    public GameObject summon_obj;
    // Start is called before the first frame update
    void Start()
    {
        if(input_eventid == -1)
        {
            Invoke(nameof(ObjSummon), summon_time);
        }
        else if(GManager.instance.EventNumber[input_eventid] <= input_eventnum )
        {
            GManager.instance.EventNumber[input_eventid] += add_event;
            Invoke(nameof(ObjSummon), summon_time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ObjSummon()
    {
        Instantiate(summon_obj, transform.position, transform.rotation);
    }
}
