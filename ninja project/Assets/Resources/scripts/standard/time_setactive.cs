using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_setactive : MonoBehaviour
{
    public GameObject target;
    public float timelimit = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(TargetFalse), timelimit);
    }
    void TargetFalse()
    {
        target.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
