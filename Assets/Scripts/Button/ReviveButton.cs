using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveButton : MonoBehaviour
{
    public GameObject dieUI;

    public GameObject player;
 
    private PlayerConditions playerConditions;
    private PlayerController playerController;

    public void OnClick()
    {
        playerConditions = player.GetComponent<PlayerConditions>();
        playerController = player.GetComponent<PlayerController>();

        playerConditions.health.curValue = playerConditions.health.startValue;
        playerConditions.stamina.curValue = playerConditions.stamina.startValue;
        playerConditions.hunger.curValue = playerConditions.hunger.startValue;

        Time.timeScale = 1f;

        playerController.ToggleCursor(false);
        dieUI.SetActive(false);
        Debug.Log("Revive");
    }
}
