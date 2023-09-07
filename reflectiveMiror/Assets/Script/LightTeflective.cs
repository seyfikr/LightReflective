using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTeflective : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction;
    private LineRenderer linerender;
    private bool isOpen;

    GameObject Reflective;
    void Start()
    {
        isOpen = false;
        linerender = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isOpen)
        {
            linerender.positionCount = 2;
            linerender.SetPosition(0, position);
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Mirror"))
                {
                    Reflective = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LightTeflective>().StartRay(hit.point, temp);
                }
                linerender.SetPosition(1, hit.point);
            }
            else
            {
                if (Reflective)
                {
                    Reflective.GetComponent<LightTeflective>().CloseRay();
                }
                linerender.SetPosition(1, direction * 150);
            }
        }

    }
    public void StartRay(Vector3 pos, Vector3 dir)
    {
        isOpen = true;
        position = pos;
        direction = dir;
    }
    public void CloseRay()
    {
        isOpen = false;
        linerender.positionCount = 0;
    }
}
