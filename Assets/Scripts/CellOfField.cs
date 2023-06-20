using Assets.Scripts.Data;
using Assets.Scripts.StaticDictionaries;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CellOfField : MonoBehaviour, IBeginDragHandler, IDropHandler, IDragHandler, IEndDragHandler
{
    public int CellId;
    public Image CellImage;
    public TextMeshProUGUI TextLevel;

    private EntityOfBoard eob => Data.Instance.Board.Entities[CellId];
    private Element myElement => eob.MyElement;
    private bool IsEmpty => eob.IsEmpty;

    public void Init(int id) 
    {
        CellId = id;
        eob.updateEntity += UpdateCell;
        UpdateCell();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsEmpty && GameProcess.Instance.TempCell.IsEmpty) 
        {
            GameProcess.Instance.TempCellId = CellId;
            GameProcess.Instance.TempCell.Copy(eob);
            CleanCell();
            GameProcess.Instance.ActivateTemp();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameProcess.Instance.TempCell.IsEmpty) 
        {
            if (GameProcess.Instance.TempCell.IsIdentity(eob)) 
            {
                eob.Upgrade();
            }
            else
            {
                Data.Instance.Board.Entities[GameProcess.Instance.TempCellId].Copy(eob);
                eob.Copy(GameProcess.Instance.TempCell);
            }
            GameProcess.Instance.DeativateTemp();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }


    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void UpdateCell() 
    {
        CellImage.color = ColorElement.ColorByElement[myElement];
        TextLevel.text = $"{eob.Level}";
    }

    private void CleanCell() 
    {
        eob.Clean();
    }
}