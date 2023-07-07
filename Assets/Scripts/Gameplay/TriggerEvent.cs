using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent enterTrigger;
    public UnityEvent exitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Astronaut astronaut = GetComponent<Astronaut>();
        if (astronaut)
        {
            enterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Astronaut astronaut = GetComponent<Astronaut>();
        if (astronaut)
        {
            enterTrigger?.Invoke();
        }
    }
}
