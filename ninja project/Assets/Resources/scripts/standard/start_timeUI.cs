using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_timeUI : MonoBehaviour
{
    public float ui_time = 0.3f;
    public GameObject ui;
    public int check_event = 3;
    public int over_eventdestroy = 0;
    public bool addevent = true;
    public bool mission_trg = false;

    // Start is called before the first frame update
    void Start()
    {
        if (mission_trg && GManager.instance.all_mission[check_event].clear_mission == over_eventdestroy)
        {
            GManager.instance.setmenu = 1;
            GManager.instance.walktrg = false;
            Invoke(nameof(SetUI), ui_time);
        }
        else if (!mission_trg && GManager.instance.EventNumber[check_event] == over_eventdestroy)
        {
            GManager.instance.setmenu = 1;
            GManager.instance.walktrg = false;
            Invoke(nameof(SetUI), ui_time);
        }
        
    }
    void SetUI()
    {
        if (addevent)
            GManager.instance.EventNumber[check_event] += 1;
        Instantiate(ui, transform.position, transform.rotation);
        Destroy(gameObject, 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
