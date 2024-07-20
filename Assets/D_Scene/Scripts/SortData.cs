using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SortData : MonoBehaviour
{
    public static SortData instance;
    public Dropdown dropdown;
    public Toggle Toggle;
    public InputField inputField;

    static string filePath = "Assets/StreamingAssets/people.csv";

    private List<CSVDateBase> personData = CSVReader.ReadPeopleFromCSV(filePath);
    public List<CSVDateBase> currentData = new List<CSVDateBase>();

    public bool isInitial = false;
    public bool isAsc = false;

    public void Awake()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "people.csv");
        personData = CSVReader.ReadPeopleFromCSV(filePath);
        currentData = NameFillter("");
         instance = this;
    }

    

    private void Update()
    {
        isAsc = Toggle.isOn;

        Debug.Log(isInitial);
        Debug.Log(dropdown.value);
        string InputCheck = InputFieldCheck();
        if (InputCheck != null) 
        {
            currentData = sortDropDown(InputCheck); 
        }
        else
        {
            currentData = NameFillter("");
        }
      

    }


    List<CSVDateBase> NameFillter(string InputText)
    {
        List<CSVDateBase> result;

        if (!isAsc)
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name ascending
                where data.Name.Contains(InputText)
                select data).ToList<CSVDateBase>();
            
        }
        else
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name descending
                where data.Name.Contains(InputText)
                select data).ToList<CSVDateBase>();
        }

        return result;
    }
    List<CSVDateBase> AgeFillter(string InputText)
    {
        List<CSVDateBase> result;

        if (!isAsc)
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name ascending
                where data.Age.ToString() == InputText
                select data).ToList<CSVDateBase>();

        }
        else
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name descending
                where data.Age.ToString() == InputText
                select data).ToList<CSVDateBase>();
        }

        return result;
    }
    List<CSVDateBase> GenderFillter(string InputText)
    {
        List<CSVDateBase> result;

        if (!isAsc)
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name ascending
                where data.Gender.Contains(InputText)
                select data).ToList<CSVDateBase>();

        }
        else
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name descending
                where data.Gender.Contains(InputText)
                select data).ToList<CSVDateBase>();
        }

        return result;
    }
    List<CSVDateBase> HobbyFillter(string InputText)
    {
        List<CSVDateBase> result;

        if (!isAsc)
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name ascending
                where data.Hobby.Contains(InputText)
                select data).ToList<CSVDateBase>();

        }
        else
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name descending
                where data.Hobby.Contains(InputText)
                select data).ToList<CSVDateBase>();
        }

        return result;
    }
    List<CSVDateBase> JobFillter(string InputText)
    {
        List<CSVDateBase> result;

        if (!isAsc)
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name ascending
                where data.Job.Contains(InputText)
                select data).ToList<CSVDateBase>();

        }
        else
        {
            result =
               (from data in personData.AsEnumerable()
                orderby data.Name descending
                where data.Job.Contains(InputText)
                select data).ToList<CSVDateBase>();
        }

        return result;
    }

    List<CSVDateBase> sortDropDown(string InputText)
    {
      

        switch (dropdown.value)
        {
            case 0: // 이름
                currentData = NameFillter(InputText);
                Debug.Log("이름");
                break;
            case 1:// 성별
                currentData = GenderFillter(InputText);
                Debug.Log("성별");
                break;
            case 2:// 나이
                currentData = AgeFillter(InputText);
                break;
            case 3:// 취미 
                currentData = HobbyFillter(InputText);
                break;
            case 4:// 직업 
                currentData = JobFillter(InputText);
                break;
            default:
                inputField.text = ""; 
                break;
        }
        return currentData;

    }

    string InputFieldCheck()
    {
        string inputText = inputField.text;
       
        return inputText;
    }

    public void changeSituation()
    {
        isInitial = true;
    }
    
    
}
