*EasyMail.NET*:

# 📬 EasyMail.NET

License: MIT
*Sending emails in .NET has never been so simple.*
Stop struggling with complex SMTP configurations and cumbersome libraries. With EasyMail, you send professional emails in seconds.

![Status dos Tests](https://github.com/bocchini/EasyMail.NET/actions/workflows/dotnet-tests.yml/badge.svg)

## ✨ Why EasyMail?

* *Zero Configuration:*
Start sending with just a few lines of code.

* *Fluent API:* 
Readable and elegant syntax that developers love.

* *Provider Agnostic:* Switch from SMTP to SendGrid or other providers by simply changing one setting.

* *Performance:* Built on MailKit for maximum reliability and speed.

## 🚀 Installation
Install via NuGet Package Manager:
bash dotnet add package EasyMail.NET

## 🛠️ How to Use
### 1. Initial Configuration
In your Program.cs or Startup.cs, add EasyMail:
Importing the Extensions class
using EasyMail.NET.Extensions;

And adding the extension passing the configuration;
builder.Services.AddConfiguration(builder.Configuration);

Adding the service settings to Appsettings.json

```
 "EasyMail": {
    "Configuration": {
      "Username": "yourUsername",
      "Password": "yourPassword",
      "Host": "yourHostEmail",
      "Port": 587,
      "EnableSSL": true
    }
  }
```

### 2. Sending your first email
Inject IEasyMail into your service or controller and use the fluent syntax:

csharp
public class CupomController : ControllerBase
{
    private readonly IEasyMail _mail;

    public CupomController(IEasyMail mail) => _mail = mail;

    [HttpPost]
    public async Task<IActionResult> EnviarCupom([FromServices] IEasyMailService service, [FromBody] string emailCliente)
    {
      var emailFrom = "market@email.com";
      var nameFrom = "Market";

       var message = EmailBuilder
        .Create()
        .AddToInformation(emailCliente, "Nome do Cliente")
        .AddFromInformation(emailFrom, nameFrom)
        .AddSubject("Seu Cupom de Desconto Chegou! 🎁")
        .AddBody("<h1>Olá!</h1><p>Use o código <strong>PRIMEIRACOMPRA</strong> para 10% OFF.</p>", true)
        .Build();

      var result = await service.SendEmail(message);
      return Ok(result);
       
    }
}

## 📄 License
Distributed under the MIT License. See LICENSE for more information.

## 🤝 Contribute
Found a bug or have a feature idea? Feel free to open an Issue or submit a Pull Request.

### Made with ❤️ for .NET developers who value their time.

*EasyMail.NET*:
# 📬 EasyMail.NET

License: MIT
*O envio de e-mails em .NET nunca foi tão simples.* 
Pare de lutar com configurações complexas de SMTP e bibliotecas pesadas. Com o EasyMail, você envia e-mails profissionais em segundos.
## ✨ Por que EasyMail?
 * *Zero Configuração:* 
 Comece a enviar com poucas linhas de código.
 * *Fluent API:* 
 Sintaxe legível e elegante que os desenvolvedores amam.
 * *Provider Agnostic:* 
 Mude de SMTP para SendGrid ou outros provedores apenas trocando uma configuração.
 * *Performance:* Construído sobre o MailKit para máxima confiabilidade e velocidade.
## 🚀 Instalação
Instale via NuGet Package Manager:
bash
dotnet add package EasyMail.NET


## 🛠️ Como Usar
### 1. Configuração Inicial
No seu Program.cs ou Startup.cs, adicione o EasyMail:
Importando a classe Extensions

using EasyMail.NET.Extensions;

E adicionando o extension passando a configuração;
builder.Services.AddConfiguration(builder.Configuration);

Adicionando as configurações do serviço no Appsettings.json
```
 "EasyMail": {
    "Configuration": {
      "Username": "yourUsername",
      "Password": "yourPassword",
      "Host": "yourHostEmail",
      "Port": 587,
      "EnableSSL": true
    }
  }
```

### 2. Enviando seu primeiro e-mail
Injete o IEasyMail em seu serviço ou controller e use a sintaxe fluente:
csharp
public class CupomController : ControllerBase
{
    private readonly IEasyMail _mail;

    public CupomController(IEasyMail mail) => _mail = mail;

    [HttpPost]
    public async Task<IActionResult> EnviarCupom([FromServices] IEasyMailService service, [FromBody] string emailCliente)
    {
      var emailFrom = "market@email.com";
      var nameFrom = "Market";

       var message = EmailBuilder
        .Create()
        .AddToInformation(emailCliente, "Nome do Cliente")
        .AddFromInformation(emailFrom, nameFrom)
        .AddSubject("Seu Cupom de Desconto Chegou! 🎁")
        .AddBody("<h1>Olá!</h1><p>Use o código <strong>PRIMEIRACOMPRA</strong> para 10% OFF.</p>", true)
        .Build();

      var result = await service.SendEmail(message);
      return Ok(result);
       
    }
}
## 📄 Licença
Distribuído sob a licença MIT. Veja LICENSE para mais informações.
## 🤝 Contribua
Achou um bug ou tem uma ideia de feature? Sinta-se à vontade para abrir uma Issue ou enviar um Pull Request.
### Feito com ❤️ para desenvolvedores .NET que valorizam seu tempo.