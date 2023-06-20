using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Data;

public class RoadOfClean : MonoBehaviour
{
    public static RoadOfClean instance;

    private float dist;
    public Transform PanelOfRoad;
    public Transform pointSpawn;
    public Transform pointEnd;
    public float Speed;

    public float TimerSpawn;
    private float PeriodSpawn = 2f;

    [Range(0f,1f)]
    public float PositionOnRoad;

    public List<Lens> Lenses = new List<Lens>();
    public MobScript MobPrefab;
    public List<MobScript> Mobs = new List<MobScript>();

    public static Dictionary<int, float> lensPositions = new Dictionary<int, float>()
    {
        { 0, 0.5f },
        { 1, 0.4f },
        { 2, 0.3f },
        { 3, 0.7f },
    };

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dist = pointSpawn.position.x - pointEnd.position.x;
        Lenses.ForEach(x => Data.Instance.Board.UpdateBoard += x.UpdateLabel);
    }

    void Update()
    {
        TimerSpawn -= Time.deltaTime * Speed;
        if (TimerSpawn < 0)
        {
            TimerSpawn = PeriodSpawn;
            SpawnMob();
        }

        for (int i = 0; i < Mobs.Count; i++) 
        {
            Mobs[i].PositionOnRoad -= Time.deltaTime * Speed;
            Mobs[i].transform.position = new Vector2(dist * Mobs[i].PositionOnRoad, Mobs[i].transform.position.y);
            Mobs[i].CheckLens();
            if (Mobs[i].PositionOnRoad <= -0.5f || Mobs[i].LevelLayers.Count <= 0)
            {
                Destroy(Mobs[i].gameObject);
                Mobs.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnMob() 
    {
        var mb = Instantiate(MobPrefab, pointSpawn.position, MobPrefab.transform.rotation, PanelOfRoad);
        mb.Init(10, Lenses, 10);
        Mobs.Add(mb);
    }

    public void ChangeOrderLens() 
    {
        foreach (var elem in Lenses) 
        {
            elem.position--;
            if (elem.position < 0) 
            {
                elem.position = 2;
            }
            elem.LenObject.transform.position = new Vector2(lensPositions[elem.position] * dist, elem.LenObject.transform.position.y);
        }
    }

    public void UpdatePowerLens() 
    {
        Lenses.ForEach(x => x.UpdateLabel());
    }
}

[System.Serializable]
public class Lens
{
    public int position;
    public GameObject LenObject;
    public TextMeshProUGUI TextLabel;
    public Element Element;
    public float PositionOnRoad => RoadOfClean.lensPositions[position];
    public int Power => Data.Instance.Board.Entities.Power(Element);

    public void UpdateLabel() 
    {
        TextLabel.text = $"{Power}";
    }
}