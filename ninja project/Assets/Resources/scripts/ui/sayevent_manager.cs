using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sayevent_manager : MonoBehaviour
{
    public GameObject[] sayevents;
    // Start is called before the first frame update
    void Start()
    {
        sayevents[GManager.instance.say_eventID].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
