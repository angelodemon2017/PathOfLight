using UnityEngine;

public class DynamicPanels : MonoBehaviour
{
    private PowerUpConfigurator powerUpConfigurator => Resources.Load<PowerUpConfigurator>("PowUpConf");

    public Transform PowerUpParent;
    public PanelPowerUp PrefabPanelPower;

    private void Awake()
    {
        foreach (var pu in powerUpConfigurator.PowerUps)
        {
            Instantiate(PrefabPanelPower, PowerUpParent).Init(pu.Key);
        }
    }
}