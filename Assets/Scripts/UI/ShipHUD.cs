using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipHUD : MonoBehaviour
{
    [SerializeField] private ShipController shipController;

    public UnityEvent<float> staminaUpdated;

    // Start is called before the first frame update
    void Start()
    {
        if (shipController != null)
            shipController.staminaUpdated += UpdateStamina;
    }

    private void UpdateStamina(float percent)
    {
        staminaUpdated.Invoke(percent);
    }
}
