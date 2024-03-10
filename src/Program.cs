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

// Extract our HTML tags using the element markers list
bool nextHtmlCharSafe = false;
for (int i = 0; i < htmlCharacters.Count; i++)
{
  nextHtmlCharSafe = i != htmlCharacters.Count;

  if (htmlCharacters[i].Item1 == '<') {
    if (nextHtmlCharSafe && htmlCharacters[i + 1].Item1 == '>') {
      var start = htmlCharacters[i].Item2 + 1;
      var end = htmlCharacters[i+1].Item2;
      elements.Add(fileContents[start..end]);
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

