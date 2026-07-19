using CraftyHookers.UserService.Infrastructure.Security;

namespace CraftyHookers.UserService.Infrastructure.Tests.Security
{
    public class PasswordServiceTests
    {
        private readonly PasswordService _sut = new();

        [Fact]
        public void Hash_ReturnsValueDifferentFromRawPassword()
        {
            var hash = _sut.Hash("correct horse battery staple");

            Assert.NotEqual("correct horse battery staple", hash);
        }

        [Fact]
        public void Hash_ProducesDifferentOutputForSamePasswordOnEachCall()
        {
            var first = _sut.Hash("correct horse battery staple");
            var second = _sut.Hash("correct horse battery staple");

            Assert.NotEqual(first, second);
        }

        [Fact]
        public void Verify_ReturnsTrue_WhenPasswordMatchesItsOwnHash()
        {
            var hash = _sut.Hash("correct horse battery staple");

            Assert.True(_sut.Verify("correct horse battery staple", hash));
        }

        [Fact]
        public void Verify_ReturnsFalse_WhenPasswordDoesNotMatch()
        {
            var hash = _sut.Hash("correct horse battery staple");

            Assert.False(_sut.Verify("wrong password", hash));
        }

        [Fact]
        public void Verify_ReturnsFalse_WhenStoredHashIsNotValidBase64()
        {
            Assert.False(_sut.Verify("correct horse battery staple", "not-base64!!"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("QQ==")]
        public void Verify_ReturnsFalse_WhenStoredHashIsWrongLength(string malformedHash)
        {
            Assert.False(_sut.Verify("correct horse battery staple", malformedHash));
        }

        [Fact]
        public void Generate_DefaultsToTwelveCharacters()
        {
            var password = _sut.Generate();

            Assert.Equal(12, password.Length);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        [InlineData(64)]
        public void Generate_ReturnsRequestedLength(int length)
        {
            var password = _sut.Generate(length);

            Assert.Equal(length, password.Length);
        }

        [Fact]
        public void Generate_ProducesDifferentValuesAcrossCalls()
        {
            var first = _sut.Generate(20);
            var second = _sut.Generate(20);

            Assert.NotEqual(first, second);
        }

        [Fact]
        public void Generate_OnlyUsesCharactersFromTheAllowedAlphabet()
        {
            const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";

            var password = _sut.Generate(200);

            Assert.All(password, c => Assert.Contains(c, allowedCharacters));
        }
    }
}
