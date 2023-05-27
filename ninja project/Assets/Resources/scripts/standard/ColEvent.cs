using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEvent : MonoBehaviour
{
    public bool ColTrigger = false;
    public bool onAction = true;
    public string tagName = "Player";
    public string tagName2 = "";
    public bool name2Trigger = false;
    public GameObject noname2;
    public string name2_target = "Player";
    private GameObject Name2Target;
    public bool managerTrg = false;
    public int managerIndex = 0;
    public bool stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        if(noname2 != null)
        {
            Name2Target = GameObject.Find(name2_target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == tagName2 && onAction && noname2 != null && noname2 != col.gameObject && col.GetComponent<enemy>() && !col.GetComponent<enemy>().enemy_dstrg  && ((Name2Target != null &&Vector3.Distance(col.gameObject.transform.position, Name2Target.transform.position ) < Vector3.Distance(this.transform.position, Name2Target.transform.position))||Name2Target==null))
        {
            name2Trigger = true;
        }
        if(col.tag == tagName && onAction)
        {
            ColTrigger = true;
            if(managerTrg )
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
        else if (tagName == "" && onAction && col.tag != "player" && col.tag != "enemy" && col.tag != "untag" && col.tag != "event" && col.tag != "water" && col.tag != "?" && col.tag != "parea" && col.tag != "shadow" && col.tag != "bero")
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == tagName && onAction && !ColTrigger)
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
        else if (tagName == "" && onAction && !ColTrigger && col.tag != "player" && col.tag != "enemy" && col.tag != "untag" && col.tag != "event" && col.tag != "water" && col.tag != "?" && col.tag != "parea" && col.tag != "shadow" && col.tag != "bero")
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == tagName2 && onAction )//&& noname2 != null && noname2 != col.gameObject)
        {
            name2Trigger =false;
        }
        if (col.tag == "Water" && tagName == "")
        {
            GManager.instance.setrg = 10;
        }
        if (col.tag == tagName)
        {
            ColTrigger = false;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = false;
            }
        }
        else if (tagName == "" && onAction && col.tag != "player" && col.tag != "enemy" && col.tag != "untag" && col.tag != "event" && col.tag != "water" && col.tag != "?" && col.tag != "parea" && col.tag != "shadow" && col.tag != "bero")
        {
            ColTrigger = false;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = false;
            }
        }
    }
}
