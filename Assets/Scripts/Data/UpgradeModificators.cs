using System.Linq;
using UnityEngine;

public static class UpgradeModificators
{
    public static PowerUpConfigurator powerUpConfigurator => Resources.Load<PowerUpConfigurator>("PowUpConf");

    public static float MaxMerge = 3;

    public static void UpdateFields()
    {
        MaxMerge = 3 + powerUpConfigurator.PowerUps
            .Where(x => x.PowerUpType == PowerUpType.MaxMerge && x.IsBuy)
            .Sum(x => x.Modificator);
    }
}