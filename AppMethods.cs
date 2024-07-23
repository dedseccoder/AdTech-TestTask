using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Linq;

namespace methodsMyJson
{
    public class AppMethods
    {
        private string __ParseArgument(string[] args, string searchWord)
        {
            foreach(string arg in args){
                if(arg.Contains(searchWord)){
                    return arg.Substring(arg.LastIndexOf(":") + 1);
                }
            }
            return null;
        }
        
        public void addPerson(string[] args, string path)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            List<PersonObj> AllPeopleAdded = JsonConvert.DeserializeObject<List<PersonObj>>(json) ?? new List<PersonObj>();

            int Id = AllPeopleAdded.Count != 0 ? AllPeopleAdded.Select(i => i.Id).Max() + 1 : 0;
            string FirstName = __ParseArgument(args, "FirstName");
            string LastName = __ParseArgument(args, "LastName");
            decimal Salary = Convert.ToDecimal(__ParseArgument(args, "Salary"));

            AllPeopleAdded.Add(new PersonObj(Id, FirstName, LastName, Salary));
            using (StreamWriter file = File.CreateText(path)){
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, AllPeopleAdded);
            }
        }

        public void getPerson(string[] args, string path)
        {
            int Id = Convert.ToInt32(__ParseArgument(args, "Id"));

            string json = string.Empty;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            List<PersonObj> people = JsonConvert.DeserializeObject<List<PersonObj>>(json) ?? new List<PersonObj>();

            PersonObj person = people.FirstOrDefault(i => i.Id == Id);
            if(person == null){
                Console.WriteLine("Ther is no such id");
                return;
            }
            Console.WriteLine($"{person.Id} {person.FirstName} {person.LastName} {person.SalaryPerHour}");
        }

        public void updatePerson(string[] args, string path)
        {
            int Id = Convert.ToInt32(__ParseArgument(args, "Id"));
            string FirstName = __ParseArgument(args, "FirstName");
            string LastName = __ParseArgument(args, "LastName");
            decimal Salary = Convert.ToDecimal(__ParseArgument(args, "Salary"));

            string json = string.Empty;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            List<PersonObj> AllPeopleAdded = JsonConvert.DeserializeObject<List<PersonObj>>(json) ?? new List<PersonObj>();
            int personIndex = AllPeopleAdded.FindIndex(i => i.Id == Id);
            if(personIndex == -1){
                Console.WriteLine("Ther is no such id");
                return;
            }

            AllPeopleAdded[personIndex].FirstName = !string.IsNullOrEmpty(FirstName) ? FirstName : AllPeopleAdded[personIndex].FirstName;
            AllPeopleAdded[personIndex].LastName = !string.IsNullOrEmpty(LastName) ? LastName : AllPeopleAdded[personIndex].LastName;
            AllPeopleAdded[personIndex].SalaryPerHour = !string.IsNullOrEmpty(Salary.ToString()) && Salary != 0  ? Salary : AllPeopleAdded[personIndex].SalaryPerHour;

            using (StreamWriter file = File.CreateText(path)){
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, AllPeopleAdded);
            }
        }

        public void deletePerson(string[] args, string path)
        {
            int Id = Convert.ToInt32(__ParseArgument(args, "Id"));

            string json = string.Empty;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            List<PersonObj> AllPeopleAdded = JsonConvert.DeserializeObject<List<PersonObj>>(json) ?? new List<PersonObj>();
            int personIndex = AllPeopleAdded.FindIndex(i => i.Id == Id);
            if(personIndex == -1){
                Console.WriteLine("Ther is no such id");
                return;
            }
            AllPeopleAdded.RemoveAt(personIndex);

            using (StreamWriter file = File.CreateText(path)){
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, AllPeopleAdded);
            }
        }

        public void getAll(string path)
        {  
            string json = string.Empty;
            using (StreamReader r = new StreamReader(path))
                json = r.ReadToEnd();

            List<PersonObj> people = JsonConvert.DeserializeObject<List<PersonObj>>(json) ?? new List<PersonObj>();
            foreach(var person in people){
                Console.WriteLine($"{person.Id} {person.FirstName} {person.LastName} {person.SalaryPerHour}");
            }
        }
    }


    public class PersonObj
    {
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public decimal SalaryPerHour {get; set;}

        [JsonConstructor]
        public PersonObj(int id, string firstName, string lastName, decimal salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SalaryPerHour = salary;
        }
    }
}
