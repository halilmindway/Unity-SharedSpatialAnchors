using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text winningText;

        private void OnEnable()
        {
            GameManager.OnGameFinished += DisplayWinner;
        }

        private void OnDisable()
        {
            GameManager.OnGameFinished -= DisplayWinner;
        }

        private void DisplayWinner(Type type)
        {
            winningText.text = Enum.GetName(typeof(Type), type) + " is the Winner!";
        }
    }
}