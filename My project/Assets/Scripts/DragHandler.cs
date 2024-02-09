using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [Header("Variables for spawning towers")]
    [SerializeField] GameObject tower;
    [SerializeField] GameObject towerContainer;

    [SerializeField] Canvas canvas;

    //private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Camera _mainCamera;
    private bool canPlace = false;


    private void Awake()
    {
        _mainCamera = Camera.main;
        towerContainer = GameObject.FindGameObjectWithTag("TowerContainer");
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canPlace)
        {
            Destroy(gameObject);
            GameObject towerObject = Instantiate(tower, rectTransform.position, Quaternion.identity);
            towerObject.transform.SetParent(towerContainer.transform, true);
            towerObject.transform.position = Camera.main.ScreenToWorldPoint(rectTransform.position);
            towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
        }
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
        if (!rayHit.collider) return;
        if (rayHit.collider.CompareTag("CanPlace"))
        {
            canPlace = true;
        }
        else
        {
            canPlace = false;
        }
    }
}
