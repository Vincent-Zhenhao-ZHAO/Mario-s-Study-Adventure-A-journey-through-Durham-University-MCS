using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    // define the tile state, meaning the background color, text color for each number
    public TileState state { get; private set; }
    
    // define the cell
    public TileCell cell { get; private set; }

    // define the number
    public int number { get; private set; }
    
    // define the background
    private Image background;
    
    // define the text
    private TextMeshProUGUI text;

    // get the current background color and the text
    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // replace the current color and text to object state color and text.
    public void BuildState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();
    }
    
    public void CreatCell(TileCell cell)
    {
        // check if the cell is null
        if (this.cell != null) {
            this.cell.tile = null;
        }
        
        // set the object cell
        this.cell = cell;
        this.cell.tile = this;

        // transfer to the object cell
        transform.position = cell.transform.position;
    }

    public void MoveAnimate(TileCell cell)
    {
        // check if the cell is null
        if (this.cell != null) {
            this.cell.tile = null;
        }
        
        // set the object cell
        this.cell = cell;
        this.cell.tile = this;
        
        // transfer to the object cell
        StartCoroutine(TileAnimate(cell.transform.position, false));
    }

    // merge tiles
    public void MergeTiles(TileCell cell)
    {
        if (this.cell != null) {
            this.cell.tile = null;
        }

        this.cell = null;

        StartCoroutine(TileAnimate(cell.transform.position, true));
    }

    // animenation
    private IEnumerator TileAnimate(Vector3 to, bool merging)
    {
        float elapsed = 0f;
        float duration = 0.2f;

        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if (merging) {
            Destroy(gameObject);
        }
    }

}
