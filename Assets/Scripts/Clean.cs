using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clean : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 StartingScale;
    float rate = .1f;

    [SerializeField]
    float cooldown = 5;
    IEnumerator clean() {
        float timer = 0f;

        while (timer < cooldown - rate) {
            transform.localScale -= StartingScale * (rate/cooldown);
            timer += rate;
            yield return new WaitForSeconds(rate);
        }
        Destroy(gameObject);
    }

    void Start() {
        StartingScale = transform.localScale;
        StartCoroutine(clean()); 
    }
}
