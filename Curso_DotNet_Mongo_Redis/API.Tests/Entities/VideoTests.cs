﻿using API.Core;
using API.Entities;
using API.Entities.Enums;
using Xunit;

namespace API.Tests.Entities
{
    public class VideoTests
    {
        [Fact]
        public void News_Validate_Title_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Entretenimento",
                 "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21), às 22h30, o público acompanha o MasterChef Especial de Natal com a presença de vários famosos. Na primeira prova, Adriana Birolli e Toni Garrido enfrentam Negra Li e Felipe Titto. A dupla que fizer o melhor hambúrguer com acompanhamento e molho vence a disputa.\n\nNo segundo desafio, as gêmeas nadadoras Bia e Branca Feres encaram os gêmeos lutadores Rodrigo Minotauro e Rogério Minotouro. Os irmãos terão de preparar receitas natalinas de família.\n\n",
                 "Da Redação",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: EStatus.Active));

            //Assert
            Assert.Equal("O título deve ter até 90 caracteres!", result.Message);
        }


        [Fact]
        public void News_Validate_Hat_Lenght()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",
                 "Da Redação",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: EStatus.Active));

            //Assert
            Assert.Equal("O chapéu deve ter até 40 caracteres!", result.Message);
        }


        [Fact]
        public void News_Validate_Title_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 "Entretenimento",
                 string.Empty,
                 "Da Redação",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: EStatus.Active));

            //Assert
            Assert.Equal("O título não pode estar vazio!", result.Message);
        }


        [Fact]
        public void News_Validate_Hat_Empty()
        {
            //Arrange & Act
            var result = Assert.Throws<DomainException>(() => new Video(
                 string.Empty,
                 "Fim de ano da Band traz programas especiais, filmes e shows exclusivos",                 
                 "Da Redação",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.webp",
                 "http://localhost:5005/imgs/f168c0e0-790a-4247-934e-1f9d32bf4a5e.mp4",
                 status: EStatus.Active));

            //Assert
            Assert.Equal("O chapéu não pode estar vazio!", result.Message);
        }
    }
}
