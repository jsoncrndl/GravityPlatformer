using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Planet temp = collision.GetComponent<Planet>();
        if (temp != null)
        {
            temp.startAtmosphere();
            temp.setAtmosphereUpVector(this.transform.position - temp.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Planet temp = collision.GetComponent<Planet>();
        if (temp != null)
        {
            temp.stopAtmosphere();
        }
    }
}
