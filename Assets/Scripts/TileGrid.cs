using UnityEngine;

public class TileGrid : MonoBehaviour
{
    // define total row
    public TileRow[] rows { get; private set; }
    
    // define total cells
    public TileCell[] cells { get; private set; }

    // define the total number of grid. eg: 16,32 etc
    public int size => cells.Length;
    
    // define the height of the grid.
    public int height => rows.Length;
    
    // define the width of the grid
    public int width => size / height;

    // get the total row and cells
    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    // get each cell's position
    private void Start()
    {
        // for each row -> vertical
        for (int i = 0; i < rows.Length; i++)
        {
            // for each cell - > horizontal
            for (int j = 0; j < rows[i].cells.Length; j++)
            {
                // coordinate is [cell,row] -> [y,x]
                rows[i].cells[j].coordinates = new Vector2Int(j, i);
            }
        }
    }
    
    // get cell according to the coordinates
    public TileCell GetObjectCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height) {
            return rows[y].cells[x];
        } else {
            return null;
        }
    }

    // get the neibour cell
    public TileCell GetNeibourCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetObjectCell(coordinates.x, coordinates.y);
    }

    // Generate new cell
    public TileCell GenerateRandomCell()
    {
        // generate random cell index
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;
        
        // check if the cell empty, if empty then found
        if (cells[index].nothing)
        {
            return cells[index];
        }
        
        // check if the cell full, if null need to keep finding
        while (cells[index].full)
        {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            // back to beginning, meaning searched everything
            if (index == startingIndex) {
                return null;
            }
        }
        // return if not full
        return cells[index];
    }

}
