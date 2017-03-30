using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public enum eAttribute
    {
        ATTRIBUTE_1 = 0,
        ATTRIBUTE_2,
        ATTRIBUTE_3,

        ATTRIBUTE_COUNT
    }

    private int[] m_attributes = new int[(int)(eAttribute.ATTRIBUTE_COUNT)];

    // Use this for initialization
    void Start()
    {
        GenerateCard();
    }

    // Update is called once per frame
    void Update() { }

    //  DEBUG method; unsure if we are randomly generating 
    void GenerateCard()
    {
        for (int i = 0; i < m_attributes.Length; ++i)
        {
            m_attributes[i] = Random.Range(0, 5);
        }
    }

    public int GetAttribute(eAttribute attribute_key) 
    {
        return m_attributes[(m_attributes[(int)(attribute_key)])];
    }

    //  Use to select the card as part of your 'set'
    public void Select()
    {
        //  Send event to PlayerHand? w/ self as param
        PlayerHandController.instance.AddCardToProposedSet(this.gameObject.GetHashCode());
    }

    public void Deselect()
    {
        //  Send event to PlayerHand? w/ self as param
        PlayerHandController.instance.RemoveAddFromProposedSet(this.gameObject.GetHashCode());
    }

}
