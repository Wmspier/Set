using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour {

    public enum eAttribute
    {
        COLOR = 0,
        FILL,
        SHAPE,
        NUMBER,

        ATTRIBUTE_COUNT
    }

    private eAttribute[] m_attributes;
    public eAttribute[] Attributes
    {
        get { return m_attributes; }
        set { m_attributes = value; }
    }

	// Use this for initialization
	void Awake () {
		m_attributes = new eAttribute[(int)(eAttribute.ATTRIBUTE_COUNT)];

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
