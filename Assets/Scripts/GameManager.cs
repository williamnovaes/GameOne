using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace GameOne {
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        private Player player;
        [SerializeField] private int lives;
        [SerializeField] private int currentLives;
        [SerializeField] private int score;
        private bool paused = false;

        void Awake() {
            scoreText = GetComponent<Text>();
            scoreText.text = "000";
            score = 0;
            lives = 3;
            currentLives = lives;
            player = FindObjectOfType<Player>();
        }
        // Update is called once per frame
        void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Submit")) {
                PauseResume();
            }
        }

        void PauseResume() {
            if (paused) {
                Time.timeScale = 1f;
            } else {
                Time.timeScale = 0f;
            }
            paused = !paused;
        }

        public void AddPoint(int point) {
            score += point;
        }


    }
}