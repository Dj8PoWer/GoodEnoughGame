using UnityEngine;
using Photon.Pun;

public class StatPanel : MonoBehaviour
{
    [SerializeField]
    StatDisplay[] statDisplays;
    [SerializeField]
    string[] statNames;

    private CharacterStat[] stats;

    private PlayerManager player;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames(); 
    }

    private void Update()
    {
        if (player == null)
        {
            PlayerManager[] players;
            players = FindObjectsOfType<PlayerManager>();

            foreach (var p in players)
            {
                if (p.gameObject.GetPhotonView().IsMine)
                    player = p;
            }
        }
    }

    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        if (stats.Length > statDisplays.Length)
        {
            Debug.Log("Not enough stat displays!");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length);

            if (i < stats.Length)
            {
                statDisplays[i].Stat = stats[i];
            }
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].Stat = stats[i];
        }
        if (player != null)
            player.LinkStats(stats);
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i];
        }
    }
}
