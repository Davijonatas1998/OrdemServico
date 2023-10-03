# OrdemServico

## Configuração da Conexão com o Banco de Dados
Este guia descreve como configurar a conexão com o banco de dados para a aplicação "OrdemServico". Para que a aplicação funcione corretamente, você precisará acessar o arquivo `appsettings.json` e alterar o nome da string de conexão do banco de dados.

### Passo 1: Localize o arquivo `appsettings.json`
O arquivo `appsettings.json` contém as configurações da sua aplicação, incluindo a configuração de conexão com o banco de dados. Ele geralmente está localizado na raiz do seu projeto.

{
  "ConnectionStrings": {
    "DefaultConnection": "SuaStringDeConexaoAqui"
  },
  // Outras configurações da aplicação...
}

### Passo 2: Encontre a seção ConnectionStrings
Dentro do arquivo appsettings.json, localize a seção chamada ConnectionStrings. Ela deve se parecer com o exemplo acima.

### Passo 3: Altere a string de conexão
Dentro da seção ConnectionStrings, você verá uma chave chamada "DefaultConnection" e o valor associado a ela é a string de conexão atual com o banco de dados. Substitua esse valor pela string de conexão do seu banco de dados.

### Exemplo:

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=NomeDoServidor;Database=NomeDoBancoDeDados;User=Usuario;Password=Senha;"
  },
  // Outras configurações da aplicação...
}
Substitua "Server=NomeDoServidor;Database=NomeDoBancoDeDados;User=Usuario;Password=Senha;" pela sua própria string de conexão.

### Passo 4: Salve as alterações
Após realizar a alteração, salve o arquivo appsettings.json.

### Passo 5: Atualize o Banco de Dados
Execute o comando update-database no console do gerenciador de pacotes ou terminal para aplicar as alterações de migração ao banco de dados.

bash
Copy code
Update-Database

### Passo 6: Execute a aplicação
Agora você deve ser capaz de executar a aplicação "OrdemServico" com a nova configuração de conexão com o banco de dados.
