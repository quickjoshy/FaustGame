using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Attack activeAttack;
    public float wager;

    [SerializeField]
    Slider HealthSlider;
    // Start is called before the first frame update
    public override float Val
    {
        get
        {
            //HealthBar.localScale = new Vector3(.1f * base.Val, 1f, 1f);
            return base.Val;
        }
        set
        {
            base.Val = value;
            if (base.Val > Max) base.Val = Max;
            HealthSlider.value = value;
            if (base.Val <= 0)
                Kill();
        }
    }



    public void UpdateCostGraphic(Attack activeAttack, float wager) {
        this.activeAttack = activeAttack;
    }

    protected override void Kill() {
        SceneManager.LoadScene("MainMenu");
    }
}
