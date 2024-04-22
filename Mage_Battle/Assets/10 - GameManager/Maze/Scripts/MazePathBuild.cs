using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePathBuild : MazePath
{
    private MazeCell mazeCell = null;

    private GameObject pathFrmWrk = null;

    private Vector3 position;

    public MazePathBuild(MazeData mazeData, MazeCell mazeCell, Vector3 position)
    {
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.mazeCell = mazeCell;
        this.position = position;
    }

    protected GameObject GetPathFrmWrk() => pathFrmWrk;
    protected MazeCell GetMazeCell() => mazeCell;

    public override GameObject RenderPath()
    {
        GameObject tile = new Framework()
            .Blueprint(pathFrmWrk)
            .SetIsPreFab(false)
            .Assemble(CreateBase(), CENTER_ANCHOR)
            .Assemble(RenderNorthPassage(mazeCell), NORTH_WALL_ANCHOR)
            .Assemble(RenderSouthPassage(mazeCell), SOUTH_WALL_ANCHOR)
            .Assemble(RenderEastPassage(mazeCell), EAST_WALL_ANCHOR)
            .Assemble(RenderWestPassage(mazeCell), WEST_WALL_ANCHOR)
            .Position(position + GetOffSet())
            .Build();

        return (tile);
    }

    protected virtual Vector3 GetOffSet()
    {
        return (new Vector3());
    }

    private GameObject RenderNorthPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasNorthWall() ? CreateNorthWall(mazeCell) : CreateNorthPath(mazeCell));
    }

    private GameObject RenderSouthPassage(MazeCell mazeCell)
    {
        return ((mazeCell.Row == 0) ? CreateSouthWall(mazeCell) : null);
        //return (((mazeCell.Row == 0) ? CreateSouthWall(mazeCell) : (mazeCell.HasSouthWall()) ? CreateSouthWall(mazeCell) : CreateSouthPath(mazeCell)));

    }

    private GameObject RenderEastPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasEastWall() ? CreateEastWall(mazeCell) : CreateEastPath(mazeCell));
    }

    private GameObject RenderWestPassage(MazeCell mazeCell)
    {
        return ((mazeCell.Col == 0) ? CreateWestWall(mazeCell) : null);
        //return (((mazeCell.Col == 0) ? CreateWestWall(mazeCell) : (mazeCell.HasWestWall()) ? CreateWestWall(mazeCell) : CreateWestPath(mazeCell)));
    }

    protected abstract GameObject CreateBase();

    protected abstract GameObject CreateNorthWall(MazeCell mazeCell);
    protected abstract GameObject CreateSouthWall(MazeCell mazeCell);
    protected abstract GameObject CreateEastWall(MazeCell mazeCell);
    protected abstract GameObject CreateWestWall(MazeCell mazeCell);

    protected abstract GameObject CreateNorthPath(MazeCell mazeCell);
    protected abstract GameObject CreateSouthPath(MazeCell mazeCell);
    protected abstract GameObject CreateEastPath(MazeCell mazeCell);
    protected abstract GameObject CreateWestPath(MazeCell mazeCell);

   
}
