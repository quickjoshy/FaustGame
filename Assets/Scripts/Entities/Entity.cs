using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public Transform CastingPoint;

    [SerializeField]
    protected Image HealthImage;

    protected float healthImageWidth;
    protected float healthImageHeight;

    public int SoulReward = 0;

    protected virtual void Start()
    {
        if (CastingPoint == null) CastingPoint = transform;
        _health = MaxHealth;
        if (!HealthImage) return;
        healthImageWidth = HealthImage.rectTransform.rect.width;
        healthImageHeight = HealthImage.rectTransform.rect.height;
    }

    [SerializeField]
    private float _health;
    public virtual float Health {
        get {
            if (_health <= 0) {
                Kill();
                return 0;
            }
            return _health;
        }
        set {
            _health = value;
            if (value > MaxHealth)
                _health = MaxHealth;

            if(HealthImage)
            HealthImage.rectTransform.sizeDelta = new Vector2(GetPercentHp() * healthImageWidth, healthImageHeight);
        }
    }
    public float MaxHealth;


    public float GetPercentHp() {
        return Health / MaxHealth;
    }


    protected virtual void Kill() {
        Player player = FindAnyObjectByType<Player>();
        if (player && gameObject.tag == "Enemy") player.OnEnemyKill(this);
        Destroy(gameObject);
    }

    // Update is called once per frame
}
