var fileStream = new FileStream("./test.html", FileMode.Open);
var streamReader = new StreamReader(fileStream);
var fileContents = streamReader.ReadToEnd();

Console.WriteLine(fileContents);
