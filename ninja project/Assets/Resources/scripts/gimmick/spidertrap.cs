using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spidertrap : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public GameObject effect;
    public AudioSource audioSource;
    public AudioClip se;
    // Start is called before the first frame update
    void Start()
    {
        SpriteSet(false);
    }
    void SpriteSet(bool settrg = false)
    {
        for (int i = 0; i < sprites.Length;)
        {
            sprites[i].enabled = settrg;
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player" || col.tag == "enemy")
        {
            Instantiate(effect, transform.position, transform.rotation);
            audioSource.PlayOneShot(se);
            SpriteSet(true);
        }
    }
}
