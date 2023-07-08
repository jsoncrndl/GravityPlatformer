using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    List<Gravity> gravities;
    [SerializeField] FloatValue mass;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Vector2 ParallelVelocity;

    bool onGround;
    [SerializeField] bool isReversed;
    [SerializeField] float walkSpeed = 1;
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
        float directionModifier = isReversed ? -1 : 1;

        var force = Vector2.zero;
        foreach (var grav in gravities)
        {
            Vector2 distance = grav.transform.position - this.transform.position;
            force += distance.normalized * ((this.mass.Value * grav.density * grav.transform.parent.localScale.x) / (distance.sqrMagnitude));
        }
        rb.AddForce(force);

        if (onGround)
        {
            this.ParallelVelocity = this.transform.right * this.walkSpeed * directionModifier;
        }
        else
        {
            ParallelVelocity -= (Vector2)Vector3.Project(ParallelVelocity, force);
            Debug.Log(this.ParallelVelocity);
        }
        
        Vector2 velocityPerpendicular = (Vector2)Vector3.Project(rb.velocity, -this.transform.up);
        rb.velocity = this.ParallelVelocity + velocityPerpendicular;



        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, -force.normalized);
    }

    public void jump(float scalar)
    {
        rb.velocity = (this.transform.up * scalar);
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
        return Physics2D.OverlapCircle(transform.position + transform.up * (capsuleCollider.size.x -.01f), capsuleCollider.size.x, groundLayers);
    }
}
