using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePathBlank : MazePath
{
    private readonly string CENTER_ANCHOR = "CenterAnchor";
    private readonly string NORTH_WALL_ANCHOR = "NorthAnchor";
    private readonly string SOUTH_WALL_ANCHOR = "SouthAnchor";
    private readonly string EAST_WALL_ANCHOR = "EastAnchor";
    private readonly string WEST_WALL_ANCHOR = "WestAnchor";

    private MazeData mazeData = null;

    private GameObject wallFrmWrk = null;
    private GameObject wallPreFab = null;
    private GameObject pathFrmWrk = null;

    private Framework framework = null;
    private GameObject tile = null;

    public MazePathBlank(MazeData mazeData, GameObject tile)
    {
        this.framework = new Framework();
        this.tile = tile;

        this.mazeData = mazeData;
        this.wallFrmWrk = mazeData.mazeWallFw;
        this.wallPreFab = mazeData.buildingColumnPreFab;
        this.pathFrmWrk = mazeData.mazePathFloorFw;
    }

    public override GameObject RenderPath(MazeCell mazeCell, Vector3 position)
    {
        int walls = CalculateWalls(mazeCell);

        GameObject northWall = (mazeCell.HasNorthWall() && ((walls & N) > 0)) ? CreateNorthSouthWall(framework) : null;
        GameObject southWall = (mazeCell.HasSouthWall() && ((walls & S) > 0)) ? CreateNorthSouthWall(framework) : null;
        GameObject eastWall = (mazeCell.HasEastWall() && ((walls & E) > 0)) ? CreateEastWestWall(framework) : null;
        GameObject westWall = (mazeCell.HasWestWall() && ((walls & W) > 0)) ? CreateEastWestWall(framework) : null;

        GameObject floor = Object.Instantiate(tile);

        GameObject path = framework
           .Blueprint(pathFrmWrk)
           .Assemble(floor, CENTER_ANCHOR)
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
