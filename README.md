# Setup
---
Para rodar as aplicações será necessário a instalação do .netcore 3.1 disponível no link abaixo:
https://dotnet.microsoft.com/download

Siga os passos abaixo para executar o projeto ItauIti.Challenge.Api utilizando dotnet CLI.
1. Abra um command prompt e entre na pasta raiz do projeto api **password-validator-api**;
2. Execute o comando ```dotnet restore ```
3. Ao finalizar o comando anterior, execute ```dotnet build ItauIti.Challenge.sln ```
4. Para cria a dll de publicação ```dotnet publish ItauIti.Challenge.sln -o publish```
5. Entre na pasta publish ```cd publish ```
6. Execute o comando para executar a aplicação ```dotnet ItauIti.Challenge.Api.dll```

Caso deseje visualizar os endpoints disponíveis e como utiliza-los, basta abrir em seu navegador a ```"http://api_url_base/swagger"```, no exemplo abaixo:
http://localhost:5050/swagger

# Definições do projeto
---

### ItauIti.Challenge.PasswordValidate
---
Este projeto é responsável por criar e manter as funções para validar strings. Toda validação é baseada em uma função com uma entrada do tipo string e retorno do tipo boolean.
ex.: 
``` c# 
Func<string, bool> func = (string input) => string.isNullOrEmpty(input);
```

A entidade **ConfigurationPasswordValidator** é responsável por receber todas as configurações necessárias para criar o serviço de validação.
Abaixo, um exemplo de uso do ConfigurationPasswordValidator para criação de uma instância de validação:

``` dotnet
var config = new ConfigurationPasswordValidator(); 
config.AddShouldHaveLowercaseValidation();
```

Com a instância de **ConfigurationPasswordValidator** você poderá adicionar quantas validações seja necessário. Abaixo, estão a descrição de todas as funções disponíveis:

| Method | Description |
| ------ | ---------- |
| AddShouldHaveLowercaseValidation() | Método responsável por adicionar a validação que verifica se há, ao menos uma, letra **minúscula** |
| AddShouldHaveUppercaseValidation() | Método que adiciona a validação que verifica se há, ao menos uma, letra **maiúscula** |
| AddShouldHaveMinimumLengthValidation(int minimumLength) | Adiciona a função que verifica se uma determinada string possui a quantidade de caracteres informada |
| AddShouldHaveNumberValidation() | Método que adiciona a função que valida a existência de números em determinada string |
| AddShouldHaveSpecialCharacterValidation() | Este método adiciona a função que valida a existência de caracteres especiais |
| AddShouldNotRepeatCharactersValidation() | Adiciona a validação de caracteres repetidos |
| AddCustomValidator(Func<string, bool> customValidator) | Este método tem a finalidade de extender as funções padrões para qualquer validação desejada, utilizando com parâmetro de entrada uma função delegate |
| Initiaze() | Retorna uma instância de **IPasswordValidator** com todas as validações adicionadas pelo usuário |

#### IPasswordValidator
Interface responsável por representar o motor de validador de strings que consiste em executar todas as validações criadas e retorna um boolean indicando se um password é válido ou não. Abaixo um exemplo de uso da interface e de sua respectiva instância:

``` dotnet
var config = new ConfigurationPasswordValidator(); 
config.AddShouldHaveLowercaseValidation();
var passwordValidate = config.Initiaze();
passwordValidate.Validate("PASSwORD");
```

Abaixo a lista de funções da interface:

| Method | Description |
| ------ | ---------- |
| Validate(string password) | Método responsável por executar todas as validações configuradas e informa se a determinada senha cumpre ou não com requisitos minímos. |

### ItauIti.Challenge.PasswordValidate.DependencyInjection
---
Este projeto tem como finalidade prover um método de extensão para utilizar a interface IPasswordValidator de modo injetável. Por padrão, este método adiciona uma instância com lifecycle Singleton.

Como usar:

``` c#
services.AddPasswordValidation(config =>
            {
                config.AddShouldHaveLowercaseValidation();
                config.AddShouldHaveUppercaseValidation();
                config.AddShouldHaveNumberValidation();
                config.AddShouldHaveSpecialCharacterValidation();
                config.AddShouldHaveMinimumLengthValidation(9);
                config.AddShouldNotRepeatCharactersValidation();
                config.AddCustomValidator((string input) => !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input));
            });
```