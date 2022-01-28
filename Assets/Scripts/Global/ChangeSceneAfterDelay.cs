﻿using UnityEngine;
using System.Collections;


public class ChangeSceneAfterDelayScript : MonoBehaviour {

	public string _nextScene = "";

	public float _delay = 5f;

	public IEnumerator Start()
	{
		yield return new WaitForSeconds(_delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(_nextScene);
	}
}
