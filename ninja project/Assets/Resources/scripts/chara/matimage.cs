using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matimage : MonoBehaviour
{
    private SpriteRenderer material;
    [System.Serializable]
    public struct set_image
    {
        public Sprite[] image;
    }
    public set_image[] setimage;
    public int select_index;
    public int select_image;
    public int old_image = 0;
    // Start is called before the first frame update
    void Start()
    {
        material = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(material != null && setimage[select_index].image.Length > select_image &&  old_image != select_image)
        {
            old_image = select_image;
            material.sprite = setimage[select_index].image[select_image];
        }
    }
}
