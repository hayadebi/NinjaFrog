using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventdestroy : MonoBehaviour
{
    public int eventnumber;
    public int inputEvent = -1;
    [Header("1はe!=i,2はe>i,3はi>e")]public int inputmode = 1;//1はe!=i,2はe>i,3はi>e;
    public bool update_trg = false;
    // Start is called before the first frame update
    void Start()
    {
        if(inputEvent != -1)
        {
            if(inputEvent != GManager.instance.EventNumber[eventnumber] && inputmode == 1)
            {
                setDestroy();
            }
            else if (inputEvent < GManager.instance.EventNumber[eventnumber] && inputmode == 2)
            {
                setDestroy();
            }
            else if (inputEvent > GManager.instance.EventNumber[eventnumber] && inputmode == 3)
            {
                setDestroy();
            }
            else if (inputEvent == GManager.instance.EventNumber[eventnumber] && inputmode == 4)
            {
                setDestroy();
            }
        }
    }
    void setDestroy()
    {
            Destroy(gameObject,0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        if(update_trg && inputEvent != -1)
        {
            if (inputEvent != GManager.instance.EventNumber[eventnumber] && inputmode == 1)
            {
                inputEvent = -1;
                setDestroy();
            }
            else if (inputEvent < GManager.instance.EventNumber[eventnumber] && inputmode == 2)
            {
                inputEvent = -1;
                setDestroy();
            }
            else if (inputEvent > GManager.instance.EventNumber[eventnumber] && inputmode == 3)
            {
                inputEvent = -1;
                setDestroy();
            }
            else if (inputEvent == GManager.instance.EventNumber[eventnumber] && inputmode == 4)
            {
                inputEvent = -1;
                setDestroy();
            }
        }
    }
}
