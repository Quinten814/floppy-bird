using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public float spawnRate = 2;
    private float timer = 0;
    public GameObject pipe;
    public float heightoffset;
    void PipeSpawn()
    {
        float lowestPoint = transform.position.y - heightoffset;
        float highestPoint = transform.position.y + heightoffset;
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate -= 0.000003f;
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            PipeSpawn();
            timer = 0;
        }
    }
}
