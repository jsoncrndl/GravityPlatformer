using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    private Quaternion targetRotation;
    private Vector2 rotationInput;
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

            if (isPushing)
            {
                currentPlanet.Push(-transform.up * thrust.Value);
            }
        }
    }

    private void AnimateShip()
    {
        FinishJump();
    }     

    private void UpdateRotation()
    {
        if (rotationInput.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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
        Debug.Log(aimPlanet);
    }

    public void Rotate(InputAction.CallbackContext ctx)
    {
        if (animating) return;

        rotationInput = ctx.ReadValue<Vector2>();
        targetRotation = Quaternion.LookRotation(Vector3.forward, rotationInput);

        //reverseInput = transform.up.y < 0;
    }

    private void FinishJump()
    {
        transform.parent = aimPlanet.transform;
        transform.localPosition = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, -transform.up);
        pivotChild.transform.localPosition = Vector3.up * (currentPlanet.transform.localScale.x + .01f);

        animating = false;
        finishJump.Invoke();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || aimPlanet == null || animating) return;

        //Look at the other planet
        transform.rotation = Quaternion.LookRotation(Vector3.forward, aimPlanet.transform.position - transform.position);
        startJump.Invoke();

        currentPlanet = aimPlanet;

        //Animate!
        animating = true;
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
            isPushing = false;
            stopThrusters.Invoke();
        }
    }
}
