using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCntrl : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;

    private readonly uint NW = MazeData.NW;
    private readonly uint SW = MazeData.SW;
    private readonly uint SE = MazeData.SE;
    private readonly uint NE = MazeData.NE;

    private readonly uint N = MazeData.N;
    private readonly uint S = MazeData.S;
    private readonly uint E = MazeData.E;
    private readonly uint W = MazeData.W;

    private Framework framework = null;

    // Start is called before the first frame update
    void Start()
    {
        framework = new Framework();

        Debug.Log("Maze has been created ...");
        MazeGenerator maze = mazeData.GenerateMaze();

        Display(maze);
    }

    private void Display(MazeGenerator maze)
    {
        Vector3 position = Vector3.zero;
        float cellSize = mazeData.cellSize;
        GameObject parent = new GameObject("World");

        for (int row = 0; row < maze.Height; row++) 
        {
            for (int col = 0; col < maze.Width; col++)
            {
                GameObject path = CreateMazePath(maze, col, row, position);
                if (path != null)
                {
                    path.transform.SetParent(parent.transform);
                }

                position.x += cellSize;
            }

            position.x = 0.0f;
            position.z += cellSize;
        }
    }

    private GameObject CreateMazePath(MazeGenerator maze, int col, int row, Vector3 position) 
    {
        MazeCell mazeCell = maze.GetMazeCell(col, row);
        GameObject path = null;

        if ((mazeCell != null) && (mazeCell.IsVisited()))
        {
            Tuple<uint, uint> colsAndwalls = ColumnsAndWalls(col, row);
            uint columns = colsAndwalls.Item1;
            uint walls = colsAndwalls.Item2;

            path = mazeData.CreatePath(framework, mazeCell, position, columns, walls);
        }

        return (path);
    }

    private Tuple<uint, uint> ColumnsAndWalls(int col, int row) 
    {
        uint walls = 0, columns = 0;

        if (row == 0) 
        {
            if (col == 0) 
            {
                columns = NW+NE+SW+SE;
                walls = N+S+E+W;
            } else {
                columns = NE+SE;
                walls = N+S+E;
            }
        } else {
            if (col == 0)
            {
                columns = NW+NE;
                walls = N+E+W;
            } else {
                columns = NE;
                walls = N+E;
            }
        }

        return(new Tuple<uint, uint>(columns, walls));
    }
}
