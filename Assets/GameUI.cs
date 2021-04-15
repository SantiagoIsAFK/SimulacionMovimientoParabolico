using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public SphereMov _sphereMov;
    public GameObject panel1, panel2;
    public Slider sldAngle, sldVelocity, sldEstimate;
    public Text txtAngle, txtVelocity, txtEstimate;
    public Text txtResult;
    public LineRenderer lineaEstimate, lineaReal;
    
    
    public GameObject estimateGO;
    
    public void Reset()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        
        txtResult.text = "";
        
        txtAngle.text = "Angulo inicial: "+sldAngle.value.ToString();
        txtVelocity.text = "Velocidad inicial: "+sldVelocity.value.ToString();
        txtEstimate.text = "Distancia estimada: "+sldEstimate.value.ToString();
        
        _sphereMov.ResetSimulation();
        lineaReal.SetPosition(1, Vector3.zero);
        
        StopAllCoroutines();
    }
    
    public void UpdateData()
    {
        _sphereMov.angle = sldAngle.value;
        _sphereMov.v0 = sldVelocity.value;
        
        
        txtAngle.text = "Angulo inicial: "+sldAngle.value.ToString();
        txtVelocity.text = "Velocidad inicial: "+sldVelocity.value.ToString();
        txtEstimate.text = "Distancia estimada: "+sldEstimate.value.ToString();
        
        lineaEstimate.SetPosition(1, new Vector3(sldEstimate.value,0,0));
        estimateGO.transform.localPosition = new Vector3(sldEstimate.value, estimateGO.transform.localPosition.y, 0);
    }

    public void StartRealTimeSimulation()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        StartCoroutine(_sphereMov.Simulate(sldEstimate.value, true, (result) =>
        {
            ShowResult(result);
        })  );
    }
    
    public void StartSimulation()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        StartCoroutine(_sphereMov.Simulate(sldEstimate.value, false, (result) =>
        {
            ShowResult(result);
        })  );
    }

    public void ShowResult(bool result)
    {        
        if (result)
        {
            txtResult.text = $"Has acertado, la distancia real es de {_sphereMov.x}";
        }
        else
        {
            txtResult.text = $"Has fallado, la distancia real es de {_sphereMov.x}";
        }           
            
        lineaReal.SetPosition(1, new Vector3(_sphereMov.x,0,0));
    }
}
