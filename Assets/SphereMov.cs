using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMov : MonoBehaviour
{
    public float x;
    public float v0, angle;
    private float g = 9.8f;
    private bool withoutStart = true;

    
    public void ResetSimulation()
    {
        withoutStart = true;
        this.transform.localPosition = Vector3.zero;
    }
    
    public IEnumerator Simulate(float estimate, bool realTime, Action<Boolean> result)
    {
        float time = 0;
        while (this.transform.localPosition.y > 0 || withoutStart)
        {
            if (realTime) time += Time.deltaTime;
            else time += 0.01f;
            
            this.transform.localPosition = new Vector3(v0*Mathf.Cos((angle*Mathf.PI)/180)*time, v0*Mathf.Sin((angle*Mathf.PI)/180)*time-(0.5f*g*time*time),0);
            withoutStart = false;
            
            if (realTime) yield return null;
            else yield return new WaitForSeconds(0.01f);
        }

        x = (v0*v0*Mathf.Sin((angle*Mathf.PI)/90))/g; 
        
        this.transform.localPosition = new Vector3(x, 0,0);
        
        bool _result = Mathf.Abs(estimate - (x)) <= x*0.05f ? true: false;
        result(_result);
    }
}
