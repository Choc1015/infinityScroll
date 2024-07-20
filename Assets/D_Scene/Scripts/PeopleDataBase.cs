using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[SerializeField]
public class CSVDateBase
{
    public int Id;  // ���� ID 
    public string Name;
    public int Age;
    public string Gender;
    public string Hobby;
    public string Job;


}

public class PeopleDataBase : MonoBehaviour
{
    private List<string> surnames = new List<string> { "��", "��", "��", "��", "��", "����", "����", "��", "��", "��" ,"��","Ȳ��" ,"��", "Ź"};
    private List<string> givenNames = new List<string> { "����", "����", "����", "����", "����", "����", "��ö", "��ȣ", "����", "�ÿ�" ,"����"};
    private List<string> genders = new List<string> { "����", "����", "���� ����" };
    private List<string> hobbies = new List<string> { "�̼�", "����", "�౸", "����", "�˵�", "�״Ͻ�", "�뷡", "��", "����", "����" };
    private List<string> jobs = new List<string> { "��ȹ/QA", "�׷���/����", "���α׷���" };
    private List<CSVDateBase> people;

    const string filePath = "Assets/StreamingAssets/people.csv";


    void Awake()
    {
        people = GenerateRandomPeople(500);
        string csvData = ConvertListToCSV(people);
        SaveCSVWithBOM(filePath, csvData);
        Debug.Log("CSV ���� ���� ��ġ: " + filePath);

    }

    List<CSVDateBase> GenerateRandomPeople(int count)
    {
        List<CSVDateBase> people = new List<CSVDateBase>();
        for (int i = 0; i < count; i++)
        {
            CSVDateBase person = new CSVDateBase
            {
                Id = i,  // ���� ID �Ҵ�
                Name = GenerateRandomName(),
                Age = Random.Range(20, 61),
                Gender = genders[Random.Range(0, genders.Count)],
                Hobby = hobbies[Random.Range(0, hobbies.Count)],
                Job = jobs[Random.Range(0, jobs.Count)] 
            };
            people.Add(person);
        }
        return people;
    }

    string GenerateRandomName()
    {
        string surname = surnames[Random.Range(0, surnames.Count)];
        string givenName = givenNames[Random.Range(0, givenNames.Count)];
        return surname + givenName;
    }

    string ConvertListToCSV(List<CSVDateBase> people)
    {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Id,Name,Age,Gender,Hobby,Job");
        foreach (var person in people)
        {
            csv.AppendLine($"{person.Id},{person.Name},{person.Age},{person.Gender},{person.Hobby},{person.Job}");
        }
        return csv.ToString();
    }

    void SaveCSVWithBOM(string filePath, string csvData)
    {
        byte[] bom = new byte[] { 0xEF, 0xBB, 0xBF };
        byte[] csvBytes = Encoding.UTF8.GetBytes(csvData);
        byte[] csvWithBom = new byte[bom.Length + csvBytes.Length];
        System.Buffer.BlockCopy(bom, 0, csvWithBom, 0, bom.Length);
        System.Buffer.BlockCopy(csvBytes, 0, csvWithBom, bom.Length, csvBytes.Length);
        File.WriteAllBytes(filePath, csvWithBom);
        
    }

}

public class CSVReader
{
    public static List<CSVDateBase> ReadPeopleFromCSV(string filePath)
    {
        var people = new List<CSVDateBase>();
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++) // ù ��° ���� ����̹Ƿ� �ǳ�
        {
            var fields = lines[i].Split(',');
            if (fields.Length == 6)
            {
                var readData = new CSVDateBase
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1],
                    Age = int.Parse(fields[2]),
                    Gender = fields[3],
                    Hobby = fields[4],
                    Job = fields[5]
                };
                people.Add(readData);
            }
        }

        return people;
    }
}
