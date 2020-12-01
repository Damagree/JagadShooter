using UnityEngine;

public class PopUps : MonoBehaviour
{
    public void PopUp(GameObject popUp)
    {
        popUp.SetActive(true);
    }

    public void PopDown(GameObject popDown)
    {
        popDown.SetActive(false);
    }
}
