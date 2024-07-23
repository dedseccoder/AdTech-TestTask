using methodsMyJson;


string pathToJsonFile = "persons.json";
AppMethods method = new AppMethods();

switch(args[0]){
    case "-add":
    //ask not one, but all arguments to the method, maybe
        method.addPerson(args, pathToJsonFile);
        break;
    case "-get":
        method.getPerson(args, pathToJsonFile);
        break;
    case "-update":
        method.updatePerson(args, pathToJsonFile);
        break;
    case "-delete":
        method.deletePerson(args, pathToJsonFile);
        break;
    case "-getall":
        method.getAll(pathToJsonFile);
        break;
}