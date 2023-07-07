using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    List<Gravity> gravities;
    [SerializeField] FloatValue mass;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;

    bool onGround;
    int CounterClockwise = -1;
    float walkSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        gravities = new List<Gravity>();
        rb = this.GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        onGround = GroundCheck();
        Debug.Log(onGround);

        var force = Vector2.zero;
        foreach (var grav in gravities)
        {
            Vector2 distance = grav.transform.position - this.transform.position;
            force += distance.normalized * ((this.mass.Value * grav.density * grav.transform.parent.localScale.x) / (distance.sqrMagnitude));
        }
        rb.AddForce(force);
        Vector2 velocityParallel = onGround ? this.transform.right * this.walkSpeed * this.CounterClockwise : Vector2.zero;
        Vector2 velocityPerpendicular = Vector2.Dot(rb.velocity, -this.transform.up) * this.transform.up;
        rb.velocity = velocityParallel + velocityPerpendicular;

        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, force.normalized);
    }

    public void jump(float scalar)
    {
        Vector3 velocityParallel = this.transform.right * this.walkSpeed * this.CounterClockwise;
        rb.velocity = velocityParallel + (this.transform.up * scalar);
    }

    public void addGravity(Gravity gravity)
    {
        this.gravities.Add(gravity);
    }

    public void removeGravity(Gravity gravity)
    {
        this.gravities.Remove(gravity);
    }

    public bool GroundCheck()
    {
        return Physics2D.OverlapCircle(transform.position + transform.up * capsuleCollider.size.x, capsuleCollider.size.x, groundLayers);
    }
}
