using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public Attack activeAttack;
    public float wager;
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
            HealthBar.localScale = new Vector3(.01f * base.Val * 4, 1f, 1f);
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
