using UnityEngine;

public class CraftMenu : MonoBehaviour
{
    private PlayerController controller;
    public GameObject craftingMenu;
    public GameObject mainMenu;
    public GameObject normalMenu;
    public GameObject cookMenu;
    public GameObject blacksmithMenu;

    bool Open = false;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void OnOffCraftMenu()
    {
        if (Open == false)
        {
            controller.ToggleCursor(true);
            craftingMenu.SetActive(true);
            mainMenu.SetActive(true);
            cookMenu.SetActive(false);
            normalMenu.SetActive(false);
            blacksmithMenu.SetActive(false);
            Time.timeScale = 0f;
            Open = !Open;
        }
        else
        {
            controller.ToggleCursor(false);
            mainMenu.SetActive(true);
            craftingMenu.SetActive(false);
            Open = !Open;
            Time.timeScale = 1f;
        }
    }

        public void OnNormalMenu()
    {
        mainMenu.SetActive(false);
        cookMenu.SetActive(false);
        normalMenu.SetActive(true);
        blacksmithMenu.SetActive(false);
    }

    public void OnCookMenu()
    {
        mainMenu.SetActive(false);
        cookMenu.SetActive(true);
        normalMenu.SetActive(false);
        blacksmithMenu.SetActive(false);
    }

    public void OnBlackSmithMenu()
    {
        mainMenu.SetActive(false);
        cookMenu.SetActive(false);
        normalMenu.SetActive(false);
        blacksmithMenu.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        mainMenu.SetActive(true);
        cookMenu.SetActive(false);
        normalMenu.SetActive(false);
        blacksmithMenu.SetActive(false);
    }
}
