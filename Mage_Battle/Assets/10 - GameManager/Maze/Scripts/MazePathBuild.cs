using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePathBuild : MazePath
{
    protected readonly string COLUMN_ANCHOR = "ColumnAnchor";

    private GameObject pathFrmWrk = null;

    public MazePathBuild(MazeData mazeData)
    {
        pathFrmWrk = mazeData.mazePathFloorFw;
    }

    public override GameObject RenderPath(MazeCell mazeCell, Vector3 position)
    {
        GameObject tile = new Framework()
            .Blueprint(pathFrmWrk)
            .SetIsPreFab(false)
            .Assemble(CreateBase(), CENTER_ANCHOR)
            .Assemble(CreateNorthWall(mazeCell), NORTH_WALL_ANCHOR)
            .Assemble(CreateSouthWall(mazeCell), SOUTH_WALL_ANCHOR)
            .Assemble(CreateEastWall(mazeCell), EAST_WALL_ANCHOR)
            .Assemble(CreateWestWall(mazeCell), WEST_WALL_ANCHOR)
            .Position(position)
            .Build();

        return (tile);
    }
    protected abstract GameObject CreateBase();

    protected abstract GameObject CreateNorthWall(MazeCell mazeCell);
    protected abstract GameObject CreateSouthWall(MazeCell mazeCell);
    protected abstract GameObject CreateEastWall(MazeCell mazeCell);
    protected abstract GameObject CreateWestWall(MazeCell mazeCell);

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
