using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{/**/
    [System.Serializable]
    public class Data : MonoBehaviour
    {
        public static Data Instance;

        public Board Board = new Board();
        public PlayerWallet playerWallet = new PlayerWallet();
        public PowerUpsData powerUpsData = new PowerUpsData();

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
        {
            Board.Init();
            powerUpsData.Init();
        }
    }

    public struct PowerUpsData
    {
        public List<string> keys;

        public void Init()
        {
            keys = new List<string>();
        }
    }

    public struct PlayerWallet
    {
        public int Ether;
        public int Souls;

        public bool TrySpend(int cost) 
        {
            if (cost <= Ether)
            {
                Ether -= cost;
                return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public struct Board
    {
        public List<EntityOfBoard> Entities;

        public System.Action UpdateBoard;

        public bool TrySpawnRandomEntity() 
        {
            if (!Entities.Any(x => x.IsEmpty))
            {
                return false;
            }
            Entities.FirstOrDefault(x => x.IsEmpty).Upgrade();

            return true;
        }

        public void Init() 
        {
            Entities = new List<EntityOfBoard>();
            for (int i = 0; i < 9; i++)
            {
                var tempEOB = new EntityOfBoard();
                tempEOB.updateEntity += UpdateEnts;
                tempEOB.updateEntity += Wallet.UpdateProfit;
                Entities.Add(tempEOB);
            }
        }

        public void UpdateEnts()
        {
            UpdateBoard?.Invoke();
            RoadOfClean.instance.UpdatePowerLens();
        }
    }

    [System.Serializable]
    public class EntityOfBoard
    {
        [SerializeField]
        private int _Level;
        public int Level
        {
            get { return _Level; }
            set 
            {
                _Level = value;
                updateEntity?.Invoke();
            }
        }
        [SerializeField]
        private Element _MyElement;
        public Element MyElement
        {
            get { return _MyElement; }
            set
            {
                _MyElement = value;
                updateEntity?.Invoke();
            }
        }
        public System.Action updateEntity;

        public bool IsEmpty => Level == 0 && MyElement == Element.none;

        public void Clean() 
        {
            Level = 0;
            MyElement = Element.none;
        }

        public void Copy(EntityOfBoard eob) 
        {
            Level = eob.Level;
            MyElement = eob.MyElement;
        }

        public bool IsIdentity(EntityOfBoard eob) 
        {
            return eob.Level == Level && eob.MyElement == MyElement && Level < UpgradeModificators.MaxMerge;
        }

        public void Upgrade(bool randomElement = true)
        {
            if (randomElement)
            {
                MyElement = (Element)Random.Range(1, 4);
            }
            Level += 1;
            Wallet.UpdateProfit();
        }
    }
}