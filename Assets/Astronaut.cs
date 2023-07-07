using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    List<Gravity> gravities;
    float mass = 1;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gravities = new List<Gravity>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var force = Vector2.zero;
        foreach (var grav in gravities)
        {
            Vector2 distance = grav.transform.position - this.transform.position;
            force += distance.normalized * ((this.mass * grav.density * grav.GetComponent<CircleCollider2D>().radius * grav.transform.parent.localScale.x) / (distance.sqrMagnitude));
        }
        rb.AddForce(force);
    }

    public void addGravity(Gravity gravity)
    {
        this.gravities.Add(gravity);
    }

    public void removeGravity(Gravity gravity)
    {
        this.gravities.Remove(gravity);
    }
}
