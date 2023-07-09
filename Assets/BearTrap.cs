using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    Animator animator;
    [SerializeField] float jump_scale;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Astronaut temp = collision.GetComponent<Astronaut>();
        if (temp != null)
        {
            Destroy(temp.gameObject);
            animator.SetTrigger("Pressed");
        }
    }
}