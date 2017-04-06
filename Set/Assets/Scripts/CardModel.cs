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

        QUANTITY
    }

    public enum eColor
    {
        RED = 0,
        BLUE,
        GREEN,
        YELLOW,

        QUANTITY
    }

    public enum eShape
    {
        CIRCLE = 0,
        DIAMOND,
        STAR,

        QUANTITY
    }

    public enum eFill
    {
        NONE = 0,
        STRIPED,
        FILL,

        QUANTITY
    }

    public enum eCount
    {
        ONE = 0,
        TWO,
        THREE,

        QUANTITY
    }

    public static int GetAttributeQuantity(int attribute)
    {
        switch ((eAttribute)attribute)
        {
            case eAttribute.COLOR:
                return (int)eColor.QUANTITY;
            case eAttribute.FILL:
                return (int)eFill.QUANTITY;
            case eAttribute.SHAPE:
                return (int)eShape.QUANTITY;
            case eAttribute.NUMBER:
                return (int)eCount.QUANTITY; //    this needs to be defined somewhere -- no sense in having an enum for it
            default:
                {
                    Debug.LogError("CardModel::GetAttributeQuantity -- attribute is not defined!");
                    return 0;
                }
        }
    }

    private int[] m_attributes;
    public int[] Attributes
    {
        get { return m_attributes; }
        set { m_attributes = value; }
    }

	// Use this for initialization
	void Awake () {
		m_attributes = new int[(int)(eAttribute.QUANTITY)];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
}
