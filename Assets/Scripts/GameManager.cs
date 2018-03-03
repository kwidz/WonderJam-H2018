﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float timeSec;
	public Vector3 balleStartPos;
	public bool edtionMode;
	public int score;
	private HudManager hud;
    public GameObject cible;
    private GameObject cibleObj;
	private Balle balle;

	public delegate void MyDelegate(bool actif);
	public  MyDelegate myDelegate;



	// Use this for initialization
	void Start () {
		hud = GameObject.FindGameObjectWithTag ("HUD").GetComponent<HudManager> ();
		balle = GameObject.FindGameObjectWithTag ("Balle").GetComponent<Balle> ();
        changeCible();
        
		hud.UpdateScoreText (0);
		launchModeEdit ();

	}
	
	// Update is called once per frame
	void Update () {
		timeSec -= Time.deltaTime;
		hud.UpdateTimerText (timeSec);
		if (timeSec <= 0) {
			SceneManager.LoadScene ("GameOverScene");
		}

		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (edtionMode) {
				launchGame ();
			} else {
				launchModeEdit ();
			}
		}
	}

	public void launchGame(){
		edtionMode = false;
		if (myDelegate != null) {
			myDelegate (true);
		}
		balle.restartBalle ();

	}

	public void launchModeEdit(){
		edtionMode = true;
		if (myDelegate != null) {
			myDelegate (false);
		}

		balle.resetPosBalle (balleStartPos);


	}
    public void cibleTouched()
    {
        addScore(1);
        launchModeEdit();
		addTime (30);
        changeCible();

    }
    public void changeCible()
    {
        Vector3 position = new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-25.0f, 25.0f),0);
        Destroy(cibleObj);
        cibleObj = Instantiate(cible, position, Quaternion.identity);
    }

	public void addScore(int score){
		this.score += score;
		hud.UpdateScoreText (this.score);
	}

	public void deleteTime(float time){
		timeSec -= time;
	}

	public void addTime(float time){
		timeSec += time;
	}
}
