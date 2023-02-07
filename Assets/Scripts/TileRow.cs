using UnityEngine;

public class TileRow : MonoBehaviour
{
    // define total cells
    public TileCell[] cells { get; private set; }

    // get total cells
    private void Awake()
    {
        cells = GetComponentsInChildren<TileCell>();
    }

}
