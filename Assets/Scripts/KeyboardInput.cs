using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput {

    [Header("====== Key settings =====")]
    //键位
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyE; // E
    public string key1; //1
    public string key2; //2
    public string key3; //3
    public string key4; //4
    public int mou0;
    public int mou1;
    public int mou2;

    public MyButton btnE = new MyButton();
    public MyButton btn1 = new MyButton();
    public MyButton btn2 = new MyButton();
    public MyButton btn3 = new MyButton();
    public MyButton btn4 = new MyButton();
    public MyButton btn0 = new MyButton();
    //public MyButton btn1 = new MyButton();
    //public MyButton btn2 = new MyButton();
	
	void Update () {

        btnE.Tick(Input.GetKey(keyE));
        btn1.Tick(Input.GetKey(key1));
        btn2.Tick(Input.GetKey(key2));
        btn3.Tick(Input.GetKey(key3));
        btn4.Tick(Input.GetKey(key4));
        btn0.Tick(Input.GetMouseButton(mou0));
        //btn1.Tick(Input.GetMouseButton(mou1));
        //btn2.Tick(Input.GetMouseButton(mou2));

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0.0f) - (Input.GetKey(keyLeft) ? 1.0f : 0.0f);

        if (inputEnable == false)
        {
            targetDup = 0.0f;
            targetDright = 0.0f;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.05f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.05f);

        //Dmag = Mathf.Sqrt(Dright * Dright);
        //Dvec = Dright * transform.forward;
        UpdateDmagDvec(Dright);

        //attack = btn0.onPressed;
        attack = btn0.onPressed;

        skill1 = btn1.onPressed;
        skill2 = btn2.onPressed;
        skill3 = btn3.onPressed;
        skill4 = btn4.onPressed;
    }
}
