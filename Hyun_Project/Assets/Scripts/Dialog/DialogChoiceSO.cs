using UnityEngine;

[CreateAssetMenu(fileName = "DialogChoiceSO", menuName = "Dailog System/DialogChoiceSO")]
public class DialogChoiceSO : ScriptableObject
{
    public string text;
    public int nextId;
}
