using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject[] slots;
    [SerializeField] private GameObject Selector;
    [SerializeField] private GameObject Settings;
    [SerializeField] private TMP_Text m_goldText;
    [SerializeField] private Slider m_healthbar;
    [SerializeField] private GameObject m_endGamePanel;

    private int index = 0;
    private bool gamePaused;
    private bool gameEnded;
    private bool canContinue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Selector.transform.position = slots[0].transform.position;
        gamePaused = false;
        m_endGamePanel.SetActive(false);
    }

    private void Start() {
        UpdateSlotCosts();
    }

    public void UpdateHealthbar(int amt) {
        m_healthbar.value = amt;
    }

    private void CanContinue() {
        canContinue = true;
    }

    private void Update()
    {
        if(gameEnded) {
            m_endGamePanel.SetActive(true);
            if(Input.anyKeyDown && canContinue) {
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            }

            return;
        }

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

    public void UpdateGold(int gold) {
        m_goldText.text = gold.ToString();
    }

    public void UpdateSlotCosts() {
        for(int i = 0; i < slots.Length; i++) {
            BuildingManager.Instance.setBuilding(i);
            slots[i].transform.GetChild(0).GetComponent<TMP_Text>().text = BuildingManager.Instance.getBuilding().BuildingCost.ToString();
        }
    }

    public void EndGameScreen() {
        gameEnded = true;
        Invoke("CanContinue", 2);
    }

    public void changeSelected(bool direction)
    {
        if (direction) {
            if (index <= 0)
            {
                index = 8;
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
            if (index >= 8)
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
