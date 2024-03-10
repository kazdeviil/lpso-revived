using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoxSize : MonoBehaviour
{
    [SerializeField] GameObject questBox;
    [SerializeField] GameObject questCheckbox;
    float questBoxHeight;
    float checkBoxYPos;
    float checkBoxXPos;

    public void CenterCheckBox()
    {
        questBoxHeight = questBox.GetComponent<RectTransform>().pivot.y;
        questCheckbox.transform.position.Set(questCheckbox.transform.position.x, checkBoxYPos, 0);
    }
}
