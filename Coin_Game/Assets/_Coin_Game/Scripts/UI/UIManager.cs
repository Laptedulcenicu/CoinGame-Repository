using System;
using System.Collections.Generic;
using _Coin_Game.Scripts.Game.Coin;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Coin_Game.Scripts.UI
{
    public enum GameState
    {
        GamePlay,
        Play,
        Lose,
        Win,
    }

    [Serializable]
    public struct PanelData
    {
        [SerializeField] private GameState gameState;
        [SerializeField] private GameObject panel;

        public GameState State => gameState;

        public GameObject Panel => panel;
    }

    public class UIManager : MonoBehaviour
    {
        public static Action<GameState> SetGameState;
        public static Action<int> CoinTextValue;

        [SerializeField] private List<PanelData> panelList;
        [SerializeField] private TextMeshProUGUI coinAmountText;
        [SerializeField] private Image coiImage;

        private int coinAmount;


        private void Awake()
        {
            SetGameState += ActivatePanel;
            CoinTextValue += CoinAmount;

            ActivatePanel(GameState.Play);
        }

        private void OnDestroy()
        {
            SetGameState -= ActivatePanel;
            CoinTextValue -= CoinAmount;
        }

        private void CoinAmount(int value)
        {
            coiImage.transform.DOScale(Vector3.one * 1.1f, 0.5f).OnComplete(() =>
            {
                coiImage.transform.DOScale(Vector3.one, 0.5f);
            });

            coinAmount += value;
            coinAmountText.text = coinAmount.ToString();
        }

        private void ActivatePanel(GameState gameState)
        {
            foreach (var panelData in panelList)
            {
                panelData.Panel.SetActive(false);
            }

            panelList.Find(e => e.State == gameState).Panel.SetActive(true);
        }

        public void TryAgainOrNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            CoinController.MoveState(false);
        }

        public void Play()
        {
            ActivatePanel(GameState.GamePlay);
            CoinController.MoveState(true);
        }
    }
}