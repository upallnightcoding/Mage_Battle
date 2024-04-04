using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazePath 
{
    public abstract GameObject RenderPath(MazeCell mazeCell, Vector3 position);
}
