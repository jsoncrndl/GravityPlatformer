using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    private float rotationInput;
    private bool animating;
    private Planet aimPlanet;

    [SerializeField] private Planet currentPlanet;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private FloatValue thrust;
    // This script is on the pivot. pivotChild has the actual ship art.
    [SerializeField] private GameObject pivotChild;

    private bool isPushing;
    private bool reverseInput;

    public UnityEvent startThrusters;
    public UnityEvent stopThrusters;

    public UnityEvent startJump;
    public UnityEvent finishJump;

    [SerializeField] float jumpDuration;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] AnimationCurve jumpRotationCurve;
    private float animationTimer;
    private Vector3 targetAnimPosition;
    private Quaternion targetAnimRotation;

    [SerializeField] private float maxFuel = 10;
    private float fuel;

    public event Action<float> staminaUpdated;

    // Start is called before the first frame update
    void Start()
    {
        fuel = maxFuel;
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

            if (isPushing)
            {
                currentPlanet.Push(-transform.up * thrust.Value);
                fuel -= Time.deltaTime;

                fuel = Math.Max(0, fuel);
                if (fuel == 0)
                {
                    StopThrusters();
                }

                staminaUpdated?.Invoke(fuel / maxFuel);
            }
        }
    }

    private void AnimateShip()
    {
        animationTimer += Time.deltaTime;

        Debug.DrawRay(currentPlanet.transform.position, targetAnimPosition - currentPlanet.transform.position, Color.red);
        Vector3 newPosition = Vector3.Lerp(pivotChild.transform.position, targetAnimPosition, jumpCurve.Evaluate(animationTimer / jumpDuration));

        pivotChild.transform.rotation = Quaternion.Lerp(transform.rotation, targetAnimRotation, jumpRotationCurve.Evaluate(animationTimer / jumpDuration));

        if ((newPosition - targetAnimPosition).sqrMagnitude <= .01)
        {
            FinishJump();
        }
        else
        {
            pivotChild.transform.position = newPosition;
        }
    }     

    private void UpdateRotation()
    {
        if (rotationInput != 0)
        {
            transform.Rotate(transform.forward, rotationInput * -rotateSpeed * Time.deltaTime);
        }

        RaycastHit2D result = Physics2D.Raycast(pivotChild.transform.position, transform.up, 100, LayerMask.GetMask("Planet"));

        if (result.collider != null)
        {
            if (aimPlanet != null && aimPlanet.gameObject == result.collider.gameObject) return;
            
            aimPlanet = result.collider.GetComponent<Planet>();
        }
        else
        {
            aimPlanet = null;
        }
    }

    public void Rotate(InputAction.CallbackContext ctx)
    {
        if (animating) return;

        rotationInput = ctx.ReadValue<float>();

        //reverseInput = transform.up.y < 0;
    }

    private void FinishJump()
    {
        animationTimer = 0;
        pivotChild.transform.parent = transform;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, -transform.up);
        pivotChild.transform.localPosition = Vector3.up * (currentPlanet.transform.localScale.x + .01f);
        pivotChild.transform.localRotation = Quaternion.identity;

        animating = false;
        finishJump.Invoke();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || aimPlanet == null || animating) return;

        //Look at the other planet
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPlanet.transform.position - transform.position);
        startJump.Invoke();

        pivotChild.transform.parent = null;
        currentPlanet = aimPlanet;

        transform.parent = currentPlanet.transform;
        transform.localPosition = Vector3.zero;

        //Animate!
        animating = true;
        targetAnimPosition = transform.position - transform.up * currentPlanet.transform.localScale.x;
        targetAnimRotation = Quaternion.LookRotation(Vector3.forward, -transform.up);
    }

    public void Thrust(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isPushing = true;
            startThrusters.Invoke();
        }
        else if (ctx.canceled)
        {
            StopThrusters();
        }
    }

    public void StopThrusters()
    {
        isPushing = false;
        stopThrusters.Invoke();
    }

    public void SetFuel(float seconds)
    {
        fuel = seconds;
    }
}
