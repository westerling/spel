using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{

    public static ActionsManager Current;

    public Button[] buttons;

    private List<Action> actionCalls = new List<Action>();

    public ActionsManager()
    {
        Current = this;
    }

    public void ClearButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
        actionCalls.Clear();
    }

    public void AddButton(Sprite pic, Action onClick)
    {
        var index = actionCalls.Count;
        buttons[index].gameObject.SetActive(true);
        buttons[index].GetComponent<Image>().sprite = pic;
        actionCalls.Add(onClick);
    }

    public void OnButtonClick(int index)
    {
        actionCalls[index]();
    }

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var index = i;
            buttons[index].onClick.AddListener(delegate ()
            {
                OnButtonClick(index);
            }
            );
        }
        ClearButtons();
    }
}
