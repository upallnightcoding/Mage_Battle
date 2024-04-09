using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Framework 
{
    private GameObject model = null;

    private GameObject activeGo = null;

    public Framework Blueprint(GameObject framework)
    {
        model = Object.Instantiate<GameObject>(framework);

        return (this);
    }

    public Framework Assemble(GameObject[] additionList, string anchorName, float turn = 0.0f)
    {
        int selection = Random.Range(0, additionList.Length);

        return (Assemble(additionList[selection], anchorName, turn));
    }

    public Framework Assemble(GameObject prefab, string anchorName, float yRotate = 0.0f, bool create = true)
    {
        GameObject go = null;
        return (Assemble(prefab, anchorName, yRotate, create, out go));
    }

    public Framework Assemble(GameObject prefab, string anchorName, float yRotate, bool create, out GameObject go)
    {
        go = null;

        if ((prefab != null) && (create))
        {
            Transform anchors = model.transform.Find("Anchors");
            Transform anchor = anchors.Find(anchorName);

            if (IsAPreFab(prefab))
            {
                activeGo = Object.Instantiate(prefab, anchor);
                activeGo.transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotate, 0.0f));
            }
            else
            {
                prefab.transform.position = anchor.transform.position;
                prefab.transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotate, 0.0f));
                prefab.transform.parent = anchor;
                activeGo = prefab;
            }

            go = activeGo;
        }

        return (this);
    }

    public Framework Decorate(GameObject[] go, int count, float xRange, float zRange, float yRotate = 0.0f)
    {
        int choice = Random.Range(0, go.Length);

        Decorate(go[choice], count, xRange, zRange, yRotate);

        return (this);
    }

    public Framework Decorate(GameObject go, int count, float xRange, float zRange, float yRotate = 0.0f)
    {
        int ndecorations = Random.Range(0, count);
        for (int i = 0; i < count; i++)
        {
            float xDelta = Random.Range(-xRange / 2.0f, xRange / 2.0f);
            float zDelta = Random.Range(-zRange / 2.0f, zRange / 2.0f);

            Vector3 position = activeGo.transform.position;
            Vector3 deltaPos = new Vector3(position.x + xDelta, 0.0f, position.z + zDelta);
            float turn = Random.Range(-yRotate, yRotate);

            GameObject decore = Object.Instantiate(go, activeGo.transform);
            decore.transform.rotation = Quaternion.Euler(new Vector3(0.0f, turn, 0.0f));
            decore.transform.position = deltaPos;
        }

        return (this);
    }

    public Framework Parent(Transform transform)
    {
        model.transform.parent = transform;

        return (this);
    }

    public Framework Position(Vector3 position)
    {
        model.transform.position = position;

        return (this);
    }

    public Framework Rotate(Vector3 rotate)
    {
        model.transform.rotation = Quaternion.Euler(rotate);

        return (this);
    }

    public GameObject Build()
    {
        return (model);
    }

    private bool IsAPreFab(GameObject thing)
    {
        return (
            PrefabUtility.GetPrefabInstanceStatus(thing) != PrefabInstanceStatus.NotAPrefab
            || PrefabUtility.GetPrefabAssetType(thing) != PrefabAssetType.NotAPrefab
        );
    }

    public static GameObject PickFromList(GameObject[] fromList)
    {
        int selection = Random.Range(0, fromList.Length);

        return (fromList[selection]);
    }

    public static GameObject CreateObject(GameObject preFab, GameObject orientation)
    {
        return (Object.Instantiate(preFab, orientation.transform.position, orientation.transform.rotation));
    }

    public static GameObject CreateObject(GameObject[] preFab, Vector3 position, float rotation, GameObject parent = null)
    {
        return (CreateObject(PickFromList(preFab), position, rotation, parent));
    }

    public static GameObject CreateObject(GameObject preFab, Vector3 position, float rotation, GameObject parent = null)
    {
        GameObject go = null;

        if (parent != null)
        {
            //go = Object.Instantiate(preFab, position, Quaternion.identity, parent.transform);
            go = Object.Instantiate(preFab, parent.transform);
            go.transform.parent = parent.transform;
            go.transform.Rotate(new Vector3(0.0f, rotation, 0.0f));
            go.transform.localPosition = position;
        }
        else
        {
            go = Object.Instantiate(preFab, position, Quaternion.identity);
            go.transform.Rotate(new Vector3(0.0f, rotation, 0.0f));
        }

        return (go);
    }

    public GameObject Build(Vector3 rotate)
    {
        model.transform.rotation = Quaternion.Euler(rotate);

        return (model);
    }


    public static float Rotate90Degree() => (90.0f * Random.Range(0, 4));

    public static float Rotate180Degree() => (180.0f * Random.Range(0, 2));




    /*private GameObject model = null;

    public Framework Blueprint(GameObject framework) 
    {
        model = Object.Instantiate<GameObject>(framework);

        return(this);
    }

    public Framework Assemble(GameObject[] additionList, string anchorName)
    {
        int selection = Random.Range(0, additionList.Length);

        return(Assemble(additionList[selection], anchorName));
    }

    public Framework Assemble(GameObject addition, string anchorName, bool create = true)
    {
        if ((addition != null) && (create))
        {
            Transform anchors = model.transform.Find("Anchors");
            Transform anchor = anchors.Find(anchorName);
            
            GameObject newPart = null;

            if (IsAPreFab(addition))
            {
                newPart = Object.Instantiate(addition, anchor);
            } else {
                addition.transform.position = anchor.transform.position;
                addition.transform.parent = anchor;
            }
        }

        return(this);
    }

    public Framework Parent(Transform transform)
    {
        model.transform.parent = transform;

        return(this);
    }

    public Framework Position(Vector3 position)
    {
        model.transform.position = position;

        return(this);
    }

    public GameObject Build()
    {
        return(model);
    }

    public GameObject Build(Vector3 rotate)
    {
        model.transform.rotation = Quaternion.Euler(rotate);

        return(model);
    }

    private bool IsAPreFab(GameObject thing) 
    {
        return(
            PrefabUtility.GetPrefabInstanceStatus(thing) != PrefabInstanceStatus.NotAPrefab 
            || PrefabUtility.GetPrefabAssetType(thing) != PrefabAssetType.NotAPrefab
        );
    }*/
}
