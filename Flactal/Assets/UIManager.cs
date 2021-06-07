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


    }

    // Update is called once per frame
    void Update()
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
                }else
                {
                    fractalMaterial[i].SetFloat("blueMode", -1.0f);
                }

            }
        }


        ///
        juliaMaterial.SetInt("_MaxIteration", (int)juliaComplicateSlider.value);
        juliaMaterial.SetInt("_Threshold", (int)julia_Threshold.value);




    }
}
