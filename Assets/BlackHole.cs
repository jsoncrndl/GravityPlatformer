using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
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
        if (collision.collider.GetComponent<Planet>())
        {
            Destroy(collision.collider.GetComponent<Planet>().gameObject);
        }
        else if (collision.collider.GetComponent<Astronaut>())
        {
            Destroy(collision.collider.GetComponent<Astronaut>().gameObject);
        }
    }
}
