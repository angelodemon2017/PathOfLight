using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;

    public CellOfField CellPrefab;
    public Transform BoardPlace;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(CellPrefab, BoardPlace).Init(i);
        }
    }

    void Update()
    {
        
    }
}