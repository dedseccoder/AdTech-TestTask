using Xunit;
using System;
using System.IO;
using methodsMyJson;

namespace JsonMethoid.Tests;

public class UnitTest1
{
    [Fact]
    public void testGetMethod1()
    {
        string JsonFile = "persons.json";

        using (File.Create(JsonFile));
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        
        AppMethods method = new AppMethods();

        string[] addArgs = {"FirstName:Json", "LastName:Todd", "Salary:200"};
        method.addPerson(addArgs, JsonFile);
        
        string[] args = {"Id:0"};
        method.getPerson(args, JsonFile);

        var temp = "0 Json Todd 200,0\n";
        Assert.Equal(temp, writer.ToString());   
    }   

    [Fact]
    public void testGetMethod2()
    {
        string JsonFile = "persons.json";

        using (File.Create(JsonFile));
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        
        AppMethods method = new AppMethods();

        string[] addArgs = {"FirstName:Json", "LastName:Todd", "Salary:200"};
        method.addPerson(addArgs, JsonFile);
        
        string[] args = {"Id:13235"};
        method.getPerson(args, JsonFile);

        var temp = "Ther is no such id\n";
        Assert.Equal(temp, writer.ToString());   
    }
}