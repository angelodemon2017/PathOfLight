using Assets.Scripts.Data;
using Assets.Scripts.StaticDictionaries;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProcess : MonoBehaviour
{
    public static GameProcess Instance;

    public int TempCellId;
    public Image TempCellImage;
    public EntityOfBoard TempCell = new EntityOfBoard();
    public TextMeshProUGUI TextProfit;
    public TextMeshProUGUI TextBalance;
    public TextMeshProUGUI TextButtonSpawn;
    private float TimerBalance = 1f;

    private float CostSpawn => BaseCostSpawn * MultiCostSpawn * CountBuy;
    private float BaseCostSpawn = 5f;
    private float MultiCostSpawn = 2f;
    private int CountBuy = 0;

    private void Awake()
    {
        Instance = this;
        TempCell.updateEntity += UpdateTemp;
        Wallet.updateProfit += UpdateTextProfit;
        UpdateSpawnButton();
    }

    void Start()
    {
        Data.Instance.Init();
        BoardController.Instance.Init();
//        UpgradeModificators.UpdateFields();
        DeativateTemp();
    }

    void Update()
    {
        TimerBalance -= Time.deltaTime;
        if (TimerBalance < 0)
        {
            TimerBalance = 1f;
            Data.Instance.playerWallet.Ether += Wallet.CurrentProfit;
            TextBalance.text = $"{Data.Instance.playerWallet.Ether} ether";
        }
        TempCellImage.transform.position = Input.mousePosition;
    }

    public void ActivateTemp() 
    {
        TempCellImage.gameObject.SetActive(true);
    }

    public void DeativateTemp()
    {
        TempCellImage.gameObject.SetActive(false);
        TempCell.Clean();
    }

    public void SpawnRandomEntity() 
    {
        if (Data.Instance.playerWallet.TrySpend((int)CostSpawn))
        {
            Data.Instance.Board.TrySpawnRandomEntity();
            CountBuy++;
            UpdateSpawnButton();
        }
    }

    private void UpdateSpawnButton()
    {
        TextButtonSpawn.text = $"Spawn: {CostSpawn}";
    }

    public void UpdateTemp() 
    {
        TempCellImage.color = ColorElement.ColorByElement[TempCell.MyElement];
    }

    public void UpdateTextProfit()
    {
        TextProfit.text = $"{Wallet.CurrentProfit} /s";
    }
}