﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject _BombPrefab;
    public int _BombCount = 3;
    private List<Bomb> _Bombs = new List<Bomb>();
    public GameObject _ScoreStick;
    public GameObject _StickEnd;
    public float _ScoreTime = 6.0f;
    public GameObject _Floor;

    public Text _ScoreText;



    // Update is called once per frame
    void Update () {
        bool lmp = Input.GetMouseButtonUp(0);

        if(lmp && _BombCount > 0)
        {
            Vector2 pos = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(pos);

            var go = Instantiate(_BombPrefab, worldPosition, Quaternion.identity);

            var bombScript = go.GetComponent<Bomb>();
                _Bombs.Add(bombScript);

            _BombCount--;
            if(_BombCount == 0)
            {
                foreach(var bomb in _Bombs)
                {
                    bomb.DoExplode();
                }
                Invoke("CalculateScore", _ScoreTime);



            }
        }
        if(_ScoreStick.activeInHierarchy)
        {
            var height = _ScoreStick.transform.position.y - _Floor.transform.position.y;
            var score = 1000.0f / (height + 0.1f);
            _ScoreText.text = "SCORE: " + Mathf.RoundToInt(score);
            var height2 = _StickEnd.transform.position.y;
            if(height2<-75)
            {
                if(score>100)
                {
                    var main_score = PlayerPrefs.GetInt("zetony");
                    main_score++;
                    PlayerPrefs.SetInt("zetony", main_score);
                }
                Application.LoadLevel("Cafe1");
            }
            //yield return new WaitForSeconds(15f);
            //Application.LoadLevel("Cafe1");


        }
        //yield return new WaitForSeconds(1);

    }

    void CalculateScore()
    {
        _ScoreStick.SetActive(true);
        _StickEnd.SetActive(true);
    }
}
