using UnityEngine;

// code base on https://github.com/zigurous/unity-2048-tutorial
[CreateAssetMenu(menuName = "Tile State")]
public class TileState : ScriptableObject
{
    // define the background color
    public Color backgroundColor;
    // define the text color
    public Color textColor;
}
