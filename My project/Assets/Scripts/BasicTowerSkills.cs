using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerSkills
{
    public enum SkillType
    {
        Shock,
    }

    private List<SkillType> unlockedSkillTypeList;

    public BasicTowerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skillType)
    {
        unlockedSkillTypeList.Add(skillType);
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }
}
