using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[SerializeField]
public class CSVDateBase
{
    public int Id;  // 고유 ID 
    public string Name;
    public int Age;
    public string Gender;
    public string Hobby;
    public string Job;


}

public class PeopleDataBase : MonoBehaviour
{
    private List<string> surnames = new List<string> { "김", "이", "박", "최", "정", "독고", "남궁", "조", "강", "안" ,"신","황보" ,"윤", "탁"};
    private List<string> givenNames = new List<string> { "민지", "서연", "지훈", "유진", "윤택", "현우", "상철", "지호", "현승", "시우" ,"영진"};
    private List<string> genders = new List<string> { "남자", "여자", "선택 안함" };
    private List<string> hobbies = new List<string> { "미술", "독서", "축구", "수영", "검도", "테니스", "노래", "춤", "연기", "조립" };
    private List<string> jobs = new List<string> { "기획/QA", "그래픽/영상", "프로그래머" };
    private List<CSVDateBase> people;

    const string filePath = "Assets/StreamingAssets/people.csv";


    void Awake()
    {
        people = GenerateRandomPeople(500);
        string csvData = ConvertListToCSV(people);
        SaveCSVWithBOM(filePath, csvData);
        Debug.Log("CSV 파일 생성 위치: " + filePath);

    }

    List<CSVDateBase> GenerateRandomPeople(int count)
    {
        List<CSVDateBase> people = new List<CSVDateBase>();
        for (int i = 0; i < count; i++)
        {
            CSVDateBase person = new CSVDateBase
            {
                Id = i,  // 고유 ID 할당
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

        for (int i = 1; i < lines.Length; i++) // 첫 번째 줄은 헤더이므로 건너
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
