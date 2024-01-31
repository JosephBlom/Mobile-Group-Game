using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        if (rayHit.collider.CompareTag("BasicTower"))
        {
            menuAnimator.SetBool("IsOpen", true);
        }
    }

    public void Close()
    {
        menuAnimator.SetBool("IsOpen", false);
    }

}
