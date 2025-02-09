using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject {
    public new string name;
    public float health;
    public float stamina;

    public float primaryDamage;
    public float primaryRange;
    public float primaryRof;
    public float primaryMagSize;

    public float secondaryDamage;
    public float secondaryRange;
    public float secondaryRof;
    public float secondaryMagSize;
}
