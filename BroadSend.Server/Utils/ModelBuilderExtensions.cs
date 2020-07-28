using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BroadSend.Server.Utils
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>().HasData(
                new Director
                {
                    Id = 1,
                    Alias = "Ростовцева",
                    Name = "(Ростовцева) Чернявская  Светлана Германовна"
                },
                new Director
                {
                    Id = 2,
                    Alias = "Рябочкина",
                    Name = "(Рябочкина) Гололобова Ольга Алексеевна"
                },
                new Director
                {
                    Id = 3,
                    Alias = "Якушева",
                    Name = "(Якушева) Кайгородова Наталия Владимировна"
                },
                new Director
                {
                    Id = 4,
                    Alias = "Антипов",
                    Name = "Антипов Василий Владимирович"
                },
                new Director
                {
                    Id = 5,
                    Alias = "Бурсин",
                    Name = "Бурсин Кирилл Владимирович"
                },
                new Director
                {
                    Id = 6,
                    Alias = "Бурсина",
                    Name = "Бурсина Мария Владимировна"
                },
                new Director
                {
                    Id = 7,
                    Alias = "Игнатов",
                    Name = "Игнатов Сергей Юрьевич"
                },
                new Director
                {
                    Id = 8,
                    Alias = "Котов",
                    Name = "Котов Николай Викторович"
                },
                new Director
                {
                    Id = 9,
                    Alias = "Крылов",
                    Name = "Крылов Илья Сергеевич"
                },
                new Director
                {
                    Id = 10,
                    Alias = "Кузнецов",
                    Name = "Кузнецов Сергей Александрович"
                },
                new Director
                {
                    Id = 11,
                    Alias = "Кузьмина",
                    Name = "Кузьмина Наталья Алексеевна"
                },
                new Director
                {
                    Id = 12,
                    Alias = "Лелякова",
                    Name = "Лелякова  Марина Георгиевна"
                },
                new Director
                {
                    Id = 13,
                    Alias = "Селиванова",
                    Name = "Селиванова Наталья Викторовна"
                },
                new Director
                {
                    Id = 14,
                    Alias = "Смирнов",
                    Name = "Смирнов Александр Геннадиевич"
                },
                new Director
                {
                    Id = 15,
                    Alias = "Цернес",
                    Name = "Цернес Александр Владимирович"
                }
            );
        }
    }
}
