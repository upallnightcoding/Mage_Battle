using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePathBuild : MazePath
{
    protected readonly string COLUMN_ANCHOR = "ColumnAnchor";

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
        return (((mazeCell.Row == 0) ? CreateSouthWall(mazeCell) : (mazeCell.HasSouthWall()) ? CreateSouthWall(mazeCell) : CreateSouthPath(mazeCell)));
    }

    private GameObject RenderEastPassage(MazeCell mazeCell)
    {
        return (mazeCell.HasEastWall() ? CreateEastWall(mazeCell) : CreateEastPath(mazeCell));
    }

    private GameObject RenderWestPassage(MazeCell mazeCell)
    {
        return (((mazeCell.Col == 0) ? CreateWestWall(mazeCell) : (mazeCell.HasWestWall()) ? CreateWestWall(mazeCell) : CreateWestPath(mazeCell)));
    }

    private GameObject RenderPassage(bool create, bool hasWall, GameObject wall, GameObject path)
    {
        return (create ? (hasWall ? wall : path) : null);
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

    protected readonly string CENTER_ANCHOR = "CenterAnchor";

    protected readonly string NORTH_TILE_ANCHOR = "TileNorthAnchor";
    protected readonly string SOUTH_TILE_ANCHOR = "TileSouthAnchor";
    protected readonly string EAST_TILE_ANCHOR = "TileEastAnchor";
    protected readonly string WEST_TILE_ANCHOR = "TileWestAnchor";

    protected readonly string NORTH_WALL_ANCHOR = "NorthAnchor";
    protected readonly string SOUTH_WALL_ANCHOR = "SouthAnchor";
    protected readonly string EAST_WALL_ANCHOR = "EastAnchor";
    protected readonly string WEST_WALL_ANCHOR = "WestAnchor";

    protected readonly string NORTH_EAST_TILE_ANCHOR = "TileNorthEastAnchor";
    protected readonly string NORTH_WEST_TILE_ANCHOR = "TileNorthWestAnchor";
    protected readonly string SOUTH_EAST_TILE_ANCHOR = "TileSouthEastAnchor";
    protected readonly string SOUTH_WEST_TILE_ANCHOR = "TileSouthWestAnchor";
}
