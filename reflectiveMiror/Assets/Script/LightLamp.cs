using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLamp : MonoBehaviour
{
    [SerializeField] Transform lightStartPoint;
    private Vector3 direction;
    LineRenderer linerender;
    private GameObject Reflective;
    void Start()
    {
        
        direction = lightStartPoint.forward;
        linerender = gameObject.GetComponent<LineRenderer>();
        linerender.positionCount = 2;
        linerender.SetPosition(0, lightStartPoint.position);
    }
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(lightStartPoint.position, direction, out hit, Mathf.Infinity))
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
