using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    // Progress bar image
    private Image Bar;

    // Type of progress bar
    public enum Type { LinealHorizontal, LinealVertical, LinealStep, Radial, RadialSnap}
    public Type type;

    // Where the radial bars start
    public enum RadialType { Top, Right, Down, Left }
    public RadialType radialType;

    // Common values
    public bool byPercent;
    public float nPercent;
    public Text nPercentText;
    public float MaxValue;
    public float CurrentValue;


    // Lineal properties
    public bool startRight = true;

    // Snaps properties
    public int nSnaps;

    // Logic
    private void OnEnable()
    {
        Bar = GetComponent<Image>();
        Bar.type = Image.Type.Filled;

        switch (type)
        {


            case Type.LinealHorizontal:
                Bar.fillMethod = Image.FillMethod.Horizontal;
                if (startRight)
                    Bar.fillOrigin = (int)Image.OriginHorizontal.Right;
                else
                    Bar.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;



            case Type.LinealVertical:
                Bar.fillMethod = Image.FillMethod.Vertical;
                if (startRight)
                    Bar.fillOrigin = (int)Image.OriginVertical.Top;
                else
                    Bar.fillOrigin = (int)Image.OriginVertical.Bottom;
                break;



            case Type.Radial:
                Bar.fillMethod = Image.FillMethod.Radial360;
                switch (radialType)
                {
                    case RadialType.Top:
                        Bar.fillOrigin = (int)Image.Origin360.Top;
                        break;

                    case RadialType.Down:
                        Bar.fillOrigin = (int)Image.Origin360.Bottom;
                        break;

                    case RadialType.Left:
                        Bar.fillOrigin = (int)Image.Origin360.Left;
                        break;

                    case RadialType.Right:
                        Bar.fillOrigin = (int)Image.Origin360.Right;
                        break;

                }

                break;



            case Type.LinealStep:
                Bar.fillMethod = Image.FillMethod.Horizontal;
                Bar.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;



            case Type.RadialSnap:
                Bar.fillMethod = Image.FillMethod.Radial360;
                switch (radialType)
                {
                    case RadialType.Top:
                        Bar.fillOrigin = (int)Image.Origin360.Top;
                        break;

                    case RadialType.Down:
                        Bar.fillOrigin = (int)Image.Origin360.Bottom;
                        break;

                    case RadialType.Left:
                        Bar.fillOrigin = (int)Image.Origin360.Left;
                        break;

                    case RadialType.Right:
                        Bar.fillOrigin = (int)Image.Origin360.Right;
                        break;

                }
                break;

        }

    }

    private void Update()
    {
        // Calculamos el porcentaje del progreso
        nPercent = ((CurrentValue * 100) / MaxValue);
        if (nPercentText)
            nPercentText.text = nPercent.ToString("f0") + "%";

        switch (type)
        {
            case Type.LinealHorizontal:
                Bar.fillAmount = nPercent / 100f;
                break;



            case Type.LinealVertical:
                Bar.fillAmount = nPercent / 100f;
                break;



            case Type.Radial:
                Bar.fillAmount = nPercent / 100f;
                break;



            case Type.LinealStep:
                float n = MaxValue / nSnaps; // Cuanto vale cada cuadro
                int m = (int)(CurrentValue / n); // cuantos cuadros hay que pintar
                Bar.fillAmount = ((m * 100) / nSnaps) / 100f; // Proporcion de los cuadros a pintar en el FillAmount
                break;



            case Type.RadialSnap:
                float rn = MaxValue / nSnaps; // Cuanto vale cada step
                int rm = (int)(CurrentValue / rn); // Cuantos steps hay que pintar
                Bar.fillAmount = ((rm * 100) / nSnaps) / 100f; // Proporcion de los cuadros a pintar en el FillAmount
                break;
        }

        

        
    }


    // Inspector editor
#if UNITY_EDITOR

    [CustomEditor(typeof(ProgressBar))]
    public class enumInspectorEditor : Editor
    {


        public override void OnInspectorGUI()
        {
            var enumScript = target as ProgressBar;
            // Ejegir el tipo de barra
            enumScript.type = (Type)EditorGUILayout.EnumPopup("Bar Type ", enumScript.type);


            // Valores comunes a todas laws barras de progresion
            EditorGUILayout.LabelField("Common values", EditorStyles.boldLabel);
            enumScript.CurrentValue = EditorGUILayout.FloatField("Current value", enumScript.CurrentValue);
            enumScript.MaxValue = EditorGUILayout.FloatField("Max value", enumScript.MaxValue);

            
            // Texto para escribir el porcentaje
            enumScript.nPercentText = (Text)EditorGUILayout.ObjectField("Text percent", enumScript.nPercentText, typeof(Text), true);


            // Segun el tipo de barra editaremos el inspector con los valores que haga falta rellenar, para que sea mas intuitivo
            switch (enumScript.type)
            {

                case Type.LinealHorizontal:
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Values LinealBar", EditorStyles.boldLabel);
                    enumScript.startRight = EditorGUILayout.Toggle("Start right", enumScript.startRight);
                    break;



                case Type.LinealVertical:
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Values LinealBar", EditorStyles.boldLabel);
                    enumScript.startRight = EditorGUILayout.Toggle("Start top", enumScript.startRight);
                    break;



                case Type.Radial:
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Values RadialBar", EditorStyles.boldLabel);
                    enumScript.radialType = (RadialType)EditorGUILayout.EnumPopup("Start", enumScript.radialType);
                    break;



                case Type.LinealStep:
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Values Lineal SnapBar", EditorStyles.boldLabel);
                    enumScript.nSnaps = EditorGUILayout.IntField("Number of steps", enumScript.nSnaps);
                    break;



                case Type.RadialSnap:
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Values Radial SnapBar", EditorStyles.boldLabel);
                    enumScript.radialType = (RadialType)EditorGUILayout.EnumPopup("Start", enumScript.radialType);
                    enumScript.nSnaps = EditorGUILayout.IntField("Number of blocks", enumScript.nSnaps);
                    break;
            }

        }
    }

#endif
}
