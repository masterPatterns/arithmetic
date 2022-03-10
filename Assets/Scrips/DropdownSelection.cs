using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSelection : MonoBehaviour
{
    Dropdown m_Dropdown;

    public GameObject arithmetic;
    public Text Text2;

    // Start is called before the first frame update
    void Start()
    {
        m_Dropdown = GetComponent<Dropdown>();
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
        //dropdown = GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(Dropdown thisDropdown)
    {
        string op = "" + thisDropdown.captionText.text;
        Text2.text = op;

        GameObject obj = GameObject.Find("Calculate");
        Arithmetic ar = obj.GetComponent<Arithmetic>();
        ar.randomNum();

        //Arithmetic ar = GameObject.Find("Calculate").GetComponent<Arithmetic>();
        //ar.randomNum();
        //arithmetic.GetComponent<Arithmetic>().randomNum();
    }

}
