using UnityEngine;

[SelectionBase]
public class Planet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject enter_atmosphere;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Planet>())
        {

        }
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void startAtmosphere()
    {
        enter_atmosphere.SetActive(true);
    }

    public void stopAtmosphere()
    {
        enter_atmosphere.SetActive(false);
    }

    public void setAtmosphereUpVector(Vector3 up)
    {
        enter_atmosphere.transform.rotation = Quaternion.LookRotation(Vector3.forward, up);
    }
}
