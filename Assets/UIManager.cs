using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    [SerializeField] TextMeshProUGUI winLoseText;
    [SerializeField] float spawnAfter = 3f;

    private const string WIN_TEXT = "GG WP\n <color=green>YOU WIN!";
    private const string LOSE_TEXT = "GG WP\n <color=red>YOU LOSE!";


    private void OnEnable()
    {
        LevelController.OnEnd += ShowUIAfter;
    }

    private void OnDisable()
    {
        LevelController.OnEnd -= ShowUIAfter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowUIAfter(bool win)
    {
        winLoseText.SetText(win == true ? WIN_TEXT : LOSE_TEXT);
        Invoke("EnableUI", spawnAfter);
    }

    void EnableUI()
    {
        endGameUI.SetActive(true);
    }

    public void RetryPressed()
    {
        Initiate.Fade("Laurio", Color.gray, 1f);
    }
}
