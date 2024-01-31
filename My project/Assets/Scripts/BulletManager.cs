using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Stats")]
    public int baseDamage = 1;
    public List<string> possibleAbilities = new List<string>();
    public List<string> unlockedAbilities = new List<string>();

    public void UnlockAbility(string abiltyName)
    {
        unlockedAbilities.Add(abiltyName);
    }

}
