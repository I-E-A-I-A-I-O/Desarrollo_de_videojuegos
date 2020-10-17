using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode_4 : MonoBehaviour
{
    private List<GameObject> remainingCubes = new List<GameObject>(), row1 = new List<GameObject>(),
        row2 = new List<GameObject>(), row3 = new List<GameObject>(), row4 = new List<GameObject>();
    private bool flag = false;


    public IEnumerator Move()
    {
        remainingCubes = GameObject.FindGameObjectsWithTag("Cube").ToList();
        Arrange();
        var AI_selection = Add(row1.Count, row2.Count, row3.Count, row4.Count);

        foreach (GameObject cube in remainingCubes)
        {
            if (AI_selection[1] == 0) break;
            switch (cube.transform.localPosition.y)
            {
                case 4.2f:
                    {
                        if (AI_selection[0] == 1)
                        {
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                            AI_selection[1]--;
                        }
                        break;
                    }
                case -2.80000019f:
                    {
                        if (AI_selection[0] == 2)
                        {
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                            AI_selection[1]--;
                        }
                        break;
                    }
                case -9.8f:
                    {
                        if (AI_selection[0] == 3)
                        {
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                            AI_selection[1]--;
                        }
                        break;
                    }
                case -17.8f:
                    {
                        if (AI_selection[0] == 4)
                        {
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                            AI_selection[1]--;
                        }
                        break;
                    }
            }
            yield return new WaitForSeconds(1);
        }

        DestroySelection();
        gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
    }

    private void DestroySelection()
    {
        Eliminate();
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

    private void Arrange()
    {
        foreach(GameObject cube in remainingCubes)
        {
            switch (cube.transform.localPosition.y)
            {
                case 4.2f:
                    {
                        row1.Add(cube);
                        break;
                    }
                case -2.80000019f:
                    {
                        row2.Add(cube);
                        break;
                    }
                case -9.8f:
                    {
                        row3.Add(cube);
                        break;
                    }
                case -17.8f:
                    {
                        row4.Add(cube);
                        break;
                    }
            }
        }
    }

    private bool checkParity(int a, int b, int c, int d)
    {
        string row1Bin, row2Bin, row3Bin, row4Bin;
        int row1Int, row2Int, row3Int, row4Int, total;
        row1Bin = Convert.ToString(a, 2);
        row2Bin = Convert.ToString(b, 2);
        row3Bin = Convert.ToString(c, 2);
        row4Bin = Convert.ToString(d, 2);
        row1Int = int.Parse(row1Bin);
        row2Int = int.Parse(row2Bin);
        row3Int = int.Parse(row3Bin);
        row4Int = int.Parse(row4Bin);
        total = row1Int + row2Int + row3Int + row4Int;
        return total % 2 == 0;
    }

    private int RowCheck(int row)
    {
        bool flag = false;
        switch (row)
        {
            case 1:
                {
                    if (row1.Count <= 0) 
                    {
                        row++;
                        flag = true;
                    }
                    break;
                }
            case 2:
                {
                    if (row2.Count <= 0) 
                    {
                        row++;
                        flag = true;
                    }
                    break;
                }
            case 3:
                {
                    if (row3.Count <= 0)
                    {
                        row++;
                        flag = true;
                    }
                    break;
                }
            case 4:
                {
                    if (row4.Count <= 0)
                    {
                        row = 1;
                        flag = true;
                    }
                    break;
                }
        }
        if (flag) return RowCheck(row);
        else return row;
    }

    private int[] Add(int row1, int row2, int row3, int row4)
    {
        int row1Temp = 0, row2Temp = 0, row3Temp = 0, row4Temp = 0, randRow;
        randRow = UnityEngine.Random.Range(1, 5);
        randRow = RowCheck(randRow);
        var totalCheck1 = checkParity(row1, row2, row3, row4);

        switch (randRow)
        {
            case 1:
                {
                    row1Temp = row1 - UnityEngine.Random.Range(1, row1 + 1);
                    break;
                }
            case 2:
                {
                    row2Temp = row2 - UnityEngine.Random.Range(1, row2 + 1);
                    break;
                }
            case 3:
                {
                    row3Temp = row3 - UnityEngine.Random.Range(1, row3 + 1);
                    break;
                }
            case 4:
                {
                    row4Temp = row4 - UnityEngine.Random.Range(1, row4 + 1);
                    break;
                }
        }
        var totalCheck2 = checkParity(row1Temp, row2Temp, row3Temp, row4Temp);
        if (!totalCheck1 && !totalCheck2)
        {
            return Add(row1, row2, row3, row4);
        }
        else
        {
            int[] results = new int[2];
            results[0] = randRow;
            switch (randRow)
            {
                case 1:
                    {
                        results[1] = row1 - row1Temp;
                        break;
                    }
                case 2:
                    {
                        results[1] = row2 - row2Temp;
                        break;
                    }
                case 3:
                    {
                        results[1] = row3 - row3Temp;
                        break;
                    }
                case 4:
                    {
                        results[1] = row4 - row4Temp;
                        break;
                    }
            }
            return results;
        }
    }

    private void Eliminate()
    {
        row1.Clear();
        row2.Clear();
        row3.Clear();
        row4.Clear();
    }
}
