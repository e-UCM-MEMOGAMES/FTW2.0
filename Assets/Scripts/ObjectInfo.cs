using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour {
    /// <summary>
    /// Panel donde se muestra la informacion
    /// </summary>
    public GameObject info;
    /// <summary>
    /// Nombre del objeto
    /// </summary>
    public new string name;
	void Start () {
        info.SetActive(false);
	}
	
    private void OnMouseEnter()
    {
        info.SetActive(true);
        info.GetComponentInChildren<Text>().text = name;
    }
    private void OnMouseExit()
    {
        info.SetActive(false);
    }
}
