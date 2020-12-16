using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    [SerializeField] private GameObject exitPanel;

    public void OnClick_Exit()
    {
        exitPanel.SetActive(true);
    }

    public void OnClick_ExitYes()
    {
        Application.Quit();
    }

    public void OnClick_ExitNo()
    {
        exitPanel.SetActive(false);
    }

}
