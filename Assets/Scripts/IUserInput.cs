using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("====== Output signals =====")]
    //信号
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    //1.pressing signal

    //2.trigger once signal
    public bool attack;
    public bool skill1;
    public bool skill2;
    public bool skill3;
    public bool skill4;

    //3.double trigger

    [Header("====== Others =====")]
    //旗标
    public bool inputEnable = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    protected void UpdateDmagDvec(float Dright) {

        Dmag = Mathf.Sqrt(Dup * Dup) + Mathf.Sqrt(Dright * Dright);
        Dvec = Dright * transform.forward;
    }
}
