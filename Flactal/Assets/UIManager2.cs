using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager2 : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text CompText;

    [SerializeField]
    UnityEngine.UI.Slider CompSlider;

    //====================

    [SerializeField]
    UnityEngine.UI.Text IncText;

    [SerializeField]
    UnityEngine.UI.Slider IncSlider;

    [SerializeField]
    UnityEngine.UI.Text DecText;

    [SerializeField]
    UnityEngine.UI.Slider DecSlider;
    //====================

    [SerializeField]
    FractalMain fractalMain;


    [SerializeField]
    TextMeshProUGUI gauge;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnUpdateComplex()
    {
        fractalMain.NextMaxGeneration = (int)CompSlider.value;
        CompText.text = "複雑さ:" + CompSlider.value.ToString();
    }

    public void OnUpdateInc()
    {
        fractalMain.IncSpeed = (int)IncSlider.value;
        IncText.text = "チャージ速度：" + IncSlider.value.ToString();
    }

    public void OnUpdateDec()
    {
        fractalMain.DecSpeed = (int)DecSlider.value;
        DecText.text = "減少速度：" + DecSlider.value.ToString();
    }



    public void OnPushedReset()
    {
        fractalMain.Restart();
    }

    public void OnPressCharge()
    {
        fractalMain.ModeOn();
        
    }

    public void OnReleaseCharge()
    {
        fractalMain.ModeOff();
    }

    public void UpdateGauge(float rate)
    {
        gauge.text = (rate).ToString() + "%";
    }
}
