using UnityEngine;

[CreateAssetMenu(menuName = "Value/Integer", fileName = "intValue")]
public class IntegerValue : ScriptableObject
{
    [SerializeField] private int value;
    public int Value => value;
}