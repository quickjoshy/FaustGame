using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public bool PlayerInRange;
    public event EventHandler OnEnterRange;
    public event EventHandler OnExitRange;
    public event EventHandler<OnInteractArgs> OnInteract;
    public InputAction InteractButton;

    [SerializeField]
    Player player;

    public class OnInteractArgs : EventArgs { 
        public Player Player;
    }

    private void Start()
    {
        InteractButton = InputSystem.actions.FindAction("Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player) {
            PlayerInRange = true;
            OnEnterRange(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
        if (player) {
            PlayerInRange = false;
        }
    }

    private void Update()
    {
        if (!PlayerInRange) return;

        else if (InteractButton.WasCompletedThisFrame()) {
            OnInteract(this, new OnInteractArgs { Player = player });
        }
    }

}
