using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNPC : Camera
{

    private bool m_ready = false;


    public void SetTarget(Transform _target){
        if(m_ready) return;
        this.target = _target;
        m_ready = true;
    }

    void Start(){
        offset = new Vector3(0f, -5f, -15f);
    }

    private void FixedUpdate()
    {
        if(m_ready){        base.FixedUpdate();}

    }
}
