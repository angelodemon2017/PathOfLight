using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Data;
using System.Linq;

public class PanelPowerUp : MonoBehaviour
{
    private PowerUpConfigurator powerUpConfigurator => Resources.Load<PowerUpConfigurator>("PowUpConf");
    private PowerUp powerUp => powerUpConfigurator.PowerUps.FirstOrDefault(x => x.Key == Key);

    public Button ButtonInfo;
    public Button ButtonBuy;
    public TextMeshProUGUI TextInfo;
    public TextMeshProUGUI TextDescription;
    public TextMeshProUGUI TextCost;
    private string Key;

    public void Init(string key)
    {
        Key = key;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        TextInfo.text = powerUp.Key;
        TextDescription.text = powerUp.Description;
        TextCost.text = $"{powerUp.Cost}";
    }

    public void InfoAction()
    {

    }

    public void BuyAction()
    {
        if (Data.Instance.playerWallet.TrySpend(powerUp.Cost))
        {
            Data.Instance.powerUpsData.keys.Add(Key);

            UpgradeModificators.UpdateFields();
            Destroy(gameObject);
        }
    }
}