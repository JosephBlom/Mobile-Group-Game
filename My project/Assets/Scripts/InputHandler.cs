using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] TextMeshProUGUI abilityText;

    private Camera _mainCamera;
    private GameObject selectedTower;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        selectedTower = rayHit.collider.gameObject;

        if (rayHit.collider.CompareTag("BasicTower"))
        {
            BulletManager bullet = GetBullet(rayHit.collider.gameObject);
            string nextAbility = getNextAbility(bullet);
            abilityText.text = "Unlock \n" + nextAbility;
            

            menuAnimator.SetBool("IsOpen", true);
        }
    }

    public void Close()
    {
        menuAnimator.SetBool("IsOpen", false);
    }

    private BulletManager GetBullet(GameObject selectedTower)
    {
        TowerBrain towerBrain = selectedTower.GetComponent<TowerBrain>();
        GameObject bullet = towerBrain.bulletPrefab;
        BulletManager selectedBullet = bullet.GetComponent<BulletManager>();
        return selectedBullet;
    }

    public void UnlockAbility()
    {
        BulletManager bullet = GetBullet(selectedTower);
        string nextAbility = getNextAbility(bullet);
        bullet.unlockedAbilities.Add(nextAbility);
    }

    public string getNextAbility(BulletManager bullet)
    {
        List<string> notUnlockedAbilites = new List<string>();
        foreach (string ability in bullet.possibleAbilities)
        {
            if (!bullet.unlockedAbilities.Contains(ability))
            {
                notUnlockedAbilites.Add(ability);
            }
        }
        if (notUnlockedAbilites.Count > 0)
        {
            return notUnlockedAbilites[0];
        }
        return "No New Abilties";
    }

}
