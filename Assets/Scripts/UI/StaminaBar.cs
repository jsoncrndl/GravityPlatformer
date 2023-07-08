using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private float emptyX = -312;
    [SerializeField] private float moveSpeed = 1;
    private bool animateMove;
    private Vector3 targetPosition;

    public void SetFill(float amount)
    {
        targetPosition = new Vector3(Mathf.Lerp(emptyX, 0, amount), 0, 0);
        //animateMove = true;
    }

    private void Update()
    {
        if (animateMove)
        {
            fillBar.transform.localPosition = Vector3.MoveTowards(fillBar.transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);

            if (fillBar.transform.localPosition == targetPosition)
            {
                animateMove = false;
            }
        }
        else 
        {
            fillBar.transform.localPosition = targetPosition;
        }
    }
}