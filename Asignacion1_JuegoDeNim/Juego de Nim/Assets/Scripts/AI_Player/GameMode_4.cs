using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        String row1Bin = ToBinary(row1.Count);
        String row2Bin = ToBinary(row2.Count);
        String row3Bin = ToBinary(row3.Count);
        String row4Bin = ToBinary(row4.Count);
        String totalXOR = XOR(XOR(XOR(row1Bin, row2Bin), row3Bin), row4Bin);
        if (IsLoosingPos(totalXOR))
        {
            DoSelection(RandomSelection());
            yield return new WaitForSeconds(1);
            DestroySelection();
            gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
        }
        else
        {
            int[] selection_data = new int[2];
            String row1XORtotal = XOR(row1Bin, totalXOR);
            int decimalRow1XORTotal = ToDecimal(row1XORtotal);
            if (decimalRow1XORTotal < row1.Count)
            {
                selection_data[0] = 1;
                selection_data[1] = row1.Count - decimalRow1XORTotal;
                DoSelection(selection_data);
                yield return new WaitForSeconds(1);
                DestroySelection();
                gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
            }
            else
            {
                String row2XORtotal = XOR(row2Bin, totalXOR);
                int decimalRow2XORTotal = ToDecimal(row2XORtotal);
                if (decimalRow2XORTotal < row2.Count)
                {
                    selection_data[0] = 2;
                    selection_data[1] = row2.Count - decimalRow2XORTotal;
                    DoSelection(selection_data);
                    yield return new WaitForSeconds(1);
                    DestroySelection();
                    gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
                }
                else
                {
                    String row3XORtotal = XOR(row3Bin, totalXOR);
                    int decimalRow3XORTotal = ToDecimal(row3XORtotal);
                    if (decimalRow3XORTotal < row3.Count)
                    {
                        selection_data[0] = 3;
                        selection_data[1] = row3.Count - decimalRow3XORTotal;
                        DoSelection(selection_data);
                        yield return new WaitForSeconds(1);
                        DestroySelection();
                        gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
                    }
                    else
                    {
                        String row4XORtotal = XOR(row4Bin, totalXOR);
                        int decimalRow4XORTotal = ToDecimal(row4XORtotal);
                        if (decimalRow4XORTotal < row4.Count)
                        {
                            selection_data[0] = 4;
                            selection_data[1] = row4.Count - decimalRow4XORTotal;
                            DoSelection(selection_data);
                            yield return new WaitForSeconds(1);
                            DestroySelection();
                            gameObject.GetComponent<MainScript>()._buttonClickSound.Play();
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void DoSelection(int[] selection_data)
    {
        foreach (GameObject cube in remainingCubes)
        {
            if (selection_data[1] == 0) break;
            switch (cube.transform.localPosition.y)
            {
                case 4.2f:
                    {
                        if (selection_data[0] == 1)
                        {
                            selection_data[1]--;
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                        }
                        break;
                    }
                case -2.80000019f:
                    {
                        if (selection_data[0] == 2)
                        {
                            selection_data[1]--;
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                        }
                        break;
                    }
                case -9.8f:
                    {
                        if (selection_data[0] == 3)
                        {
                            selection_data[1]--;
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                        }
                        break;
                    }
                case -17.8f:
                    {
                        if (selection_data[0] == 4)
                        {
                            selection_data[1]--;
                            cube.GetComponent<Cube_script>().IsSelected = true;
                            cube.GetComponent<Light>().enabled = true;
                            gameObject.GetComponent<Player_control_4>()._cubeSelectionSound.Play();
                        }
                        break;
                    }
            }
        }
    }

    private int[] RandomSelection()
    {
        int[] data = new int[2];
        int randomRow = UnityEngine.Random.Range(1, 5), selection = 0;
        randomRow = CheckEmptyRow(randomRow);
        switch (randomRow)
        {
            case 1:
                {
                    selection = UnityEngine.Random.Range(1, row1.Count + 1);
                    break;
                }
            case 2:
                {
                    selection = UnityEngine.Random.Range(1, row2.Count + 1);
                    break;
                }
            case 3:
                {
                    selection = UnityEngine.Random.Range(1, row3.Count + 1);
                    break;
                }
            case 4:
                {
                    selection = UnityEngine.Random.Range(1, row4.Count + 1);
                    break;
                }
        }
        data[0] = randomRow;
        data[1] = selection;
        return data;
    }

    private int CheckEmptyRow(int row)
    {
        switch (row)
        {
            case 1:
                {
                    if (row1.Count == 0)
                    {
                        row++;
                        row = CheckEmptyRow(row);
                    }
                    break;
                }
            case 2:
                {
                    if (row2.Count == 0)
                    {
                        row++;
                        row = CheckEmptyRow(row);
                    }
                    break;
                }
            case 3:
                {
                    if (row3.Count == 0)
                    {
                        row++;
                        row = CheckEmptyRow(row);
                    }
                    break;
                }
            case 4:
                {
                    if (row3.Count == 0)
                    {
                        row = 1;
                        row = CheckEmptyRow(row);
                    }
                    break;
                }
        }
        return row;
    }

    private Boolean IsLoosingPos(String xor)
    {
        int flag = 0;
        for (int i = 0; i < 3; i++)
        {
            if (xor[i] == '0') flag++;
        }
        return flag == 3;
    }

    private int ToDecimal(String binary)
    {
        return Convert.ToInt32(binary, 2);
    }

    private String XOR(String A, String B)
    {
        String XOR = "";
        for (int i = 0; i < 3; i++)
        {
            if (A[i] == B[i])
            {
                XOR += "0";
            }
            else
            {
                XOR += "1";
            }
        }
        return XOR;
    }

    private String ToBinary(int number)
    {
        String Bin = Convert.ToString(number, 2);
        while (Bin.Length < 3)
        {
            Bin = "0" + Bin;
        }
        return Bin;
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

    private void Eliminate()
    {
        row1.Clear();
        row2.Clear();
        row3.Clear();
        row4.Clear();
    }
}
