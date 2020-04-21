using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Text text;
    private int skillPoints;

    public int SkillPoints
    {
        get { return skillPoints; }
        set
        {
            skillPoints = value;
            UpdateSP();
        }
    }

    private void UpdateSP()
    {
        text.text = "Skillpoints : " + skillPoints.ToString();
    }
}
