using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoulVendor : MonoBehaviour
{
    Interactable Interactable;

    [SerializeField]
    GameObject UpgradeUI;

    bool IsInMenu = false;

    private void Start()
    {
        Interactable = GetComponent<Interactable>();
        Interactable.OnEnterRange += PlayerEnterRange;
        Interactable.OnExitRange += PlayerExitRange;
        Interactable.OnInteract += PlayerInteract;
    }

    private void PlayerInteract(object sender, Interactable.OnInteractArgs e)
    { //send player through as an arg and freeze his movement
        Player player = e.Player;
        Debug.Log("Player Interacted!");
        if(!IsInMenu)
            OpenMenu(player);
        else 
            CloseMenu(player);
    }

    public void PlayerEnterRange(object sender, EventArgs e) {
        Debug.Log("Player in range!");
    }

    public void PlayerExitRange(object sender, EventArgs e) {
        Debug.Log("Player exited range!");
    }

    void OpenMenu(Player player) {
        if (!player) return;
        Time.timeScale = 0;
        UpgradeUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        IsInMenu = true;
        player.enabled = false;
    }

    void CloseMenu(Player player) {
        if (!player) return;
        Time.timeScale = 1;
        UpgradeUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        IsInMenu = false;
        player.enabled = true;
    }

}
