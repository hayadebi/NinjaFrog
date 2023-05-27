using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ui : MonoBehaviour
{
    public GameObject set_ui;
    public string target_ui = "player";
    private bool set_trg = false;
    public bool get_trg = false;
    public int mission_check = -1;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == target_ui && col.GetComponent <player>() && get_trg == col.GetComponent<player >().get_missiontarget && !set_trg && (mission_check==-1 || (mission_check!=-1&&GManager.instance.Triggers[GManager.instance.all_mission[mission_check].targettrg_id] >= GManager.instance.all_mission[mission_check].targettrg_num)))
        {
            set_trg = true;
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            Instantiate(set_ui, transform.position, transform.rotation);
        }
    }
}
