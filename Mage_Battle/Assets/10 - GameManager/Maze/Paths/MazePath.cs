using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePath 
{
    public abstract GameObject RenderPath();

    protected readonly string COLUMN_ANCHOR = "ColumnAnchor";

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
