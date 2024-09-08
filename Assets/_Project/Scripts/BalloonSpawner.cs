using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;
using PhotonPun = Photon.Pun;

namespace _Project.Scripts
{
    public class BalloonSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject redBalloonPrefab;
        [SerializeField] private GameObject blueBalloonPrefab;
        [SerializeField] private int balloonCount = 10;

        [SerializeField] private Transform redBalloonsParent;
        [SerializeField] private Transform blueBalloonsParent;


        const string redBalloonName = "RedBalloon ";
        const string blueBalloonName = "BlueBalloon ";

        [Button]
        public void SpawnBalloons()
        {
            for (int i = 0; i < balloonCount; i++)
            {
                GameManager.Instance.redBalloons.Add(InstantiateBalloon(i, redBalloonPrefab,
                    redBalloonName, redBalloonsParent).GetComponent<Balloon>());

                GameManager.Instance.blueBalloons.Add(InstantiateBalloon(i, blueBalloonPrefab,
                    blueBalloonName, blueBalloonsParent).GetComponent<Balloon>());
            }
        }

        private GameObject InstantiateBalloon(int i, GameObject balloonPrefab, string name, Transform balloonParent)
        {
            var b = PhotonPun.PhotonNetwork.Instantiate(balloonPrefab.name, transform.position + GetRandomPoint(), Quaternion.identity/*,balloonParent*/);
            b.name = name + i;
            return b;
        }

        private Vector3 GetRandomPoint()
        {
            return Random.insideUnitSphere * 0.1f;
        }
    }
}