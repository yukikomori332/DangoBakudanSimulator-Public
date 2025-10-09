using NUnit.Framework;
using System;
using Project.Core.Scripts.Domain.GameData.Model;
using UniRx;

namespace Project.Core.Scripts.Tests.EditMode.Domain.GameData.Model
{
    public sealed class GameDataTest
    {
        private const int TestScore = 0;
        private const int TestSpecialScore = 0;
        private Project.Core.Scripts.Domain.GameData.Model.GameData _gameData;

        [SetUp]
        public void SetUp()
        {
            _gameData = new Project.Core.Scripts.Domain.GameData.Model.GameData();
        }

        [TearDown]
        public void TearDown()
        {
            _gameData.Dispose();
        }

        [Test]
        public void _初期状態_正しく設定されている()
        {
            // Assert
            Assert.That(_gameData.Score.Value, Is.EqualTo(TestScore));
            Assert.That(_gameData.SpecialScore.Value, Is.EqualTo(TestSpecialScore));
        }

        [Test]
        public void Score_正常系_値が正しく更新される()
        {
            // Arrange
            bool valueChanged = false;
            _gameData.Score.Subscribe(_ => valueChanged = true);

            // Act
            _gameData.Score.Value++;

            // Assert
            Assert.That(_gameData.Score.Value, Is.EqualTo(TestScore + 1));
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void SpecialScore_正常系_値が正しく更新される()
        {
            // Arrange
            bool valueChanged = false;
            _gameData.SpecialScore.Subscribe(_ => valueChanged = true);

            // Act
            _gameData.SpecialScore.Value++;

            // Assert
            Assert.That(_gameData.SpecialScore.Value, Is.EqualTo(TestSpecialScore + 1));
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void Dispose_正常系_リソースが正しく解放される()
        {
            // Arrange
            bool isDisposed = false;
            _gameData.Score.Subscribe(_ => { }, () => isDisposed = true);
            _gameData.SpecialScore.Subscribe(_ => { }, () => isDisposed = true);

            // Act
            _gameData.Dispose();

            // Assert
            Assert.That(isDisposed, Is.True);
        }
    }
}
