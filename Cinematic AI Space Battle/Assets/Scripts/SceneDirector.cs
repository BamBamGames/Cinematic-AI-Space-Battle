using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneDirector : MonoBehaviour {

    public GameObject _audioTrack;
    public AudioClip _introTheme;
    public AudioClip _battleTheme;
    public AudioClip _outroTheme;

    private AudioSource _introSource;
    private AudioSource _battleSource;
    private AudioSource _outroSource;

    public Image _screenFade;
    public float _alphaChange = 0.05f;
    public float _alphaInterval = 0.05f;

    IEnumerator _fadeScreenIn;
    IEnumerator _fadeScreenOut;

    public float _timer = 0;

    public float _introTimeCutOff;
    public float _takeOffTimeCutOff;
    public float _battleCutOff;

    public GameObject _constantCanvas;

    public enum Scene {
        Intro,
        TakeOFF,
        Battle,
        Outro
    }

    public static SceneDirector Instance;

    public Scene _currentScene = Scene.Intro;

    // Use this for initialization
    void Start () {
        Instance = this;
       DontDestroyOnLoad(gameObject);
       DontDestroyOnLoad(_constantCanvas);

        _introSource = gameObject.AddComponent<AudioSource>();
        _introSource.loop = true;
        _introSource.clip = _introTheme;
        _introSource.spatialBlend = 0f;
        _introSource.volume = 1f;
        _introSource.Play(0);

        _battleSource = gameObject.AddComponent<AudioSource>();
        _battleSource.loop = true;
        _battleSource.clip = _battleTheme;
        _battleSource.spatialBlend = 0f;
        _battleSource.volume = 1f;

        _outroSource = gameObject.AddComponent<AudioSource>();
        _outroSource.loop = true;
        _outroSource.clip = _outroTheme;
        _outroSource.spatialBlend = 0f;
        _outroSource.volume = 1f;

        StartCoroutine(Timer());

        _fadeScreenIn = FadeScreenIn();
        _fadeScreenOut = FadeScreenOut();

        StartScreenFadeIn();
    }

    public void StartScreenFadeIn() {
        Debug.Log("StartSceenFadeIn");
        _screenFadeInFinished = false;
        StartCoroutine(_fadeScreenIn);
    }

    public void StartScreenFadeOut() {
        Debug.Log("StartSceenFadeOut");
        _screenFadeOutFinished = false;
        StartCoroutine(_fadeScreenOut);
    }

    public bool _screenFadeInFinished = true;
    

    IEnumerator FadeScreenIn() {
        while (true) {
            
            var tempColor = _screenFade.color;
            tempColor.a = tempColor.a - _alphaChange;
            Debug.Log("Reductig Alpha : " + tempColor.a);
            _screenFade.color = tempColor;
            yield return new WaitForSeconds(_alphaInterval);
        }
    }

    public bool _screenFadeOutFinished = true;

    IEnumerator FadeScreenOut() {
        while (true) {
            var tempColor = _screenFade.color;
            Debug.Log("Adding Alhpa : " + tempColor.a);
            tempColor.a = tempColor.a + _alphaChange/2;
            _screenFade.color = tempColor;
            yield return new WaitForSeconds(_alphaInterval);
        }
    }

    IEnumerator Timer() {
        while (true) {
            yield return new WaitForSecondsRealtime(1);
            _timer++;
        }
    }

    private bool _battleFinished = false;

	// Update is called once per frame
	void Update () {

        if (!_battleFinished && _timer > 320) {

            float redScore = GameObject.FindGameObjectWithTag("RedHQ").GetComponent<HQScript>()._totalPoints;
            float greenScore = GameObject.FindGameObjectWithTag("GreenHQ").GetComponent<HQScript>()._totalPoints;

            if (redScore > greenScore) {
                SceneManager.LoadScene("RedWin", LoadSceneMode.Single);
            } else {
                SceneManager.LoadScene("GreenWin", LoadSceneMode.Single);
            }
            _battleFinished = true;
        }

        if (_currentScene == Scene.Intro && _timer >= _introTimeCutOff - 10) {

            if (_screenFadeOutFinished) {
                StartScreenFadeOut();
            }
           
            if (!_screenFadeOutFinished) {
                Debug.Log("Scene fade out started");
                
            }

            if (_screenFadeOutFinished) {
                Debug.Log("Next scene");
                _currentScene = Scene.TakeOFF;
                NextScene();
            }
        }

         if (_currentScene == Scene.TakeOFF && _timer >= _takeOffTimeCutOff - 10) {
         
                if (_screenFadeOutFinished) {
                    StartScreenFadeOut();
                }

                if (_screenFadeOutFinished) {
                    Debug.Log("Next scene");
                    _currentScene = Scene.Battle;
                    NextScene();
                }

            }

        if (_timer > _takeOffTimeCutOff && _currentScene == Scene.TakeOFF) {
            _currentScene = Scene.Battle;
            NextScene();
        }


        if (_timer > _introTimeCutOff && _currentScene == Scene.Intro) {
            _currentScene = Scene.TakeOFF;
            NextScene();
        }

        if (!_screenFadeInFinished) {
            if (_screenFade.color.a < 0) {
                StopCoroutine(_fadeScreenIn);
                Debug.Log("EndSceenFadeIn");
                _screenFadeInFinished = true;
            }
        }

        if (!_screenFadeOutFinished) {
            if (_screenFade.color.a >= 1) {
                StopCoroutine(_fadeScreenOut);
                _screenFadeOutFinished = true;
                Debug.Log("EndSceenFadeOut");
            }
        }
    }

    public void NextScene() {
        Debug.Log("NextScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartScreenFadeIn();
    }
}
