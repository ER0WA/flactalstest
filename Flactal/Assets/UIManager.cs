using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Renderer[] FractalRenderer = new Renderer[4];

    [SerializeField]
    Slider complicateSlider;

    [SerializeField]
    Toggle blueMode;

    [SerializeField]
    Renderer JuliaRenderer;

    [SerializeField]
    Slider juliaComplicateSlider;

    [SerializeField]
    Slider julia_Threshold;

    [SerializeField]
    Triangle triangles;

    [SerializeField]
    Slider tri_Min;

    [SerializeField]
    Text min_tex;

    [SerializeField]
    Slider tri_Max;

    [SerializeField]
    Text max_tex;


    [SerializeField]
    Slider perfect;

    [SerializeField]
    Text perfect_text;





    public float  test = 1.0f;
    public float _MaxIter = 1.0f;

    const float PI = 3.14159f;

    private Material[] fractalMaterial = new Material[4];
    private Material juliaMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        /// flactal
        complicateSlider.maxValue = PI-0.2f;
        complicateSlider.minValue = 0.0f;
        for (int i = 0; i < 4; ++i)
        {
            fractalMaterial[i] = FractalRenderer[i].material;
        }

        /// julia
        juliaComplicateSlider.maxValue = 256.0f;
        juliaComplicateSlider.minValue = 1.0f;
        juliaComplicateSlider.value = 16;

        julia_Threshold.maxValue = 200.0f;
        julia_Threshold.minValue = 1.0f;
        julia_Threshold.value = 2.0f;
        juliaMaterial = JuliaRenderer.material;

        tri_Min.maxValue = 1.0f;
        tri_Min.minValue = 0.0f;
        tri_Min.value = 0.1f;

        tri_Max.maxValue = 5.0f;
        tri_Max.minValue = tri_Min.maxValue;
        tri_Max.value = 0.8f;


        perfect.minValue = 0.0f;
        perfect.maxValue = 1.0f;
        perfect.value = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {
       if (FractalRenderer[0].gameObject.activeSelf == true)
        {

            for (int i = 0; i < 4; ++i)
            {
                if (fractalMaterial[i] != null)
                {
                    _MaxIter = fractalMaterial[i].GetFloat("_MaxIter");
                    fractalMaterial[i].SetFloat("complexity", Mathf.Min(complicateSlider.value, 2.0f * PI));
                    fractalMaterial[i].SetFloat("test", test);

                    if (blueMode.isOn)
                    {
                        fractalMaterial[i].SetFloat("blueMode", 1.0f);
                    } else
                    {
                        fractalMaterial[i].SetFloat("blueMode", -1.0f);
                    }

                }
            }
        }


        ///
        if (JuliaRenderer.gameObject.activeSelf == true)
        {
            juliaMaterial.SetInt("_MaxIteration", (int)juliaComplicateSlider.value);
            juliaMaterial.SetInt("_Threshold", (int)julia_Threshold.value);
        }
        


        //if (triangles.gameObject.activeSelf == true)
        //{
            tri_Max.minValue = tri_Min.value;

            triangles.RandMax = tri_Max.value;
            triangles.RandMin = tri_Min.value;

            max_tex.text = string.Concat("Maxaaa:", tri_Max.value.ToString());
            min_tex.text = string.Concat("Min:", tri_Min.value.ToString());

            triangles.imperfect = perfect.value;
            perfect_text.text = string.Concat("完全性:", perfect.value.ToString());
        //}


    }
}
