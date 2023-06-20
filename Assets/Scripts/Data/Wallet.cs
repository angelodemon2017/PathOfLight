using System.Linq;
using Assets.Scripts.Data;

public static class Wallet
{
    public static int CurrentProfit;
    public static System.Action updateProfit;
    public static int IncrementByLevelCrystal => 1;//TODO UPGRADE

    public static void UpdateProfit()
    {
        CurrentProfit = Data.Instance.Board.Entities.Sum(x => ProfitByLevel(x.Level));
        updateProfit.Invoke();
    }

    public static int ProfitByLevel(int level) 
    {
        if (level == 0)
            return 0;

        int result = 1;

        for (int i = 1; i < level; i++)
        {
            result = result * 2 + IncrementByLevelCrystal;
        }

        return result;
    }
}