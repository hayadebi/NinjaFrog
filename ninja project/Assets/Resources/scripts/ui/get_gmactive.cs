using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class get_gmactive : MonoBehaviour
{
    public int check_num = 1;
    public string gm_name = "hp";
    private Image img;
    private int old_num = -1;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm_name == "hp" && old_num != GManager.instance.Pstatus.hp)
        {
            if (check_num <= GManager.instance.Pstatus.hp)
            {
                old_num = GManager.instance.Pstatus.hp;
                img.enabled = true;
            }
            else if (check_num > GManager.instance.Pstatus.hp)
            {
                old_num = GManager.instance.Pstatus.hp;
                img.enabled = false;
            }
        }
    }
}
