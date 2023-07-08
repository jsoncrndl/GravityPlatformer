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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * transform.lossyScale.x);
    }
}
