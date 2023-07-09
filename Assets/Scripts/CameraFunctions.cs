using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    [SerializeField] private FadingGraphic fadingGraphic;

    public void Shake()
    {

    }

    public void FadeIn()
    {
        fadingGraphic.FadeOut();
    }

    public void FadeOut()
    {
        fadingGraphic.FadeIn();
    }
}