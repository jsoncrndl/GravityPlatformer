using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent<Astronaut> enterTrigger;
    public UnityEvent<Astronaut> exitTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Astronaut astronaut = other.GetComponent<Astronaut>();
        if (astronaut != null)
        {
            enterTrigger?.Invoke(astronaut);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Astronaut astronaut = other.GetComponent<Astronaut>();
        if (astronaut != null)
        {
            exitTrigger?.Invoke(astronaut);
        }
    }
}