using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Animator animator;
    [SerializeField] float jump_scale;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            temp.jump(jump_scale);
            audioSource.Play();
            animator.SetTrigger("Pressed");
        }
    }
}
