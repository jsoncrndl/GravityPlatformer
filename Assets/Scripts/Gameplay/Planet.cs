using System.Collections;
using UnityEngine;

[SelectionBase]
public class Planet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float angularVelocity;
    [SerializeField] private GameObject enter_atmosphere;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject starEffectPrefab;
    [SerializeField] private FloatValue hitStopTime;
    [SerializeField] private AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.GetComponent<Planet>())
        {
            StartCoroutine(Explode(collision.gameObject, collision.contacts[0].point));
        }
    }

    private IEnumerator Explode(GameObject hitObject, Vector3 hitPoint)
    {
        hitObject.GetComponent<Rigidbody2D>().isKinematic = true;
        hitObject.GetComponent<Rigidbody2D>().velocity *= 0;
        rb.isKinematic = true;
        rb.velocity *= 0;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(hitStopTime.Value);
        Camera.main.GetComponent<CameraFunctions>().Shake();
        Time.timeScale = 1;
        Instantiate(explosionPrefab, transform.position - Vector3.forward, Quaternion.identity).transform.localScale = transform.localScale;
        Instantiate(explosionPrefab, hitObject.transform.position - Vector3.forward, Quaternion.identity).transform.localScale = transform.localScale;
        Instantiate(starEffectPrefab, hitPoint - Vector3.forward * 2, Quaternion.identity);
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(explosion, hitPoint);
        Destroy(gameObject, 1);
        Destroy(hitObject);
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
