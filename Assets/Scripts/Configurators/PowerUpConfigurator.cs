using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowUpConf", menuName = "Configurator/PowUpConf", order = 51)]
public class PowerUpConfigurator : ScriptableObject
{
    public List<PowerUp> PowerUps;
}
 
[System.Serializable]
public class PowerUp
{
    public string Key;
    public string Description;
    public int Cost;
    public PowerUpType PowerUpType;
    public float Modificator;

        public bool IsBuy => Data.Instance.powerUpsData.keys.Contains(Key);
}