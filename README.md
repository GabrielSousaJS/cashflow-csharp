## Sobre o projeto

Esta API, desenvolvida utilizando **.NET 8**, adota os princípios do **Domain-Driven Design (DDD)** para oferecer uma solução estruturada e eficaz no gerencimaneto de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalahndo informações como título, data e hora, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em banco de dados **MySQL**.

A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é completada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentro os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de amnter. Por fim, o **EntityFramework** atua como um ORM (Object-Relational Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

### Features

- **Domain-Drive Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Teste de Unidade**: Teste abrangentes com FluentAssetions para garantir a funcionalidade e a qualidade.
- **Geração de Relatórios**: Capacidade de exportar relatórios detalhados em PDF e Excel, oferencendo uma análise visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por pate dos desenvolvedores.

## Getting started

Para obter uma cópia local funcionando siga esses passos simples.

### Requisitos

* Visual Studio versão 2022 ou Visual Studio Code
* Windows 10+ ou Linux/MacOS com o [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado
* MySql Server

### Instalação

1. Clone o repositório:

```sh
git clone https://github.com/GabrielSousaJS/cashflow-csharp.git
 ```

2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a API e aproveite o seu teste.