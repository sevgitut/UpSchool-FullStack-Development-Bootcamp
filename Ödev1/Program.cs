using Microsoft.VisualBasic;
using UpSchool_SimplePasswordGenerator;

var passwordGenerator = new PasswordGenerator();

passwordGenerator.ReadInputs();

passwordGenerator.Generate();

passwordGenerator.WriteLatestGeneratedPassword();

Console.ReadLine();

return 0;