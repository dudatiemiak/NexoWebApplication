# NexoWebApplication

## Tecnologias Utilizadas
- .NET 8 (ASP.NET Core)
- Entity Framework Core (Oracle)
- JWT (JSON Web Token) para autenticação
- OpenTelemetry para logging e tracing
- xUnit para testes automatizados
- Docker para containerização e deploy
- Render para hospedagem cloud
- Clean Architecture (separação de camadas)

## Relação com o tema "O Futuro do Trabalho"
Esta solução foi desenvolvida para abordar o tema "O Futuro do Trabalho" ao oferecer uma API moderna, escalável e segura para gestão de perfis profissionais e descrições de competências. A aplicação permite:
- Cadastro, atualização e consulta de clientes e suas descrições profissionais.
- Integração com banco de dados Oracle, simulando cenários reais de grandes empresas.
- Autenticação JWT, garantindo segurança e controle de acesso.
- Observabilidade e monitoramento, essenciais para ambientes corporativos modernos.
- Práticas REST, versionamento e HATEOAS, facilitando integração com sistemas diversos e adaptabilidade para novas demandas do mercado de trabalho.

A arquitetura e as tecnologias escolhidas refletem tendências do futuro do trabalho: cloud, automação, segurança, interoperabilidade e facilidade de evolução.

## Versionamento da API
Todos os endpoints seguem o padrão `/api/v1/` para garantir versionamento e evolução segura da API.

## Endpoints principais

### Autenticação
- `POST /api/v1/auth/login` — Realiza login e retorna JWT.
- `GET /api/v1/auth/dados-protegidos` — Endpoint protegido, requer JWT.

### Cliente
- `GET /api/v1/cliente?page=1&pageSize=10` — Lista clientes (paginado, com HATEOAS).
- `GET /api/v1/cliente/{id}` — Busca cliente por ID.
- `PUT /api/v1/cliente/{id}` — Atualiza cliente.
- `DELETE /api/v1/cliente/{id}` — Remove cliente.

### Cadastro
- `POST /api/v1/register` — Cria novo cliente.

### Descrição do Perfil
- `GET /api/v1/descricaocliente?page=1&pageSize=10` — Lista descrições (paginado, com HATEOAS).
- `GET /api/v1/descricaocliente/{id}` — Busca descrição por ID.
- `GET /api/v1/descricaocliente/cliente/{clienteId}` — Lista descrições de um cliente.
- `POST /api/v1/descricaocliente` — Cria nova descrição.
- `PUT /api/v1/descricaocliente/{id}` — Atualiza descrição.
- `DELETE /api/v1/descricaocliente/{id}` — Remove descrição.

### Health Check
- `GET /health` — Verifica status da API.

## Testes
- Testes unitários e de integração implementados com xUnit.

## Observabilidade
- Logging e tracing configurados via OpenTelemetry.

## Observações
- Todos os endpoints protegidos exigem JWT no header `Authorization: Bearer {token}`.
- Paginação e links HATEOAS disponíveis nas listagens.
- API pronta para evoluir com novas versões e recursos.

---

## Deploy no Render
A aplicação está publicada em: [https://nexowebapplication.onrender.com](https://nexowebapplication.onrender.com)

## Como testar a API no Postman

1. **Autenticação (obter JWT):**
   - Método: POST
   - URL: `https://nexowebapplication.onrender.com/api/v1/auth/login`
   - Body (JSON):
     ```json
     {
       "email": "seu@email.com",
       "senha": "sua_senha"
     }
     ```
   - O token JWT será retornado no campo `token`.

2. **Usar JWT nos endpoints protegidos:**
   - Adicione o header:
     ```
     Authorization: Bearer <seu_token_jwt>
     ```

3. **Exemplo de requisição para cadastrar cliente:**
   - Método: POST
   - URL: `https://nexowebapplication.onrender.com/api/v1/register`
   - Body (JSON):
     ```json
     {
       "nome": "Heloisa",
       "email": "helo@email.com",
       "senha": "123456",
       "descricoes": []
     }
     ```

4. **Exemplo de requisição para editar cliente:**
   - Método: PUT
   - URL: `https://nexowebapplication.onrender.com/api/v1/cliente/5`
   - Body (JSON):
     ```json
     {
       "id": 5,
       "nome": "Heloisa",
       "email": "helo@email.com",
       "senha": "123456",
       "descricoes": []
     }
     ```

5. **Exemplo de requisição para editar descrição de cliente:**
   - Método: PUT
   - URL: `https://nexowebapplication.onrender.com/api/v1/descricaocliente/2`
   - Body (JSON):
     ```json
     {
       "id": 2,
       "clienteId": 1,
       "areaEstudo": "Saúde",
       "ocupacaoAtual": "Farmaceutica",
       "anosExperiencia": 5,
       "satisfacao": 8,
       "adocaoTecnologia": 8,
       "interesseMudar": false
     }
     ```

6. **Health Check:**
   - Método: GET
   - URL: `https://nexowebapplication.onrender.com/health`

## Como executar os testes

1. Instale o .NET SDK 8.0 ou superior.
2. No terminal, navegue até a raiz do projeto.
3. Execute:
   ```sh
   dotnet test
   ```
4. Os resultados dos testes serão exibidos no terminal.

## Como rodar localmente

1. Instale o .NET SDK 8.0 ou superior.
2. No terminal, navegue até a pasta do projeto.
3. Execute:
   ```sh
   dotnet run --project NexoWebApplication/NexoWebApplication.csproj
   ```
4. Por padrão, a aplicação roda na porta **5236**.
5. Acesse no navegador ou Postman:
   - [http://localhost:5236/swagger/index.html]
Se precisar alterar a porta, edite o arquivo `appsettings.json`.

## Observações adicionais
- Todos os exemplos acima podem ser importados diretamente no Postman.
- Certifique-se de obter o JWT antes de acessar endpoints protegidos.
- O Dockerfile está pronto para deploy em ambientes como Render.
