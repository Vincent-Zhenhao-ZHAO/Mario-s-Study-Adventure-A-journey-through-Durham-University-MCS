using UnityEngine;

// code base on https://github.com/zigurous/unity-2048-tutorial
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
