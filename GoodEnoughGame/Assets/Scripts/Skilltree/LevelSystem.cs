using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField]
    ExpUI expUI;
    [SerializeField]
    SkillTree skillTree;

    private int level = 0;
    private int experience = 0;
    private int experienceToNextLevel = 100;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            AddExperience(20);
    }


    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            expUI.OnLevelChanged();
            skillTree.SkillPoints++;
        }
        expUI.OnExperienceChanged();
    }

    public int GetLevel => level;

    public float GetExperienceNormalized()
    {
        return (float) experience / experienceToNextLevel;
    }
}