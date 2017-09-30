using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
// using UnityEditor;

public class main : MonoBehaviour
{

    // Use this for initialization
    UnityEngine.UI.RawImage Image;
    public Texture Texture;
    private UnityEngine.Object[] CubeList;
    private int CurrentIndex = 0;
    private GameObject CurrentCube;
    void Start()
    {
        CubeList = Resources.LoadAll("", typeof(GameObject));
        Image = GameObject.Find("RawImage").GetComponent<UnityEngine.UI.RawImage>();
        CurrentCube = Instantiate(Resources.Load(CubeList[CurrentIndex].name, typeof(GameObject)) as GameObject);
        CurrentCube.AddComponent<CUBE>();

        Texture = Resources.Load("Icons/" + CubeList[CurrentIndex].name, typeof(Texture)) as Texture;
        Image.texture = Texture;

    }

    // Update is called once per frame
    void Update()
    {
        MakeCube();
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void MakeCube()
    {
        if (IsPointerOverUIObject()) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            CurrentCube.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0)) Instantiate(CurrentCube).AddComponent<BoxCollider>();
            if (Input.GetMouseButtonDown(1)) if (hit.transform.GetComponent<CUBE>() != null) Destroy(hit.transform.gameObject);

        }
    }

    public void NextCube()
    {
        Destroy(CurrentCube);
        CurrentIndex = CurrentIndex == CubeList.Length - 1 ? 0 : CurrentIndex + 1;
        Texture = Resources.Load("Icons/" + CubeList[CurrentIndex].name, typeof(Texture)) as Texture;
        Image.texture = Texture;
        CurrentCube = Instantiate(Resources.Load(CubeList[CurrentIndex].name, typeof(GameObject)) as GameObject);
        CurrentCube.AddComponent<CUBE>();

    }
}
