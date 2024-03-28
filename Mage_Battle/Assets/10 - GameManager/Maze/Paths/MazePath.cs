using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePath 
{
    protected readonly uint N = MazeData.N;
    protected readonly uint S = MazeData.S;
    protected readonly uint E = MazeData.E;
    protected readonly uint W = MazeData.W;

    public abstract GameObject RenderPath(MazeCell mazeCell, Vector3 position);

    protected int CalculateWalls(MazeCell mazeCell)
    {
        int row = mazeCell.Row;
        int col = mazeCell.Col;

        uint walls = 0;

        if (row == 0)
        {
            if (col == 0)
            {
                walls = N + S + E + W;
            }
            else
            {
                walls = N + S + E;
            }
        }
        else
        {
            if (col == 0)
            {
                walls = N + E + W;
            }
            else
            {
                walls = N + E;
            }
        }

        return ((int) walls);
    }
}
