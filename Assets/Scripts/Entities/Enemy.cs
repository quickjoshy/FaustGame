using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Attack[] Attacks { get; set; }

    Entity Entity { get; set; }

    public void AttackPlayer();

    public void Die();

}
