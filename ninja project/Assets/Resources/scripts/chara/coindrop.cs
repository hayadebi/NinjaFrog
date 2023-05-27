using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coindrop : MonoBehaviour
{
    public int get_coinnum = 1;
    private bool gettrg = false;
    private bool movetrg = false;
    private GameObject P;
    private float move_speed = 8;
    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("coinpos");
        Invoke(nameof(MoveSet), 1);
    }
    void MoveSet()
    {
        movetrg = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (movetrg && P != null)
        {
            Vector3 pos = P.transform.position - this.transform.position;
            pos *= move_speed;
            this.GetComponent<Rigidbody>().velocity = pos;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" && !gettrg && movetrg )
        {
            gettrg = true;
            GManager.instance.setrg = 6;
            GManager.instance.get_coin += get_coinnum ;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
