using TMPro;
using UnityEngine;
using UtilityScripts;

public class HandlePlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameField;

    //Once the player entered or modified the name in the input field then save it in the Game Manager
    public void HandlePlayerNameEdited()
    {
        if (!string.IsNullOrEmpty(playerNameField.text))
        {
            Debugging.instance.DebugLog(playerNameField.text);
            GameManager.instance.playerName = playerNameField.text;
        }        
    }
}
