using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// code base on https://github.com/zigurous/unity-2048-tutorial
public class Board : MonoBehaviour
{
    public MiniGameManager MiniGameManager;
    
    // define the tile, note this need to be Preb and need has Tile script
    public Tile tilePrefab;
    
    // define the state 
    public TileState[] tileStates;

    // define the grid
    private TileGrid grid;
    
    // define tiles
    private List<Tile> tiles;

    private AudioSource _moveSounds;

    private void Start()
    {
        _moveSounds = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        // get the grid
        grid = GetComponentInChildren<TileGrid>();
        // build the tiles and set size as the same size as grid
        tiles = new List<Tile>(16);
    }

    // reset the board
    public void CleanBoard()
    {
        // for each tile make its null
        foreach (var cell in grid.cells) {
            cell.tile = null;
        }

        // Destroy each tile objet
        foreach (var tile in tiles) {
            Destroy(tile.gameObject);
        }
        
        // clean the list
        tiles.Clear();
    }

    // function to generate new tile
    public void GenerateNewTile()
    {
        // build new tile
        Tile tile = Instantiate(tilePrefab, grid.transform);
        
        // set possibility to get the number
        float possibility = Random.Range(0f, 1f);
        
        // get the number according to the possibility
        if (possibility < 0.3f)
        {
            tile.BuildState(tileStates[0],2);
        }

        else if (possibility < 0.5f)
        {
            tile.BuildState(tileStates[0],2);
        }
        else if (possibility < 0.7f)
        {
            tile.BuildState(tileStates[0],2);
        }
        else if (possibility < 0.8f)
        {
            tile.BuildState(tileStates[1],2);
        }
        else
        {
            tile.BuildState(tileStates[2],4);
        }
        
        // put the tile in the right position
        tile.CreatCell(grid.GenerateRandomCell());
        
        // update the tile list
        tiles.Add(tile);
    }

    // happen when user press the keyboard commend
    private void Update()
    {
        // when move up, start from top-bottom
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            _moveSounds.Play();
            MoveAll(Vector2Int.up, 0, 1, 1, 1);
            if (tiles.Count < grid.size)
            {
                GenerateNewTile();
            }
            MiniGameManager.DecreaseTime();
        } 
        
        // when move down, consider from top line, as bottom/top line not move , so minus one again
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            _moveSounds.Play();
            MoveAll(Vector2Int.left, 1, 1, 0, 1);
            if (tiles.Count < grid.size)
            {
                GenerateNewTile();
            }
            MiniGameManager.DecreaseTime();
        } 
        
        // same logic as move up. start from left-right
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            _moveSounds.Play();
            MoveAll(Vector2Int.down, 0, 1, grid.height - 2, -1);
            if (tiles.Count < grid.size)
            {
                GenerateNewTile();
            }
            MiniGameManager.DecreaseTime();
        } 
        // same logic as move down
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            _moveSounds.Play();
            MoveAll(Vector2Int.right, grid.width - 2, -1, 0, 1);
            if (tiles.Count < grid.size)
            {
                GenerateNewTile();
            }
            MiniGameManager.DecreaseTime();
        }

        // OverTime or the grid is full and not able to merge anymore
        if (CheckGameOver())
        {
            MiniGameManager.GameOver();
        }
    }

    // move all tiles, base on top-bottom, left-right
    private void MoveAll(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                // get each cell
                TileCell cell = grid.GetObjectCell(x, y);

                // check if full or empty
                if (cell.full) {
                    // if full then check neight and move single one
                    MoveSingle(cell.tile, direction);
                }
            }
        }
    }

    // move single one, check neighbour
    private void MoveSingle(Tile tile, Vector2Int direction)
    {
        // define new cell and find neibour
        TileCell newCell = null;
        TileCell neibour = grid.GetNeibourCell(tile.cell, direction);

        // find full neibour -> need to next to this neibour
        while (neibour != null)
        {
            // if neibour is full, meaning found it
            if (neibour.full)
            {
                // check if can merge
                if (MergeReady(tile, neibour.tile))
                {
                    Merge(tile, neibour.tile);
                }
                break;
            }

            // new cell will be neibour
            newCell = neibour;
            // get new neibour cell
            neibour = grid.GetNeibourCell(neibour, direction);
        }

        // if newcell not null, meaning there is movement happening
        if (newCell != null)
        {
            tile.MoveAnimate(newCell);
        }
    }

    // check if can merge, base on two number same or not
    private bool MergeReady(Tile a, Tile b)
    {
        return a.number == b.number;
    }
    
    // if able to merge, then merge
    // Need to remove the list element
    // and then change the state,
    // last destory element
    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.MergeTiles(b.cell);

        int index = Mathf.Clamp(FindIndex(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;

        b.BuildState(tileStates[index], number);
        MiniGameManager.AddScore(number);
    }

    // help to find the index of object state
    private int FindIndex(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i]) {
                return i;
            }
        }

        return -1;
    }

    // check if game over
    // - grid is full and not able to move
    // - OverTime
    public bool CheckGameOver()
    {
        if (tiles.Count != grid.size) {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetNeibourCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetNeibourCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetNeibourCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetNeibourCell(tile.cell, Vector2Int.right);

            if (up != null && MergeReady(tile, up.tile)) {
                return false;
            }

            if (down != null && MergeReady(tile, down.tile)) {
                return false;
            }

            if (left != null && MergeReady(tile, left.tile)) {
                return false;
            }

            if (right != null && MergeReady(tile, right.tile)) {
                return false;
            }
        }

        return true;
    }

}
