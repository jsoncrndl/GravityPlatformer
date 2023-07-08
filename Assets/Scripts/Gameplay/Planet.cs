using UnityEngine;

[SelectionBase]
public class Planet : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force);
        Debug.Log(force);
    }
}
