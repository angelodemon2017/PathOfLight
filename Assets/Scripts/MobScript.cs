using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Data;
using UnityEngine.UI;

public class MobScript : MonoBehaviour
{
    public TextMeshProUGUI LevelLabel;
    public float PositionOnRoad = 1f;
    private int _Level;
    public int Level
    {
        get => _Level;
        set
        {
            _Level = value;
            LevelLabel.text = $"{value}";
        }
    }
    private int EtherReward;
    public List<LevelLayer> LevelLayers = new List<LevelLayer>();

    public List<Image> Indicators;
    public List<Lens> lensLeft;

    public void Init(int level, List<Lens> lenses, int rew)
    {
        Level = level;
        lensLeft.AddRange(lenses);
        EtherReward = rew;
    }

    public void CheckLens()
    {
        for (int i = 0; i < lensLeft.Count; i++)
        {
            if (PositionOnRoad < lensLeft[i].PositionOnRoad && LevelLayers.Count > 0)
            {
                if (LevelLayers[0].Element == lensLeft[i].Element)
                {
                    LevelLayers[0].Level -= lensLeft[i].Power;
                    if (LevelLayers[0].Level <= 0)
                    {
                        Debug.Log($"Check {lensLeft[i].Element}");
                        lensLeft.RemoveAt(i);
                        i--;

                        LevelLayers.RemoveAt(0);
                        if (LevelLayers.Count <= 0)
                        {

                            Data.Instance.playerWallet.Ether += EtherReward;
                        }
                    }
                }
            }
        }
    }
}

public class LevelLayer
{
    public int Level;
    public Element Element;
}