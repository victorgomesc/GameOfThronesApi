using GameOfThronesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOfThronesAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Garante que o banco está criado
            context.Database.Migrate();

            // Evita duplicar seed se já existir dados
            if (context.Houses.Any() || context.Strongholds.Any() || context.Characters.Any())
            {
                return;
            }

            // Criar fortalezas
            var winterfell = new Stronghold
            {
                Nome = "Winterfell",
                Localizacao = "The North",
                Descricao = "Antiga sede dos Reis do Inverno e lar da Casa Stark."
            };

            var casterlyRock = new Stronghold
            {
                Nome = "Casterly Rock",
                Localizacao = "Westerlands",
                Descricao = "Fortaleza dos Lannister, construída sobre uma mina de ouro."
            };

            var dragonstone = new Stronghold
            {
                Nome = "Dragonstone",
                Localizacao = "Blackwater Bay",
                Descricao = "Fortaleza ancestral dos Targaryen."
            };

            // Criar casas
            var stark = new House
            {
                Nome = "Stark",
                Idade = 8000,
                Lema = "Winter is Coming",
                Stronghold = winterfell
            };

            var lannister = new House
            {
                Nome = "Lannister",
                Idade = 6000,
                Lema = "Hear Me Roar",
                Stronghold = casterlyRock
            };

            var targaryen = new House
            {
                Nome = "Targaryen",
                Idade = 300,
                Lema = "Fire and Blood",
                Stronghold = dragonstone
            };

            // Criar personagens
            var ned = new Character
            {
                Nome = "Eddard Stark",
                Titulo = "Warden of the North",
                Status = "Morto",
                Descricao = "Lorde de Winterfell e pai de Robb, Sansa, Arya, Bran e Rickon.",
                Sexo = "Masculino",
                House = stark,
                Fortaleza = winterfell
            };

            var jon = new Character
            {
                Nome = "Jon Snow",
                Titulo = "Lord Commander of the Night's Watch",
                Status = "Vivo",
                Descricao = "Filho bastardo de Eddard Stark, criado em Winterfell.",
                Sexo = "Masculino",
                House = stark,
                Fortaleza = winterfell
            };

            var cersei = new Character
            {
                Nome = "Cersei Lannister",
                Titulo = "Queen of the Seven Kingdoms",
                Status = "Morta",
                Descricao = "Filha de Tywin Lannister, irmã gêmea de Jaime.",
                Sexo = "Feminino",
                House = lannister,
                Fortaleza = casterlyRock
            };

            var daenerys = new Character
            {
                Nome = "Daenerys Targaryen",
                Titulo = "Mother of Dragons",
                Status = "Morta",
                Descricao = "Última Targaryen sobrevivente após Robert's Rebellion.",
                Sexo = "Feminino",
                House = targaryen,
                Fortaleza = dragonstone
            };

            // Adicionar ao contexto
            context.Houses.AddRange(stark, lannister, targaryen);
            context.Strongholds.AddRange(winterfell, casterlyRock, dragonstone);
            context.Characters.AddRange(ned, jon, cersei, daenerys);

            // Salvar mudanças
            context.SaveChanges();
        }
    }
}
