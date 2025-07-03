using TMPro;
using UnityEngine;

public class HandlePlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameField;

    //Once the player entered or modified the name in the input field then save it in the Game Manager
    public void HandlePlayerNameEdited()
    {
        if (playerNameField.text != string.Empty)
        {
            GameManager.instance.playerName = playerNameField.text;
        }        
    }
}
