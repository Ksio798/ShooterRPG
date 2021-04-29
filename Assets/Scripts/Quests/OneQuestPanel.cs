using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OneQuestPanel : MonoBehaviour
{
    public event System.Action<int> OnClick;
    [HideInInspector]
    public TextMeshProUGUI text;
    [HideInInspector]
    public string textQuest;
    [HideInInspector]
    public int index;
    public void AddText()
    {
        text.text = textQuest;
        OnClick?.Invoke(index);
    }


}
