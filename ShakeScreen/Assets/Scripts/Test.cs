using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float duracion, fuerza;


    private ShakeScreen shakeScreen;
    // Start is called before the first frame update
    void Start()
    {
        shakeScreen = FindObjectOfType<ShakeScreen>();
        StartCoroutine(shakeScreen.Shake(duracion, fuerza));
    }
    private void OnEnable()
    {
        StartCoroutine(shakeScreen.Shake(duracion, fuerza));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(shakeScreen.Shake(duracion, fuerza));
        }
    }
}
