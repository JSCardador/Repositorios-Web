using System.Collections;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    
    /// <summary>
    /// Corutina para agitar la panlla cuandlo lo indiquemos.
    /// </summary>
    /// <param name="duracion">Duracion del efecto de agitar la pantalla</param>
    /// <param name="fuerza">Fuerza con la que se agita la pantalla.</param>
    /// <returns></returns>
    public IEnumerator Shake(float duracion, float fuerza)
    {
        Transform Cam = Camera.main.transform;
        Vector3 originalPos = Cam.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duracion)
        {
            float x = Random.Range(-1f, 1f) * fuerza;
            float y = Random.Range(-1f, 1f) * fuerza;

            Cam.localPosition = new Vector3(Cam.transform.localPosition.x + x, Cam.transform.localPosition.y + y, Cam.transform.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        Cam.localPosition = originalPos;
    }

}

