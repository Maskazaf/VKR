using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField] private Camera topDownCamera;
    [SerializeField] private GameObject navTargetObject;

    private NavMeshPath path;
    private LineRenderer line;
    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = GetComponent<LineRenderer>();

        // Убедимся, что линия выключена в начале
        if (line != null)
        {
            line.enabled = false;
        }
    }

    private void Update()
    {
        // При касании экрана включаем/выключаем маршрут
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            lineToggle = !lineToggle;

            // Если выключили — отключаем визуализацию
            if (!lineToggle && line != null)
            {
                line.enabled = false;
            }
        }

        // Если маршрут активен — обновляем путь и рисуем
        if (lineToggle && line != null && navTargetObject != null)
        {
            NavMesh.CalculatePath(
                transform.position,
                navTargetObject.transform.position,
                NavMesh.AllAreas,
                path
            );

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);
                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
        }
    }
}
