using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] TextMeshProUGUI abilityText;
    [SerializeField] Button abilityButton;
    [SerializeField] Button damageButton;
    [SerializeField] Button attackSpeedButton;

    private Camera _mainCamera;
    private GameObject selectedTower;
    private TowerBrain towerBrain;
    private string nextAbility;

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
        towerBrain = rayHit.collider.gameObject.GetComponent<TowerBrain>();

        if (rayHit.collider.CompareTag("BasicTower"))
        {
            OpenMenu();
            menuAnimator.SetBool("IsOpen", true);
        }
    }

    public void Close()
    {
        menuAnimator.SetBool("IsOpen", false);
        abilityButton.interactable = true;
        damageButton.interactable = true;
        attackSpeedButton.interactable = true;
    }

    public void UnlockAbility()
    {
        nextAbility = getNextAbility(towerBrain);
        towerBrain.unlockedAbilities.Add(nextAbility);
        nextAbility = getNextAbility(towerBrain);
        OpenMenu();
    }

    public void IncreaseDamage()
    {
        towerBrain.damageMult += 0.3f;
        towerBrain.damageLvl++;
        OpenMenu();
    }

    public void IncreaseAttackSpeed()
    {
        towerBrain.attackSpeed += 0.3f;
        towerBrain.attackSpeedLvl++;
        OpenMenu();
    }

    private void OpenMenu()
    {
        nextAbility = getNextAbility(towerBrain);
        if (!(nextAbility == "Max"))
        {
            abilityText.text = "Unlock \n" + nextAbility;
        }
        else
        {
            abilityButton.interactable = false;
            abilityText.text = nextAbility;
        }

        if(towerBrain.attackSpeedLvl >= 3)
        {
            attackSpeedButton.interactable = false;
        }

        if (towerBrain.damageLvl >= 3)
        {
            damageButton.interactable = false;
        }
    }

    public string getNextAbility(TowerBrain tower)
    {
        List<string> notUnlockedAbilites = new List<string>();
        foreach (string ability in tower.possibleAbilities)
        {
            if (!tower.unlockedAbilities.Contains(ability))
            {
                notUnlockedAbilites.Add(ability);
            }
        }
        if (notUnlockedAbilites.Count > 0)
        {
            return notUnlockedAbilites[0];
        }
        return "Max";
    }

}
