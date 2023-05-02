using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public LogicScript logic;
    public float movespeed = 7;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movespeed = GameObject.Find("logic manager").GetComponent<LogicScript>().newmovespeed;
        transform.position += Vector3.left * movespeed * Time.deltaTime;
    }
}
