using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public Transform CastingPoint;

    [SerializeField]
    Slider HealthSlider;

    [SerializeField]
    Image HealthImage;
    public int SoulReward = 0;
    protected virtual void Start()
    {
        if (CastingPoint == null) CastingPoint = transform;
        _health = MaxHealth;
    }

    [SerializeField]
    private float _health;
    public float Health {
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
            if (HealthSlider)
                HealthSlider.value = _health;
            else if (HealthImage)
                HealthImage.rectTransform.localScale = new Vector3(Health/MaxHealth, 1, 1);
        }
    }
    public float MaxHealth;

    


    protected virtual void Kill() {
        Player player = FindAnyObjectByType<Player>();
        if (player && gameObject.tag == "Enemy") player.OnEnemyKill(this);
        Destroy(gameObject);
    }

    // Update is called once per frame
}
