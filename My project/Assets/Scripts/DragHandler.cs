using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Variables for spawning towers")]
    [SerializeField] GameObject tower;
    [SerializeField] GameObject towerContainer;

    [SerializeField] Canvas canvas;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        towerContainer = GameObject.FindGameObjectWithTag("TowerContainer");
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.69f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Duck");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(gameObject);
        GameObject towerObject = Instantiate(tower, rectTransform.position, Quaternion.identity);
        towerObject.transform.SetParent(towerContainer.transform, true);
        towerObject.transform.position = Camera.main.ScreenToWorldPoint(rectTransform.position);
        towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
