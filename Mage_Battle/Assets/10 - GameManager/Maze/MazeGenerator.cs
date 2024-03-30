using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeGenerator 
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public IDictionary<MazeIndex, MazeCell> GetMaze() { return(maze); }

    private bool MazeStackEmpty() => (mazeStack.Count == 0);

    private IDictionary<MazeIndex, MazeCell> maze;
    private Stack<MazeCell> mazeStack;
    private Stack<MazeCell> maxStack;
    private int maxCellCount = 0;
    private int cellCount = 0;
    //private List<MazeCell> mazeList;

    public MazeGenerator(int width, int height)
    {
        this.Width = width;
        this.Height = height;

        mazeStack = new Stack<MazeCell>();
        maxStack = new Stack<MazeCell>();
        maze = new Dictionary<MazeIndex, MazeCell>();
    }

    public void Generate()
    {
        InitMazeGenerator();

        while (WalkMaze(PickAValidNeighbor(mazeStack.Peek())));

        SetMazePath();

        /*while (!MazeStackEmpty()) 
        {
            WalkMaze(PickAValidNeighbor(mazeStack.Peek()));
        }*/

        //mazeList = new List<MazeCell>(maze.Values);
    }

    public MazeCell GetMazeCell(int col, int row)
    {
        MazeCell mazeCell = null;

        if (!maze.TryGetValue(new MazeIndex(col, row), out mazeCell)) {
            mazeCell = null;
        } 

        return(mazeCell);
    }

    private void SetMazePath()
    {
        int count = 0;

        foreach (MazeCell mazeCell in maxStack)
        {
            count += 1;

            if (count == 1)
            {
                mazeCell.PathType = MazePathType.START;
            }
            else if (count == maxStack.Count)
            {
                mazeCell.PathType = MazePathType.END;
            }
            else
            {
                mazeCell.PathType = MazePathType.PATH;
            }
        }
    }

    /*public MazeCell PickRandomCell()
    {
        return(mazeList[UnityEngine.Random.Range(0, mazeList.Count)]);
    }*/

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
            Tuple.Create(0, -1),    // North
            Tuple.Create(0, 1),     // South
            Tuple.Create(1, 0),     // East
            Tuple.Create(-1, 0)     // West
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
        }

        return(validNeighbor);
    }

    private void InitMazeGenerator()
    {
        BuildMazeDictionary();

        SetStartingCell();
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

        //GetMazeCell(3, 3).MarkAsVisited();
        //GetMazeCell(4, 3).MarkAsVisited();
        //GetMazeCell(4, 4).MarkAsVisited();
        //GetMazeCell(3, 4).MarkAsVisited();
    }

    /**
    GetRandom() - Returns a random number from 0 to n-1.
    */
    private int GetRandom(int n) {
        return(UnityEngine.Random.Range(0, n));
    }
}
