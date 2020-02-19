# APIBackEnd

APIBackEnd é a API desenvolvida para o desafio da Ativa Investimentos, que retorna uma lista de fundos carregada previamente em um Banco de dados em memória e realiza movimentações de Aplicação e Retirada nesses fundos

## How to Start

Para inicializar o projeto, basta cloná-lo deste repositório com o seguinte comando :

```bash
git clone https://github.com/lucascmb/DesafioAPIBackEnd
```

Feito isso, Vá até o diretório desta solução e entre na pasta do projeto APIBackEnd, e execute o seguinte comando :

```bash
dotnet run
```

Para subir o container linux, basta rodar os seguintes comandos :

```bash
sudo docker build -t apibackend .
sudo docker run -d -p 4000:80 --name apibackend apibackend
```
Feito isso, basta acessar pelo navegador em localhost:4000

## Chamadas

As requisições estão bem documentadas no swagger junto à API.

## Testes

Foram incluidos testes unitários em um outro projeto junto à solução da API, para rodá-los, basta buildar os 2 projetos (a solução), ir no diretório Test que se encontra na raiz e digitar :

```bash
dotnet test
```
No caso de utilizar o Visual Studio, também é possível utilizar o gerenciador de testes, o qual permite uma depuração melhor no caso de falhas. Ao fim da execução, é gerado um diretorio (caso não haja) chamado TestResults, que guarda os resultados das execuções
