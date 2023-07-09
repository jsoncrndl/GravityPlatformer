using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] LayerMask ignoreLayers;
    List<Gravity> gravities;
    [SerializeField] FloatValue mass;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    Vector2 ParallelVelocity;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool onGround;
    [SerializeField] bool isReversed;
    [SerializeField] float walkSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        gravities = new List<Gravity>();
        rb = this.GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            Vector2 gravForce = distance.normalized * ((this.mass.Value * grav.density * grav.transform.parent.localScale.x) / (distance.sqrMagnitude));

            Debug.DrawRay(transform.position, gravForce * 1000, Color.yellow);
            
            force += gravForce;
        }

        Debug.DrawRay(transform.position, force * 1000, Color.red);
        rb.AddForce(force);

        if (onGround)
        {
            this.ParallelVelocity = this.transform.right * this.walkSpeed * directionModifier;
            animator.SetBool("Landed", true);
        }
        else
        {
            ParallelVelocity -= (Vector2)Vector3.Project(ParallelVelocity, force);
        }
        
        Vector2 velocityPerpendicular = (Vector2)Vector3.Project(rb.velocity, -this.transform.up);
        rb.velocity = this.ParallelVelocity + velocityPerpendicular;

        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, -force.normalized);
    }

    public void jump(float scalar)
    {
        rb.velocity = (this.transform.up * scalar);
        animator.SetTrigger("IsJumping");

    }

    public void reverse()
    {
        isReversed = !isReversed;
        spriteRenderer.flipX = !isReversed;
    }

    public bool getReversed()
    {
        return isReversed;
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
        return Physics2D.OverlapCircle(transform.position + transform.up * (capsuleCollider.size.x -.01f - capsuleCollider.size.y / 2), capsuleCollider.size.x, ~ignoreLayers);
    }
}
