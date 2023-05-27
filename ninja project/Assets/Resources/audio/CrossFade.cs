using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CrossFade : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerSnapshot[] snapshots;
    float[] weights = new float[2];

    [SerializeField] float fadetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        weights[0] = 0f;
        weights[1] = 1f;
        mixer.TransitionToSnapshots(snapshots, weights, fadetime);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    weights[0] = 1f;
        //    weights[1] = 0f;
        //    mixer.TransitionToSnapshots(snapshots, weights, 3f);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    weights[0] = 0f;
        //    weights[1] = 1f;
        //    mixer.TransitionToSnapshots(snapshots, weights, 3f);
        //}
    }
    public void AudioIn()
    {
        weights[0] = 0f;
        weights[1] = 1f;
        mixer.TransitionToSnapshots(snapshots, weights, 0.3f);
    }
    public void AudioOut()
    {
        weights[0] = 1f;
        weights[1] = 0f;
        mixer.TransitionToSnapshots(snapshots, weights, 0.3f);
    }
}