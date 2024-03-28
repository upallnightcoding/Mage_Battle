using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathBasic : MazePath
{
    private readonly string CENTER_ANCHOR = "CenterAnchor";

    private readonly string NORTH_TILE_ANCHOR = "TileNorthAnchor";
    private readonly string SOUTH_TILE_ANCHOR = "TileSouthAnchor";
    private readonly string EAST_TILE_ANCHOR = "TileEastAnchor";
    private readonly string WEST_TILE_ANCHOR = "TileWestAnchor";

    private readonly string NORTH_WALL_ANCHOR = "NorthAnchor";
    private readonly string SOUTH_WALL_ANCHOR = "SouthAnchor";
    private readonly string EAST_WALL_ANCHOR = "EastAnchor";
    private readonly string WEST_WALL_ANCHOR = "WestAnchor";

    private readonly string NORTH_EAST_TILE_ANCHOR = "TileNorthEastAnchor";
    private readonly string NORTH_WEST_TILE_ANCHOR = "TileNorthWestAnchor";
    private readonly string SOUTH_EAST_TILE_ANCHOR = "TileSouthEastAnchor";
    private readonly string SOUTH_WEST_TILE_ANCHOR = "TileSouthWestAnchor";

    private GameObject[] tilePreFab = null;
    private GameObject pathFrmWrk = null;
    private GameObject wallFrmWrk = null;
    private GameObject wallPreFab = null;

    private Framework framework = null;

    public MazePathBasic(MazeData mazeData)
    {
        this.framework = new Framework();

        this.tilePreFab = mazeData.tileList;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.wallPreFab = mazeData.buildingColumnPreFab;
    }

    public override GameObject RenderPath(MazeCell mazeCell, Vector3 position)
    {
        int walls = CalculateWalls(mazeCell);

        GameObject northWall = (mazeCell.HasNorthWall() && ((walls & N) > 0)) ? CreateNorthSouthWall(framework) : null;
        GameObject southWall = (mazeCell.HasSouthWall() && ((walls & S) > 0)) ? CreateNorthSouthWall(framework) : null;
        GameObject eastWall = (mazeCell.HasEastWall() && ((walls & E) > 0)) ? CreateEastWestWall(framework) : null;
        GameObject westWall = (mazeCell.HasWestWall() && ((walls & W) > 0)) ? CreateEastWestWall(framework) : null;

        GameObject path = framework
            .Blueprint(pathFrmWrk)
            .Assemble(tilePreFab, CENTER_ANCHOR)
            .Assemble(tilePreFab, NORTH_TILE_ANCHOR)
            .Assemble(tilePreFab, SOUTH_TILE_ANCHOR)
            .Assemble(tilePreFab, EAST_TILE_ANCHOR)
            .Assemble(tilePreFab, WEST_TILE_ANCHOR)

            .Assemble(tilePreFab, NORTH_EAST_TILE_ANCHOR)
            .Assemble(tilePreFab, NORTH_WEST_TILE_ANCHOR)

            .Assemble(tilePreFab, SOUTH_EAST_TILE_ANCHOR)
            .Assemble(tilePreFab, SOUTH_WEST_TILE_ANCHOR)

            .Assemble(northWall, NORTH_WALL_ANCHOR)
            .Assemble(southWall, SOUTH_WALL_ANCHOR)
            .Assemble(eastWall, EAST_WALL_ANCHOR)
            .Assemble(westWall, WEST_WALL_ANCHOR)
            .Position(position)
            .Build();

        return (path);
    }

    private GameObject CreateNorthSouthWall(Framework framework)
    {
        GameObject wall = framework
            .Blueprint(wallFrmWrk)
            .Assemble(wallPreFab, "ColumnAnchor")
            .Build(new Vector3(0.0f, 0.0f, 0.0f));

        return (wall);
    }

    private GameObject CreateEastWestWall(Framework framework)
    {
        GameObject wall = framework
            .Blueprint(wallFrmWrk)
            .Assemble(wallPreFab, "ColumnAnchor")
            .Build(new Vector3(0.0f, 90.0f, 0.0f));

        return (wall);
    }
}