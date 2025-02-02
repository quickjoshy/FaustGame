using UnityEngine;
using UnityEngine.UI;

public class CostDisplay : MonoBehaviour
{
    [SerializeField]
    Slider CostSlider;

    private void Start()
    {
        CostSlider = GetComponent<Slider>();
    }

    public void UpdateCostGraphic(Attack attack, int Wager) {
        float totalCost = attack.DefaultCost * Wager;
        CostSlider.value = totalCost;
    }
}
