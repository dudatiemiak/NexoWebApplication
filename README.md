# NexoWebApplication

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
