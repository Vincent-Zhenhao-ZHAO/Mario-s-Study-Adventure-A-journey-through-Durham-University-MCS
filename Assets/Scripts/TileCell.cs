using UnityEngine;

public class TileCell : MonoBehaviour
{
    // define the position of each cell
    public Vector2Int coordinates { get; set; }
    
    // define each tile
    public Tile tile { get; set; }

    // define if the tile empty or not
    public bool nothing => tile == null;
    public bool full => tile != null;
}
