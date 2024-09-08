using System;
using System.Collections.Generic;
using Mindway.MindwayDEV.Scripts.Common;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public List<Balloon> redBalloons;
        public List<Balloon> blueBalloons;
        [SerializeField] private BalloonSpawner balloonSpawner;
        public bool isGrabbing;
        public static event Action<Type> OnGameFinished;
        public static event Action<Type, int> OnBalloonCountChanged;

        private void Start()
        {
            redBalloons = new List<Balloon>();
            blueBalloons = new List<Balloon>();
            SpawnBalloons();
        }

        [Button]
        public void SpawnBalloons()
        {
            balloonSpawner.SpawnBalloons();
        }


        private void OnEnable()
        {
            OnBalloonCountChanged += DebugBalloonNumber;
        }

        private void OnDisable()
        {
            OnBalloonCountChanged -= DebugBalloonNumber;
        }

        private void DebugBalloonNumber(Type type, int count)
        {
            Debug.Log(Enum.GetName(typeof(Type), type) + " count: " + count);
        }

        private void CheckWinningSituation(Type type, int count)
        {
            if (count >= 1) return;
            OnGameFinished?.Invoke(type);
            Debug.Log(Enum.GetName(typeof(Type), type) + " won the game");
            if (redBalloons.Count > 0)
            {
                foreach (var red in redBalloons)
                {
                    red.Pop(Type.Red);
                }
            }
            else if (blueBalloons.Count > 0)
            {
                foreach (var blue in blueBalloons)
                {
                    blue.Pop(Type.Blue);
                }
            }
        }

        public void AddBalloonsToList(List<Balloon> balloons, Type type)
        {
            switch (type)
            {
                case Type.Blue:
                    blueBalloons = balloons;
                    break;
                case Type.Red:
                    redBalloons = balloons;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void RemoveBalloonFromList(Balloon balloon)
        {
            if (redBalloons.Contains(balloon))
            {
                redBalloons.Remove(balloon);
                OnBalloonCountChanged?.Invoke(Type.Red, redBalloons.Count);
            }
            else if (blueBalloons.Contains(balloon))
            {
                blueBalloons.Remove(balloon);
                OnBalloonCountChanged?.Invoke(Type.Blue, blueBalloons.Count);
            }
        }

        public void SetIsGrabbingToTrue()
        {
            isGrabbing = true;
        }

        public void SetIsGrabbingToFalse()
        {
            isGrabbing = false;
        }
    }
}