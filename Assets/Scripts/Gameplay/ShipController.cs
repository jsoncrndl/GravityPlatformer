using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    private float rotationInput;
    private bool animating;
    private GameObject aimPlanet;
    [SerializeField] private float rotateSpeed;

    // This script is on the pivot. pivotChild has the actual ship art.
    [SerializeField] private GameObject pivotChild; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            AnimateShip();
        }
        else
        {
            UpdateRotation();
        }
    }

    private void AnimateShip()
    {

    }     

    private void UpdateRotation()
    {
        if (rotationInput != 0)
        {
            transform.Rotate(transform.forward, rotationInput * -rotateSpeed * Time.deltaTime);
        }
    }

    public void Rotate(InputAction.CallbackContext ctx)
    {
        if (animating) return;

        rotationInput = ctx.ReadValue<float>();
    }

    public void Jump()
    {
        if (aimPlanet != null || animating) return;

        //Jump to other planet
        transform.parent = aimPlanet.transform;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.LookRotation(Vector3.forward, -transform.up));
    }

    public void Thrust()
    {
        if (animating) return;
    }
}
