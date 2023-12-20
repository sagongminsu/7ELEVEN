using System.Collections;
using TMPro;
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

    public GameObject alarmBox;
    public TextMeshProUGUI alarmTxt;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void OnOffCraftMenu()
    {
        SoundManager.Instance.CraftAudio("Open", 1f);
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
        SoundManager.Instance.CraftAudio("ButtonClickSound", 1f);
        mainMenu.SetActive(false);
        cookMenu.SetActive(false);
        normalMenu.SetActive(true);
        blacksmithMenu.SetActive(false);
    }

    public void OnCookMenu()
    {
        if (PlayerController.instance.isInCookArea)
        {
            SoundManager.Instance.CraftAudio("ButtonClickSound", 1f);
            mainMenu.SetActive(false);
            cookMenu.SetActive(true);
            normalMenu.SetActive(false);
            blacksmithMenu.SetActive(false);
        }
        else
        {
            SoundManager.Instance.CraftAudio("ButtonClickSoundFaile", 1f);
            alarmTxt.text = "Need Fire";
            StartCoroutine(Alarm());
            Debug.Log("Need Fire");
        }
    }

    public void OnBlackSmithMenu()
    {
        if (PlayerController.instance.isInBlacksmithArea)
        {
            SoundManager.Instance.CraftAudio("ButtonClickSound", 1f);
            mainMenu.SetActive(false);
            cookMenu.SetActive(false);
            normalMenu.SetActive(false);
            blacksmithMenu.SetActive(true);
        }
        else
        {
            SoundManager.Instance.CraftAudio("ButtonClickSoundFaile", 1f);
            alarmTxt.text = "Need Warkbanch";
            StartCoroutine(Alarm());
            Debug.Log("Need Warkbanch");
        }
    }

    public void ReturnMainMenu()
    {
        SoundManager.Instance.CraftAudio("ButtonClickSound", 1f);
        mainMenu.SetActive(true);
        cookMenu.SetActive(false);
        normalMenu.SetActive(false);
        blacksmithMenu.SetActive(false);
    }

    IEnumerator Alarm()
    {
        alarmBox.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        alarmBox.SetActive(false);
        alarmTxt.text = string.Empty;
    }
}
