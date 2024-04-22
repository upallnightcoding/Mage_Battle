using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell 
{
    public static readonly int N = 1;
    public static readonly int S = 2;
    public static readonly int E = 4;
    public static readonly int W = 8;

    // Type of Cell
    public MazePathType PathType { get; set; }  = MazePathType.OFFPATH;
    public MazePathDirection MazePathDir { get; set; } = MazePathDirection.NONE;

    // References to north, south, east and west walls of a cell
    public MazeCell NorthWall   { get; private set; } = null;
    public MazeCell SouthWall   { get; private set; } = null;
    public MazeCell EastWall    { get; private set; } = null;
    public MazeCell WestWall    { get; private set; } = null;

    // Cell Column and Row position
    public int Col { get; private set; }
    public int Row { get; private set; }

    public Vector3 Position     { get; set; }

    public int PathValue { get; set; } = 0;
    private void AddNorthPath() { PathValue += N; }
    private void AddSouthPath() { PathValue += S; }
    private void AddWestPath() { PathValue += W; }
    private void AddEastPath() { PathValue += E; }

    public bool IsRoom() => (PathCount == 1);
    public int PathCount { get; set; } = 0;
    public void UpdatePathCount() => PathCount++;

    public void MarkAsVisited() => type = MazeCellType.VISITED; 

    public bool IsUnVisited()   => (type == MazeCellType.UNVISITED); 
    public bool IsVisited()     => (type == MazeCellType.VISITED);

    // Predicate functions that returns true if a wall exists
    public bool HasNorthWall()  => NorthWall == null; 
    public bool HasSouthWall()  => SouthWall == null; 
    public bool HasEastWall()   => EastWall == null; 
    public bool HasWestWall()   => WestWall == null;

    public bool HasNorthPath() => NorthWall != null;
    public bool HasSouthPath() => SouthWall != null;
    public bool HasEastPath() => EastWall != null;
    public bool HasWestPath() => WestWall != null;

    private MazeCellType type = MazeCellType.UNVISITED;

    public MazeCell(int col, int row) 
    {
        this.Col = col;
        this.Row = row;
    }

    public List<MazeCell> ListFreeNeighbor()
    {
        List<MazeCell> freeList = new List<MazeCell>(); 

        if (!HasNorthWall()) freeList.Add(NorthWall);
        if (!HasSouthWall()) freeList.Add(SouthWall);
        if (!HasEastWall()) freeList.Add(EastWall);
        if (!HasWestWall()) freeList.Add(WestWall);

        return(freeList);
    }

    public void CollapseWall(MazeCell neighbor)
    {
        int col = neighbor.Col - Col;
        int row = neighbor.Row - Row;

        if (col == 0) 
        {
            if (row == 1) 
            {
                NorthWall = neighbor;
                neighbor.SouthWall = this;
                AddNorthPath();
                neighbor.AddSouthPath();
            } else {
                SouthWall = neighbor;
                neighbor.NorthWall = this;
                AddSouthPath();
                neighbor.AddNorthPath();
            }
        }

        if (row == 0)
        {
            if (col == 1) 
            {
                EastWall = neighbor;
                neighbor.WestWall = this;
                AddEastPath();
                neighbor.AddWestPath();
            } else {
                WestWall = neighbor;
                neighbor.EastWall = this;
                AddWestPath();
                neighbor.AddEastPath();
            }
        }
    }
}

public enum MazeCellType
{
    UNVISITED,
    VISITED,
    ARENA
}

public enum MazePathType
{
    START, 
    PATH, 
    END,
    OFFPATH
}

public enum MazePathDirection
{
    NONE,
    NORTH,
    SOUTH,
    EAST,
    WEST
}