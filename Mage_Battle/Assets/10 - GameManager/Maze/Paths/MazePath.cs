using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePath 
{
    protected readonly uint N = MazeData.N;
    protected readonly uint S = MazeData.S;
    protected readonly uint E = MazeData.E;
    protected readonly uint W = MazeData.W;

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
