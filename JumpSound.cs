using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 1;
    public GameObject bird;
    public GameObject button;
    public int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.GetComponent<BirdScript>().isBirdAlive == true && Input.GetKeyDown(KeyCode.Space))
        {
                audioSource.PlayOneShot(audioSource.clip, volume);
        }
    }
}
