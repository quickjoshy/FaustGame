using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    int Cost;

    [SerializeField]
    string AttackName;

    [SerializeField]
    int UpgradeIndex;

    EventTrigger Trigger;

    public Player Player;

    private void Start()
    {
        Trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => OnPointerClick((PointerEventData)data));
        Trigger.triggers.Add(entry);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogFormat("{0} upgrade for {1}", AttackName, Cost);
        Attack attack = Player.GetAttackByName(AttackName);

        if (Player.Souls >= Cost)
        {
            Player.ChangeSouls(-Cost);
            attack.UpgradeFunctions[UpgradeIndex].Invoke();
        }
            

    }


}
