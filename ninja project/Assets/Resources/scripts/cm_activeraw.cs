using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cm_activeraw : MonoBehaviour
{
    public string raw_name = "subcm_raw";
    private GameObject raw_obj = null;
    public string target_tag = "parea";
    public string wall_tag = "shadow";
    private GameObject[] p_side = null;

    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;   // Rayを飛ばす方向
    private float distance = 0;    // Rayを飛ばす距離
    public float distance_speed = 10;
    private GameObject child_pos;
    // Start is called before the first frame update
    void Start()
    {
        raw_obj = GameObject.Find(raw_name);
        p_side = GameObject.FindGameObjectsWithTag(target_tag);
        child_pos = transform.GetChild(0).gameObject;
        for (int i = 0; i < p_side.Length;)
        {
            if (p_side[i].transform.root.gameObject.tag == "player")
            {
                p_side[0] = p_side[i];
                break;
            }
                i++;
        }
        if (raw_obj != null)
        {
            raw_obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (GManager.instance.walktrg && !GManager.instance.over && raw_obj != null && p_side != null)
        //{
        //    distance += Time.deltaTime * distance_speed;
        //    Vector3 temp = p_side[0].transform.position - child_pos.transform.position;
        //    direction = temp.normalized;
        //    ray = new Ray(child_pos.transform.position, direction);  // Rayを飛ばす
        //    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Rayをシーン上に描画

        //    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
        //    {
        //        if (hit.collider.CompareTag(wall_tag) || hit.collider.CompareTag("ground") || hit.collider.CompareTag("obj"))
        //        {
        //            raw_obj.SetActive(true);
        //            distance = 0;
        //        }
        //        else if (temp.magnitude-1f  < distance)
        //        {
        //            raw_obj.SetActive(false);
        //            distance = 0;
        //        }
        //    }
        //}
    }
}
//CompareTag
