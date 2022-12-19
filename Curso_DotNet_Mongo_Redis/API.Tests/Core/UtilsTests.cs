using API.Core;
using Xunit;

namespace API.Tests.Infra
{
    public class UtilsTests
    {
        [Fact]
        public void Should_Return_Validate_Slug()
        {
            //Arrange
            var title = "Fim de ano da Band traz programas especiais, filmes e shows exclusivos";

            //Act
            var slug = Utils.GenerateSlug(title);

            //Assert
            Assert.Equal("fim-de-ano-da-band-traz-programas-especiais-filmes-e-shows-exclusivos", slug);
        }
    }
}
