using UnityEngine;

public enum KindOfItem { Stake, Garlic, HolyWater, BloodVial, Key}
[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public KindOfItem type;
    public float distractedTime;
    public float healAmount;
    public float healing;
    public float vampireSpeed;
    public Sprite objectIcon;
    public bool isKey;
}
