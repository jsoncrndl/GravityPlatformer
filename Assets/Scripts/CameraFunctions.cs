using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    [SerializeField] private FadingGraphic fadingGraphic;

    public float shakeTime = .5f;

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        CinemachineBasicMultiChannelPerlin noise = ((CinemachineVirtualCamera)GetComponent<CinemachineBrain>().ActiveVirtualCamera).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        for (int i = 9; i >= 0; i--)
        {
            noise.m_AmplitudeGain = i;
            yield return new WaitForSeconds(shakeTime / 10);
        }
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