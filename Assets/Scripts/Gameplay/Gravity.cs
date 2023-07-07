using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    //enum GravityType { RADIAL, LINEAR }
    public float density;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Astronaut temp = collision.GetComponent<Astronaut>();
        if (temp != null)
        {
            temp.addGravity(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Astronaut temp = collision.GetComponent<Astronaut>();
        if (temp != null)
        {
            temp.removeGravity(this);
        }
    }
}
