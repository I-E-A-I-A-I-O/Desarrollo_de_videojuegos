using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode_11 : MonoBehaviour
{
    private int _take;
    private List<GameObject> remainingCubes = new List<GameObject>();

    public IEnumerator Move()
    {
        remainingCubes = GameObject.FindGameObjectsWithTag("Cube").ToList();
        if (remainingCubes.Count == 4)
        {
            _take = 3;
        }
        else if (remainingCubes.Count == 3)
        {
            _take = 2;
        }
        else if (remainingCubes.Count == 2)
        {
            _take = 1;
        }
        else if (remainingCubes.Count % 2 == 0)
        {
            do
            {
                _take = Random.Range(1, 4);
            } while (_take % 2 == 0);
        }
        else if (remainingCubes.Count % 2 == 1)
        {
            _take = 2;
        }

        foreach (GameObject cube in remainingCubes)
        {
            if (_take == 0) break;
            cube.GetComponent<Cube_script>().IsSelected = true;
            cube.GetComponent<Light>().enabled = true;
            gameObject.GetComponent<Player_control_11>().CubeSelectionSound.Play();
            _take--;
            yield return new WaitForSeconds(1);
        }

        DestroySelection();
        gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
    }

    private void DestroySelection()
    {
        for (int i = 0; i < remainingCubes.Count; i++)
        {
            GameObject cube = remainingCubes[i];
            if (cube.GetComponent<Cube_script>().IsSelected)
            {
                remainingCubes.Remove(cube);
                gameObject.GetComponent<MainScript>()._cubes.Remove(cube);
                Destroy(cube);
                DestroySelection();
            }
        }
    }
}
