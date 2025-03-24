
using Bogus;
using Rotas.Domain.Entities;

namespace Tests.Domain.Entities
{
    internal static class DeslocamentoEntityFactory
    {
        internal static List<Deslocamento> FakeList(int qtd = 1)
        {
            var faker = new Faker<Deslocamento>()
                .RuleFor(v => v.Origem, f => f.Lorem.Letter(3))
                .RuleFor(v => v.Destino, f => f.Lorem.Letter(4))
                .RuleFor(v => v.Custo, f => f.Random.Decimal(1, 999));

            var list = new List<Deslocamento>();
            for (int i = 0; i < qtd; i++)
                list.Add(faker.Generate());

            return list;
        }
    }
}