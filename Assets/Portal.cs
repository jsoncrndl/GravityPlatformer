using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal twin;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Astronaut temp = collision.GetComponent<Astronaut>();
        if (temp != null && twin != null)
        {
            temp.transform.position = twin.transform.position + 2 *(temp.getReversed() ? -twin.transform.right : twin.transform.right);
            temp.transform.rotation = twin.transform.rotation;
            temp.resetVelocity();
            audioSource.Play();
        }
    }
}