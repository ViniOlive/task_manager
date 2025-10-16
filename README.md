TaskManager - Guia de Execução

Um sistema completo de gerenciamento de tarefas, desenvolvido com .NET 9 (ASP.NET Core) no backend, React no frontend e SQL Server como banco de dados.
O projeto está totalmente containerizado com Docker, permitindo executar tudo com um único comando.

------------------------------------------------------------
🚀 Tecnologias Utilizadas

Backend (.NET 9)
- ASP.NET Core Web API
- Entity Framework Core 9
- Clean Architecture (camadas: Domain, Application, Infrastructure, Api)
- Validações de negócio:
  - Título obrigatório e único
  - Data de conclusão não pode ser anterior à data de criação
- Documentação via Swagger

Frontend (React)
- Create React App
- React Router DOM
- Axios para comunicação com a API
- Interface simples para listar, criar, editar, excluir e filtrar tarefas

Banco de Dados
- SQL Server 2022 (rodando em container Docker)

Containerização
- Docker e Docker Compose
- Containers para: Banco de dados, API .NET e Frontend React

------------------------------------------------------------
📂 Estrutura do Projeto

- task-manager/
- │
- ├── backend/
- │   ├── TaskManager.Api/           # API principal (Controllers e configuração)
- │   ├── TaskManager.Application/   # Regras de negócio e casos de uso
- │   ├── TaskManager.Domain/        # Entidades e contratos
- │   ├── TaskManager.Infrastructure/# Repositórios e persistência com EF Core
- │   └── Dockerfile                 # Dockerfile do backend (.NET 9)
- │
- ├── frontend/
- │   ├── src/                       # Código React
- │   ├── public/
- │   └── Dockerfile                 # Dockerfile do frontend (React + Nginx)
- │
- └── docker-compose.yml             # Orquestra todos os serviços

------------------------------------------------------------
⚙️ Como Executar o Projeto

Pré-requisitos:
- Docker e Docker Compose instalados
- Portas 1433, 8080 e 3000 livres

Passos:
1. Clone o repositório
   git clone https://github.com/seu-usuario/task-manager.git
   cd task-manager

2. Construa e suba os containers
   docker compose up -d --build

3. Verifique se os containers estão rodando
   docker ps

4. Acesse os serviços:
   - Frontend: http://localhost:3000
   - API (Swagger): http://localhost:8080/swagger
   - Banco de Dados: localhost,1433 (usuário: sa, senha: Nova_Senha_Forte!456)

------------------------------------------------------------
🧪 Testando a API

Use o Swagger (http://localhost:8080/swagger) para testar endpoints.
Ou use a interface React:
- Crie novas tarefas
- Edite e exclua tarefas
- Filtre por status (Pendente, EmProgresso, Concluida)

------------------------------------------------------------
🧰 Comandos Úteis

- docker compose up -d      -> Sobe os containers
- docker compose down       -> Para os containers
- docker compose down -v    -> Remove containers e volumes (zera o banco)
- docker logs -f taskmanager_api   -> Exibe logs da API
- docker exec -it taskmanager_db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Your_password123"  -> Acessa o SQL Server

------------------------------------------------------------
🧩 Estrutura das Entidades

TaskItem
- Id: int — Identificador da tarefa
- Titulo: string — Título da tarefa (único e obrigatório)
- Descricao: string — Descrição opcional
- Status: string — Pendente, EmProgresso, ou Concluida
- DataCriacao: datetime — Gerada automaticamente
- DataConclusao: datetime? — Opcional, mas não pode ser anterior à criação

------------------------------------------------------------
🧠 Regras de Negócio Implementadas

- Não é permitido cadastrar ou atualizar uma tarefa com título duplicado.
- É permitido que DataConclusao seja igual à DataCriacao, mas nunca anterior.
- DataCriacao é sempre definida automaticamente pelo servidor.

------------------------------------------------------------
💡 Estrutura Arquitetural

- Domain: Entidades e interfaces base
- Application: Casos de uso e validações
- Infrastructure: Implementação de repositórios e EF Core
- API: Controllers e configuração de injeção de dependência
