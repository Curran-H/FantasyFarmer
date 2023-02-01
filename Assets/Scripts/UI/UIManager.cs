using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject Selector;
    [SerializeField] private GameObject Settings;
    private int index = 0;
    private bool gamePaused;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Selector.transform.position = slots[0].transform.position;
        gamePaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (gamePaused) {
                Settings.SetActive(false);
                ResumeGame();
            } else {
                Settings.SetActive(true);
                PauseGame();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !gamePaused) // forward
        {
            changeSelected(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && !gamePaused) // backwards
        {
            changeSelected(false);
        }
    }

    public void changeSelected(bool direction)
    {
        if (direction) {
            if (index <= 0)
            {
                index = 7;
                Selector.transform.position = slots[index].transform.position;
            }
            else
            {
                index--;
                Selector.transform.position = slots[index].transform.position;
            }

            BuildingManager.Instance.setBuilding(index);
        }
        if (!direction)
        {
            if (index >= 7)
            {
                index = 0;
                Selector.transform.position = slots[index].transform.position;
            }
            else
            {
                index++;
                Selector.transform.position = slots[index].transform.position;
            }

            BuildingManager.Instance.setBuilding(index);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }
}
