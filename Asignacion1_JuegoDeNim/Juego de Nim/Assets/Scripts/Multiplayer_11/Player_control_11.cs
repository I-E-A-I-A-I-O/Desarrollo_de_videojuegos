using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_control_11 : MonoBehaviour
{
    private RaycastHit _hit;
    public List<GameObject> _playerSelection = new List<GameObject>();
    public AudioSource CubeSelectionSound;

    private void Update()
    {
        if ((SceneManager.GetActiveScene().name.Equals("SinglePlayer_11") || SceneManager.GetActiveScene().name.Equals("SinglePlayer_4")) && gameObject.GetComponent<MainScript>()._turn == 1) return;
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(mousePos, out _hit))
            {
                if (_hit.transform.CompareTag("Cube"))
                {
                    CubeSelectionSound.Play();
                    if (_playerSelection.Count < 3)
                    {
                        _hit.transform.GetComponent<Light>().enabled = _hit.transform.GetComponent<Light>().enabled ? false : true;
                        _hit.transform.GetComponent<Cube_script>().IsSelected = _hit.transform.GetComponent<Cube_script>().IsSelected ? false : true;
                        if (_playerSelection.Contains(_hit.transform.gameObject))
                        {
                            _playerSelection.Remove(_hit.transform.gameObject);
                        }
                        else
                        {
                            _playerSelection.Add(_hit.transform.gameObject);
                        }
                    }
                    else
                    {
                        if (_hit.transform.GetComponent<Light>().enabled)
                        {
                            _hit.transform.GetComponent<Light>().enabled = false;
                            _hit.transform.GetComponent<Cube_script>().IsSelected = false;
                            _playerSelection.Remove(_hit.transform.gameObject);
                        }
                    }
                }
            }
        }
    }
}
