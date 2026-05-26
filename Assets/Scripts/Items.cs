using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public float distractedTime;
    public float wearingTime;
    public Sprite objectIcon;
}
