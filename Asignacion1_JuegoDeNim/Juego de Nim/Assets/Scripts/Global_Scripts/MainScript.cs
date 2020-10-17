using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public List<GameObject> _cubes = new List<GameObject>();
    [SerializeField] private GameObject _turnLabel, _endgamePanel, _endgameText;
    public int _turn { get; private set; }
    public AudioSource _buttonClickSound;
    private bool _gameOver = false;

    private int Rand(int start, int finish)
    {
        return Random.Range(start, finish + 1);
    }

    private void Start()
    {
        _turn = Rand(0, 1);
        ChangeLabel();
        if ((SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4")) 
            && gameObject.GetComponent<MainScript>()._turn == 1)
        {
            StartCoroutine(AI_Turn());
        }
    }

    private void ChangeLabel()
    {
        switch (_turn)
        {
            case 0:
                {
                    _turnLabel.GetComponent<Text>().text = "Turno de: Jugador 1";
                    break;
                }
            case 1:
                {
                    _turnLabel.GetComponent<Text>().text = "Turno de: Jugador 2";
                    break;
                }
            default:
                {
                    Debug.Log("Unknown error.");
                    break;
                }
        }
    }

    public void onEndTurnClick()
    {
        if ((SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4")) 
            && _turn == 1) return;

        _buttonClickSound.Play();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Multiplayer_4":
                {
                    gameObject.GetComponent<Player_control_4>()._playerSelection.Clear();
                    break;
                }
            case "Multiplayer_11":
                {
                    gameObject.GetComponent<Player_control_11>()._playerSelection.Clear();
                    break;
                }
            case "SinglePlayer_11":
                {
                    gameObject.GetComponent<Player_control_11>()._playerSelection.Clear();
                    break;
                }
            case "SinglePlayer_4":
                {
                    gameObject.GetComponent<Player_control_4>()._playerSelection.Clear();
                    break;
                }
        }
        for (int i = 0; i < _cubes.Count; i++)
        {
            GameObject cube = _cubes[i];
            if (cube.GetComponent<Cube_script>().IsSelected)
            {
                _cubes.Remove(cube);
                Destroy(cube);
                onEndTurnClick();
            }
        }
    }

    public void EndTurnLabelChange()
    {
        if (_gameOver) return;
        if (_cubes.Count <= 1) return;
        _turn = _turn == 0 ? 1 : 0;
        ChangeLabel();
        if ((SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4")) && 
            gameObject.GetComponent<MainScript>()._turn == 1)
        {
            StartCoroutine(AI_Turn());
        }
    }

    private void LateUpdate()
    {
        if (!(_cubes.Count <= 1)) return;
        _gameOver = true;
        _endgamePanel.SetActive(true);
        Debug.Log(_turn);
        switch (_turn)
        {
            case 0:
                {
                    if (SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4"))
                    {
                        _endgameText.GetComponent<Text>().text = "Ganador: Jugador 1";
                    }
                    else
                    {
                        _endgameText.GetComponent<Text>().text = "Ganador: Jugador 2";
                    }
                    break;
                }
            case 1:
                {
                    if (SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4"))
                    {
                        _endgameText.GetComponent<Text>().text = "Ganador: Jugador 2";
                    }
                    else
                    {
                        _endgameText.GetComponent<Text>().text = "Ganador: Jugador 1";
                    }
                    break;
                }
        }
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    private void AI_think()
    {
        if (_gameOver) return;
        if (SceneManager.GetActiveScene().name.Equals("SinglePlayer_11"))
        {
           StartCoroutine(gameObject.GetComponent<GameMode_11>().Move());
        }
        else
        {
            StartCoroutine(gameObject.GetComponent<GameMode_4>().Move());
        }
    }

    private IEnumerator AI_Turn()
    {
        yield return new WaitForSeconds(1.2f);
        AI_think();
        yield return new WaitForSeconds(10);
        EndTurnLabelChange();
    }
}
