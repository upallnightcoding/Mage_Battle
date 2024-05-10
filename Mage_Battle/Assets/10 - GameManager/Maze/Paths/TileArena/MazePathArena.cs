using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathArena : MazePath3x3
{
    private readonly GameObject tile = null;

    private int col = 0, row = 0, nCol = 0, nRow = 0;

    public MazePathArena(MazeData mazeData, MazeCell mazeCell, Vector3 position, int col, int row, int nCol, int nRow) 
        : base(mazeData, mazeCell, position)
    {
        tile = mazeData.mazeArenaPathPrefab;

        this.col = col;
        this.row = row;
        this.nCol = nCol;
        this.nRow = nRow;
    }

    protected override GameObject CreateBase()
    {
        GameObject arenaTile = new Framework()
            .Blueprint(GetPathFrmWrk())
            .Assemble(tile, CENTER_ANCHOR)
            .Build();

        return (arenaTile);
    }

    protected override GameObject CreateNorthWall(MazeCell mazeCell)
    {
        return ((mazeCell.Row == row+nRow-1) ? CreateNorthSouthWall(wallPreFab, 0.0f) : null);
    }

    protected override GameObject CreateEastWall(MazeCell mazeCell)
    {
        return ((mazeCell.Col == col+nCol-1) ? CreateEastWestWall(wallPreFab, 90.0f) : null);
    }

}
