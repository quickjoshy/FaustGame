using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChanging : MonoBehaviour
{
    // Start is called before the first frame update
    Material mat;
    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    IEnumerator ChangeColor() {
        while (gameObject.activeInHierarchy) {
            yield return new WaitForSeconds(.1f);
            float r, g, b;
            r = Random.Range(0f, 1f);
            g = Random.Range(0f, 1f);
            b = Random.Range(0f, 1f);
            mat.color = new Color(r, g, b, 1f);
        }
    }
}
