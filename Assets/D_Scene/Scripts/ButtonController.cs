using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject popup;
    public GameObject Content;
    public Text nameIndex;
    public Scrollbar scrollbar;
    public GameObject scoll;
    
    private List<GameObject> buttonObject = new List<GameObject>();
    private List<CSVDateBase> sortData;
    int cons = 0;
    float time = 0;

    private void Start()
    {
        sortData = SortData.instance.currentData;
        optimizationButtonCount();
       
    }
    private void Update()
    {
        sortData = SortData.instance.currentData;
        Debug.Log(buttonObject.Count);
        OnScoll();
        if (SortData.instance.isInitial == true)
        {

            Initialized();
        }
        
            optimizationButtonCount();
        
    }

    private void OnScoll()
    {
        if (!scoll.activeSelf)
        {

            scrollbar.value = 0;
        }
    }

    void InstantiateButtons()
    {
        
        //for (int i = 0; i < sortData.Count; i++)
        //{
           
        //        GameObject par = Instantiate(buttonPrefab);
        //        buttonObject.Add(par);
        //        par.transform.SetParent(Content.transform, false);
        //        int id = sortData[i].Id;
        //        par.name = id.ToString();
        //        par.GetComponentInChildren<Text>().text = (i + 1).ToString() + ". " + sortData[i].Name;
                
        //        //par.SetActive(false);
                
        //        // 버튼 클릭 이벤트 
        //        Button button = par.GetComponent<Button>();
        //        button.onClick.AddListener(() => OnButtonClick(id));
                
        //}
       
    }
    void destroyObject()
    {
        for (int i = 0; i < buttonObject.Count; i++) 
        {
            Destroy(buttonObject[i].gameObject);
        }
          
    }

    void Initialized()
    {

         destroyObject();
        cons = 0;
        buttonObject.Clear();       
        InstantiateButtons();
        SortData.instance.isInitial = false;    
          
    }

    public void OnButtonClick(int id)
    {
        Debug.Log("버튼 클릭됨: ID " + id);  
        popup.SetActive(true);

        CSVDateBase person = sortData.Find(x => x.Id == id);

        if (person != null)
        {

            string message = $"이름: {person.Name} \n나이: {person.Age} \n성별: {person.Gender} \n취미: {person.Hobby} \n파트: {person.Job}";
            nameIndex.text = message;
        }
    }

    public void OnCancel()
    {
        popup.SetActive(false);
    }

    
    void optimizationButtonCount()
    {
        float delay = 0.3f;

        time += Time.deltaTime;
        if (time >= delay)
         {
            if (scrollbar.value <= 0.01)
            {
                if (sortData.Count == 0)
                    return;
                for (int i = cons; i < 4 + cons; i++)
                {
                    
                    //buttonObject[i].SetActive(true);
                    instantiatePrefab(i);
                    if (sortData.Count - 1 == i)
                        return;
                }   
                
                cons += 4;
            }
                
            time = 0;
        }
            
                // scrollbar.value
                // scrollRect.verticalNormalizedPosition
          
    }

    void instantiatePrefab(int i)
    {


        GameObject par = Instantiate(buttonPrefab);
        buttonObject.Add(par);
        par.transform.SetParent(Content.transform, false);
        int id = sortData[i].Id;
        par.name = id.ToString();
        par.GetComponentInChildren<Text>().text = (i + 1).ToString() + ". " + sortData[i].Name;

        //par.SetActive(false);

        // 버튼 클릭 이벤트 
        Button button = par.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(id));
    }

    
}
