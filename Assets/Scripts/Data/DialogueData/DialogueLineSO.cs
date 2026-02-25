using UnityEngine;


[CreateAssetMenu(menuName = "RPG SetUp/Dialogue Data/New Line Data", fileName = "Line - ")]

public class DialogueLineSO : ScriptableObject
{
    [Header("Dialogue Info")]
    public string DialogueGroupName;
    public DialogueSpeakerSO speaker;

    [Header("Text Options")]
    [TextArea] public string[] textLine;

    [Header("Choices Info")]
    [TextArea] public string playerChoiceAnswer;
    public DialogueLineSO[] choiceLines;

    [Header("Dialogue Action")]
    [TextArea] public string actionLine;
    public DialogueActionType actionType;

    public string GetFirstLine() => textLine[0];

    public string GetRandomLine()
    {
        return textLine[Random.Range(0, textLine.Length)];
    }
}
