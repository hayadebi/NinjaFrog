using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightObj : MonoBehaviour
{
    public GameObject[] obj;
    public float maxhight;
    public GameObject p;
    private bool objOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(obj.Length != 0)
        {
            if(p.transform.position.y > maxhight && !objOn)
            {
                objOn = true;
                for(int i=0;i < obj.Length;)
                {
                    obj[i].SetActive(true);
                    //player pcs = p.GetComponent<player>();
                    //pcs.o8removetrg = true;
                    i++;
                }
            }
            else if (p.transform.position.y < maxhight && objOn)
            {
                objOn = false;
                for (int i = 0; i < obj.Length;)
                {
                    obj[i].SetActive(false);
                    //player pcs = p.GetComponent<player>();
                    //pcs.o8removetrg = false;
                    i++;
                }
            }
        }
    }
}
