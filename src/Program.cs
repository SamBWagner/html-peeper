if (args.Length == 0)
{
    Console.WriteLine("No file path provided!");
    return;
}
else if (args.Length > 1)
{
    Console.WriteLine("Too many arguments! Provide only one file path.");
    return;
}

var path = args[0];

var fileExists = File.Exists(path);

if (!fileExists)
{
    Console.WriteLine($"File does not exist: {path}");
    return;
}

var fileContents = File.ReadAllText(path);

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

