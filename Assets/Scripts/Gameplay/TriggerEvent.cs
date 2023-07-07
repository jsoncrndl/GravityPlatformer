using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent<Astronaut> enterTrigger;
    public UnityEvent<Astronaut> exitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Astronaut astronaut = GetComponent<Astronaut>();
        if (astronaut)
        {
            enterTrigger?.Invoke(astronaut);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Astronaut astronaut = GetComponent<Astronaut>();
        if (astronaut)
        {
            enterTrigger?.Invoke(astronaut);
        }
    }
}