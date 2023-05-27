using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mode_active : MonoBehaviour
{
    public int max_mode = 3;
    public int min_mode = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.mode >= min_mode && GManager.instance.mode <= max_mode)
        {
            ;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
