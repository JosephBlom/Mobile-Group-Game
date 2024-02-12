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
    public GameObject selectedTower;
    private TowerBrain towerBrain;
    private string nextAbility;

    Vector3 worldPosition;
    Vector2 screenPosition;

    Coins playerBank;

    private void Awake()
    {
        playerBank = FindObjectOfType<Coins>();
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if(Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
            worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if(hit.collider != null && hit.collider.CompareTag("Tower"))
            {
                selectedTower = hit.collider.gameObject; 
                towerBrain = hit.collider.gameObject.GetComponent<TowerBrain>();
                menuAnimator.SetBool("IsOpen", true);
                OpenMenu();
            }
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;


        if (rayHit.collider.CompareTag("Tower"))
        {
            selectedTower = rayHit.collider.gameObject;
            towerBrain = rayHit.collider.gameObject.GetComponent<TowerBrain>();
            menuAnimator.SetBool("IsOpen", true);
            OpenMenu();
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
        if(playerBank.coinTotal >= (towerBrain.unlockedAbilities.Count * 200))
        {
            nextAbility = getNextAbility(towerBrain);
            towerBrain.unlockedAbilities.Add(nextAbility);
            nextAbility = getNextAbility(towerBrain);
            OpenMenu();
        }
        else
        {
            Debug.Log("This Upgrade is Too Expensive!");
        }
    }

    public void IncreaseDamage()
    {
        if(playerBank.coinTotal >= (towerBrain.damageLvl * 100))
        {
            towerBrain.damageMult += 0.3f;
            towerBrain.damageLvl++;
            OpenMenu();
        }
        else
        {
            Debug.Log("This Upgrade is Too Expensive!");
        }
        
    }

    public void IncreaseAttackSpeed()
    {
        if(playerBank.coinTotal >= (towerBrain.attackSpeedLvl * 100))
        {
            towerBrain.attackSpeed += 0.3f;
            towerBrain.attackSpeedLvl++;
            OpenMenu();
        }
        else
        {
            Debug.Log("This Upgrade is Too Expensive!");
        } 
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
