using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 1;
    public GameObject bird;
    public int i = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bird.GetComponent<BirdScript>().isBirdAlive == false)
        {
            if (i == 1)
            {
                audioSource.PlayOneShot(audioSource.clip, volume);
            }
            i++;
        }
    }
}
