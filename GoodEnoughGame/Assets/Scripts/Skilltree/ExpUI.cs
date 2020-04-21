using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField]
    Image xpbarImage;
    [SerializeField]
    Text levelText;
    [SerializeField]
    LevelSystem levelSystem;

    private void Awake()
    {
        SetLevelSystem(levelSystem);
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        xpbarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int lvl)
    {
        levelText.text = (lvl + 1).ToString();
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        SetLevelNumber(levelSystem.GetLevel);
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }

    public void OnLevelChanged()
    {
        SetLevelNumber(levelSystem.GetLevel);
    }

    public void OnExperienceChanged()
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
