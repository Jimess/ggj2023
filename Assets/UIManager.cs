using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    [SerializeField] TextMeshProUGUI winLoseText;

    private const string WIN_TEXT = "GG WP\n <color=green>YOU WIN!";
    private const string LOSE_TEXT = "GG WP\n <color=red>YOU LOSE!";


    private void OnEnable()
    {
        LevelController.OnEnd += ShowUI;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= ShowUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowUI(bool win)
    {
        endGameUI.SetActive(true);
        winLoseText.SetText(win == true ? WIN_TEXT : LOSE_TEXT);
    }

    public void RetryPressed()
    {
        Initiate.Fade("Laurio", Color.gray, 1f);
    }
}
