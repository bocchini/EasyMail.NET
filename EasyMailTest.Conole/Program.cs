using EasyMail.NET;



var emailAdress = "";

Console.Write("Password: ");
var password = Console.ReadLine() ?? string.Empty;

var message = new EasyMailService();

var result = await message.SendAsync(emailAdress, password);
Console.WriteLine(result);


message.From("Américo Vespúcio", "americo@exemplo.com");
message.To("Cristóvão Colombo", "albocchini@gmail.com");
message.Subject("Teste de email");
message.Body("Olá, este é um email de teste enviado usando a EasyMail.NET!");


