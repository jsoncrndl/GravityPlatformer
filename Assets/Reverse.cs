using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Astronaut temp = collision.gameObject.GetComponent<Astronaut>();
        if (temp != null)
        {
            Debug.Log("hit");
            temp.reverse();
        }
    }
}
