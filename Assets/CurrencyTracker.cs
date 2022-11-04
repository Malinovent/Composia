using System;
using UnityEngine;


namespace Composia
{
    public class CurrencyTracker : MonoBehaviour
    {
        public int minScore = 0;
        public int maxScore = 1000;

        private int _currentScore = 0;
        public int CurrentScore 
        { 
            get => _currentScore;
            set 
            { 
                _currentScore = value;

                if(_currentScore > maxScore) { _currentScore = maxScore; }
                if(_currentScore < 0) { _currentScore = 0; }

                OnUpdateScore(CurrentScore);
                //if (ui != null) { ui.SetTextUI(_currentScore); }
            }
        }

        public event EventHandler<GenericEventArgs> UpdateScore;

        private void OnEnable()
        {
            Events_Global.GetCurrency += AddScore;
        }

        private void OnDisable()
        {
            Events_Global.GetCurrency -= AddScore;
        }

        private void Start()
        {
            OnUpdateScore(CurrentScore);
        }

        public void AddScore(int score)
        {
            CurrentScore += score;        
        }
        public bool SpendScore(int amount)
        {
            if (amount > _currentScore)
            {
                Debug.Log("Not enough currency!");
                //Trigger fail feedback
                return false;
            }

            CurrentScore -= amount;
            return true;
        }

        public void OnUpdateScore(int amount)
        { 
            if(UpdateScore != null) { UpdateScore(this, new GenericEventArgs(amount)); }
        }
    }
}