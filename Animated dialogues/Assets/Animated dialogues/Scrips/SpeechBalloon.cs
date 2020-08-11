using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBalloon : MonoBehaviour
{
    // Variables publicas


    /// <summary>
    /// Retraso al escribir dos letras.
    /// </summary>
    public float delay = 0.01f;


    /// <summary>
    /// Imagen que indica que la frase ha acabado y podemos continuar.
    /// </summary>
    public Image imgPressA;


    /// <summary>
    /// Lista de frases que escribiremos en la TextBox.
    /// </summary>
    public List<string> speechs = new List<string>();



    // Variables privadas
    private float auxDelay;
    private Text textQuad;
    private string currentText;
    private bool writing;
    private int countSentences;


    void Awake()
    {
        // Busca el cuadro de texto donde escribir, suponiendo que será uno de sus hijos.
        textQuad = this.GetComponentInChildren<Text>();

        imgPressA.enabled = false;
    }


    private void OnEnable()
    {
        StartCoroutine(WriteWord());

        auxDelay = delay;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {


            // Para acelerar el texto
            if (writing)
            {
                delay = 0;
            }


            // una vez acabada de escribir una frase, saltar a la siguiente
            else if (countSentences <= speechs.Count - 2)
            {
                countSentences++;
                delay = auxDelay;
                currentText = "";
                StartCoroutine(WriteWord());
                imgPressA.enabled = false;
            }


            // Cuando acabe el parrafo
            else
            {
                countSentences = 0;
                imgPressA.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// Corrutina que escribe las frases que le indiquemos letra por letra.
    /// </summary>
    private IEnumerator WriteWord()
    {
        writing = true;
        for (int i = 0; i < speechs[countSentences].Length + 1; i++)
        {
            currentText = speechs[countSentences].Substring(0, i);
            textQuad.text = currentText;

            yield return new WaitForSeconds(delay);
        }

        // Final de la frase
        imgPressA.enabled = true;
        writing = false;
        yield return null;
    }

}
