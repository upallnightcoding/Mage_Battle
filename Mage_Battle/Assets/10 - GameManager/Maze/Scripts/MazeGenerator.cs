using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeGenerator 
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public IDictionary<MazeIndex, MazeCell> GetMaze() { return(maze); }

    private IDictionary<MazeIndex, MazeCell> maze;
    private Stack<MazeCell> mazeStack;
    private Stack<MazeCell> maxStack;
    private int maxCellCount = 0;
    private int cellCount = 0;
    private MazeCell startMazeCell;
    private MazeCell endMazeCell;
    //private List<MazeCell> mazeList;

    public MazeGenerator(int width, int height)
    {
        this.Width = width;
        this.Height = height;

        mazeStack = new Stack<MazeCell>();
        maxStack = new Stack<MazeCell>();
        maze = new Dictionary<MazeIndex, MazeCell>();

        Generate();
    }

    private void Generate()
    {
        BuildMazeDictionary();

        SetStartingCell();

        while (WalkMaze(PickAValidNeighbor(mazeStack.Peek())));

        SetMazePath();
    }

    public MazeCell GetMazeCell(int col, int row)
    {
        MazeCell mazeCell = null;

        if (!maze.TryGetValue(new MazeIndex(col, row), out mazeCell)) {
            mazeCell = null;
        } 

        return(mazeCell);
    }

    /**
     * GetStartMazeCell() - Returns the Maze Cell that represents the start
     * of the maze journey.
     */
    public MazeCell GetStartMazeCell()
    {
        return (startMazeCell);
    }

    /**
     * SetMazePath() - Determines the maze path.
     */
    private void SetMazePath()
    {
        int count = 0;
        MazeCell[] cellList = maxStack.ToArray();

        for (int i = 0; i < maxStack.Count; i++)
        {
            MazeCell mazeCell = cellList[i];
            count += 1;

            if (count == 1)
            {
                mazeCell.PathType = MazePathType.START;
                startMazeCell = mazeCell;
            }
            else if (count == maxStack.Count)
            {
                mazeCell.PathType = MazePathType.END;
                endMazeCell = mazeCell;
            }
            else
            {
                mazeCell.PathType = MazePathType.PATH;
            }

            if (i != maxStack.Count-1) 
            {
                int col = cellList[i + 1].Col - cellList[i].Col;
                int row = cellList[i + 1].Row - cellList[i].Row;

                if (col == 0)
                {
                    mazeCell.MazePathDir = (row == 1) ? MazePathDirection.NORTH : MazePathDirection.SOUTH;
                } else
                {
                    mazeCell.MazePathDir = (col == 1) ? MazePathDirection.EAST : MazePathDirection.WEST;
                }
            }
        }
    }

    private bool WalkMaze(MazeCell neighbor)
    {
        if (neighbor != null) 
        {
            neighbor.MarkAsVisited();
            cellCount += 1;
            mazeStack.Push(neighbor);

            if (mazeStack.Count > maxStack.Count)
            {
                maxStack.Clear();
                foreach (MazeCell mazeCell in mazeStack)
                {
                    maxStack.Push(mazeCell);
                }
            }
        } else {
            mazeStack.Pop();
        }

        return (cellCount < maxCellCount);
    }

    private MazeCell PickAValidNeighbor(MazeCell currentMazeCell)
    {
        Tuple<int, int>[] neighbors = {
            Tuple.Create( 0, -1),   // North
            Tuple.Create( 0,  1),   // South
            Tuple.Create( 1,  0),    // East
            Tuple.Create(-1,  0)     // West
        };

        List<MazeCell> validNeighborList = new List<MazeCell>();

        foreach (Tuple<int, int> neighbor in neighbors) 
        {
            int col = currentMazeCell.Col + neighbor.Item1;
            int row = currentMazeCell.Row + neighbor.Item2;

            MazeCell candidate = GetMazeCell(col, row);

            if (candidate != null && (candidate.IsUnVisited())) 
            {
                validNeighborList.Add(candidate);
            }
        }

        MazeCell validNeighbor = null;
        int nNeighbors = validNeighborList.Count;

        if (nNeighbors > 0) 
        {
            validNeighbor = validNeighborList[GetRandom(nNeighbors)];
            currentMazeCell.CollapseWall(validNeighbor);
            validNeighbor.UpdatePathCount();
            currentMazeCell.UpdatePathCount();
        }

        return(validNeighbor);
    }

    private void BuildMazeDictionary()
    {
        maxCellCount = Width * Height;

        for (int col = 0; col < Width; col++) 
        {
            for (int row = 0; row < Height; row++)
            {
                maze.Add(new MazeIndex(col, row), new MazeCell(col, row));
            }
        }
    }

    private void SetStartingCell()
    {
        MazeCell cell = GetMazeCell(GetRandom(Width), GetRandom(Height));

        if (cell != null) 
        {
            cell.MarkAsVisited();
            cellCount = 1;
            mazeStack.Push(cell);
        }
    }

    /**
    GetRandom() - Returns a random number from 0 to n-1.
    */
    private int GetRandom(int n) {
        return(UnityEngine.Random.Range(0, n));
    }
}
