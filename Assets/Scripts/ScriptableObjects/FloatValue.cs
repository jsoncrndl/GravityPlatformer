using UnityEngine;

[CreateAssetMenu(menuName = "Value/Float", fileName = "floatValue")]
public class FloatValue : ScriptableObject
{
    [SerializeField] private float value;
    public float Value => value;
}