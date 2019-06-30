using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    private bool m_Selected = false;

    public bool Selected { get { return m_Selected;  } }

    public bool swap = false;

    public void Select()
    {
        m_Selected = true;
        foreach(var selection in GetComponents<Interaction>())
        {
            selection.Select();
        }
    }

    public void Deselect()
    {
        m_Selected = false;
        foreach (var selection in GetComponents<Interaction>())
        {
            selection.Deselect();
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (swap)
        {
            swap = false;
            if(m_Selected)
            {
                Deselect();
            }
            else
            {
                Select();
            }
        }
    }
}
