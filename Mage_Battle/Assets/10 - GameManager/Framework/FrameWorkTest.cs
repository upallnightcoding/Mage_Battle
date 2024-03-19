using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWorkTest : MonoBehaviour
{
    private readonly string CENTER_ANCHOR = "CenterAnchor";

    [Header("Framework")]
    public GameObject mazeWallFw;
    public GameObject mazePathFloorFw;

    [Header("PreFabs")]
    public GameObject buildingColumnPreFab;
    public GameObject buildingFloor01PreFab;

    private Framework framework = null;

    // Start is called before the first frame update
    void Start()
    {
        framework = new Framework();

        CreatePath(framework, new Vector3());
    }

    private void CreatePath(Framework framework, Vector3 position)
    {
        GameObject path = framework
            .Blueprint(mazePathFloorFw)
            .Assemble(buildingFloor01PreFab, CENTER_ANCHOR)
            .Position(position)
            .Build();
    }
}
