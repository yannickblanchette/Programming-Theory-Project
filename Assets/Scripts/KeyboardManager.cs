using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager instance {  get; private set; }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public bool IsSpacebarPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
