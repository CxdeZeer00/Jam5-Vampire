using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public float distractedTime;
    public float healAmount;
    public float healing;
    public float vampireSpeed;
    public Sprite objectIcon;
}
