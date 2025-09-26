<h1 align="center">⚔️🐉 Game of Thrones API</h1>

<p align="center">
  API REST para gerenciar <b>Personagens</b>, <b>Casas</b> e <b>Fortalezas</b> do universo de <i>Game of Thrones</i>.<br>
  Construída com <b>.NET 8</b>, <b>ASP.NET Core</b> e <b>Entity Framework Core</b>.
</p>

<p align="center">
  <a href="http://localhost:5283/swagger"><b>Swagger (local)</b></a>
</p>

---

## ✨ Funcionalidades

- CRUD de **Characters**, **Houses** e **Strongholds**
- Uso de **DTOs** para evitar ciclos de referência
- Dados iniciais via **Seed** (Stark, Lannister, Targaryen, Winterfell etc.)
- Documentação automática com **Swagger**
- Estrutura pronta para **autenticação JWT**

---

## 🏗️ Estrutura de Pastas

GameOfThronesAPI/ <br>
│── Controllers/ # Endpoints <br>
│── DTOs/ # Data Transfer Objects <br>
│── Models/ # Entidades <br>
│── Data/ # DbContext e Seed <br>
│── Program.cs <br>
│── appsettings.json <br>

---

## 🗃️ Modelos

### Character
- `Id`, `Nome`, `Titulo`, `Status`, `Descricao`, `Sexo`
- `HouseId`, `NovaCasaId`, `FortalezaId`

### House
- `Id`, `Nome`, `Idade`, `Lema`
- `StrongholdId`
- `Vassalos` (outras casas subordinadas)

### Stronghold
- `Id`, `Nome`, `Localizacao`, `Descricao`
- `HouseId` (casa dominante)
- `Residents` (personagens)

---

## 🔌 Endpoints Principais

### Characters
- `GET /api/characters`
- `GET /api/characters/{id}`
- `POST /api/characters`
- `PUT /api/characters/{id}`
- `DELETE /api/characters/{id}`

### Houses
- `GET /api/houses`
- `GET /api/houses/{id}`
- `POST /api/houses`
- `PUT /api/houses/{id}`
- `DELETE /api/houses/{id}`

### Strongholds
- `GET /api/strongholds`
- `GET /api/strongholds/{id}`
- `POST /api/strongholds`
- `PUT /api/strongholds/{id}`
- `DELETE /api/strongholds/{id}`

---

## 🧪 Exemplos de Requisição

### Criar Personagem
```bash
curl -X POST "http://localhost:5283/api/Characters" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Arya Stark",
    "titulo": "No One",
    "status": "Vivo",
    "descricao": "Filha mais nova de Eddard Stark e Catelyn Tully.",
    "sexo": "Feminino",
    "houseId": 1,
    "fortalezaId": 1
  }'
  ```
Criar Casa

```bash
Copiar código
curl -X POST "http://localhost:5283/api/Houses" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Baratheon",
    "idade": 300,
    "lema": "Ours is the Fury",
    "strongholdId": null
  }'
  ```
Criar Fortaleza
```bash
Copiar código
curl -X POST "http://localhost:5283/api/Strongholds" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Storm'\''s End",
    "localizacao": "Stormlands",
    "descricao": "Fortaleza ancestral da Casa Baratheon.",
    "houseId": 4
  }'
  ```
⚠️ Sempre envie apenas IDs (houseId, fortalezaId, novaCasaId) em POST/PUT.
O GET retorna DTOs já resolvendo nomes (Casa = "Stark", Fortaleza = "Winterfell").

⚙️ Como Rodar o Projeto
Pré-requisitos
.NET 8 SDK

Instalar pacotes
```bash
Copiar código
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```
Configuração do Banco (appsettings.json)
json
Copiar código
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gameofthrones.db"
  }
}
```
Criar Banco
bash
Copiar código
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
Rodar a API
bash
Copiar código
dotnet run
Swagger: 👉 http://localhost:5283/swagger
```

🧰 DTOs
A API retorna DTOs de leitura para evitar ciclos e simplificar resposta.

Exemplo CharacterDto:

csharp
Copiar código
```bash
public class CharacterDto {
  public int Id { get; set; }
  public string Nome { get; set; } = string.Empty;
  public string? Titulo { get; set; }
  public string Status { get; set; } = "Vivo";
  public string Sexo { get; set; } = "Masculino";
  public string? Descricao { get; set; }
  public string? Casa { get; set; }
  public string? NovaCasa { get; set; }
  public string? Fortaleza { get; set; }
}
```
🔐 Futuro: Autenticação JWT
Estrutura pronta para adicionar:

AuthController (login/register)

User model

AuthService + TokenService

Configuração em Helpers/JwtSettings.cs

📄 Licença
Livre para uso educacional.
Inspirado no universo criado por George R. R. Martin.
