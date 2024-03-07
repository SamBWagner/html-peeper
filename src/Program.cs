string? fileContents = File.ReadAllText("./test.html");

Console.WriteLine("Analysing this HTML file...");
Console.WriteLine(fileContents);

if (string.IsNullOrWhiteSpace(fileContents)) {
  Console.WriteLine("File empty");
  return;
}

var elements = new List<string>();
List<(char, int)> htmlCharacters = new();

// Build our list of html element markers
for (int i = 0; i < fileContents.Length; i++)
{
  if (IsHtmlSpecialCharacter(fileContents[i])) {
    htmlCharacters.Add((fileContents[i], i));
  }
}

foreach (var item in htmlCharacters)
{
  System.Console.WriteLine($"found character: {item.Item1} at index: {item.Item2}");
}

// Extract our HTML tags using the element markers list
// TODO: This doesn't work right now!
bool nextCharSafe = false;
for (int i = 0; i < htmlCharacters.Count; i++)
{
  nextCharSafe = i != htmlCharacters.Count;

  Console.WriteLine($"html character under inspection: {htmlCharacters[i]}");
  if (htmlCharacters[i].Item1 == '<') {
    Console.WriteLine($"character was <");
    if (nextCharSafe && htmlCharacters[i + 1].Item1 == '>') {
      Console.WriteLine($"adding element to list: {ExtractHtmlTag(fileContents, i, htmlCharacters[i + 1].Item2)}");
      elements.Add(ExtractHtmlTag(fileContents, i, htmlCharacters[i + 1].Item2));
    }
  }
}

foreach(var item in elements) {
  Console.WriteLine($"element: {item}");
}

bool IsHtmlSpecialCharacter(char character){
  switch (character)
  {
    case '<':
    case '>':
    case '/':
      return true;
    default:
      return false;
  }
}

string ExtractHtmlTag(string htmlString, int start, int end) {
  start += 1;
  return htmlString[start..end];
}

