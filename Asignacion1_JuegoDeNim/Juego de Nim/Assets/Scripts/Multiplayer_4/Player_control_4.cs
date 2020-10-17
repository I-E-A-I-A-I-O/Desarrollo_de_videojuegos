using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_control_4 : MonoBehaviour
{
    private RaycastHit _hit;
    private int _activeLine = -1;
    public AudioSource _cubeSelectionSound;
    public List<GameObject> _playerSelection = new List<GameObject>();

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
                    _cubeSelectionSound.Play();
                    if (_playerSelection.Count == 0)
                    {
                        switch (_hit.transform.localPosition.y)
                        {
                            case 4.2f:
                                {
                                    _activeLine = 0;
                                    break;
                                }
                            case -2.80000019f:
                                {
                                    _activeLine = 1;
                                    break;
                                }
                            case -9.8f:
                                {
                                    _activeLine = 2;
                                    break;
                                }
                            case -17.8f:
                                {
                                    _activeLine = 3;
                                    break;
                                }
                        }
                        _hit.transform.GetComponent<Light>().enabled = true;
                        _hit.transform.GetComponent<Cube_script>().IsSelected = true;
                        _playerSelection.Add(_hit.transform.gameObject);
                    }
                    else
                    {
                        switch(_activeLine)
                        {
                            case 0:
                                {
                                    if (!(_hit.transform.localPosition.y == 4.2f)) return;
                                    ToggleSelect(_hit.transform.gameObject);
                                    break;
                                }
                            case 1:
                                {
                                    if (!(_hit.transform.localPosition.y == -2.80000019f)) return;
                                    ToggleSelect(_hit.transform.gameObject);
                                    break;
                                }
                            case 2:
                                {
                                    if (!(_hit.transform.localPosition.y == -9.8f)) return;
                                    ToggleSelect(_hit.transform.gameObject);
                                    break;
                                }
                            case 3:
                                {
                                    if (!(_hit.transform.localPosition.y == -17.8f)) return;
                                    ToggleSelect(_hit.transform.gameObject);
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }

    private void ToggleSelect(GameObject selection)
    {
        selection.GetComponent<Light>().enabled = selection.GetComponent<Light>().enabled ? false : true;
        selection.GetComponent<Cube_script>().IsSelected = selection.GetComponent<Cube_script>().IsSelected ? false : true;
        if (selection.GetComponent<Cube_script>().IsSelected)
        {
            _playerSelection.Add(selection);
        }
        else
        {
            _playerSelection.Remove(selection);
        }
    }
}
