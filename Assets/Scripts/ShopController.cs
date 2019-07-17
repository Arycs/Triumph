using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public GameObject detail;
    public Toggle toggle;

	void Awake () {

        toggle = GetComponent<Toggle>();
	}
	
	void Update () {

        detail.SetActive(toggle.isOn);
        GetComponent<Image>().color = toggle.isOn ? Color.red : Color.white;
	}

    //public void ChangeToggle(GameObject detail)
    //{
        
    //}
}
