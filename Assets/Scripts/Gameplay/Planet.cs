using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Planet : MonoBehaviour
{
    public float radius;
    private Rigidbody2D rb;
    private FloatValue rocketThrust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push(Vector2 direction)
    {
        rb.AddForce(direction * rocketThrust.Value);
    }
}
