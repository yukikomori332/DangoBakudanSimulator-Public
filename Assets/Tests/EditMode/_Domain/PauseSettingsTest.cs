using NUnit.Framework;
using System;
using Project.Core.Scripts.Domain.Setting.Model;
using UniRx;

namespace Project.Core.Scripts.Tests.EditMode.Domain.Settings.Model
{
    public sealed class PauseSettingsTest
    {
        private PauseSettings _pauseSettings;

        [SetUp]
        public void SetUp()
        {
            _pauseSettings = new PauseSettings();
        }

        [TearDown]
        public void TearDown()
        {
            _pauseSettings.Dispose();
        }

        [Test]
        public void _初期状態_ポーズが無効になっている()
        {
            // Assert
            Assert.That(_pauseSettings.Paused, Is.False);
        }

        [Test]
        public void SetValue_正常系_値が正しく設定される()
        {
            // Arrange
            bool expectedValue = true;
            bool valueChanged = false;

            _pauseSettings.ValueChanged.Subscribe(_ => valueChanged = true);

            // Act
            _pauseSettings.SetValue(expectedValue);

            // Assert
            Assert.That(_pauseSettings.Paused, Is.EqualTo(expectedValue));
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void SetValue_正常系_値が変更されるたびにイベントが発火する()
        {
            // Arrange
            int eventCount = 0;
            _pauseSettings.ValueChanged.Subscribe(_ => eventCount++);

            // Act
            _pauseSettings.SetValue(true);
            _pauseSettings.SetValue(false);
            _pauseSettings.SetValue(true);

            // Assert
            Assert.That(eventCount, Is.EqualTo(3));
        }

        [Test]
        public void SetValues_正常系_変更された値と変更される前の値が同じ場合でもイベントが発火される()
        {
            // Arrange
            bool value = false;
            bool valueChanged = false;

            _pauseSettings.SetValue(value);
            _pauseSettings.ValueChanged.Subscribe(_ => valueChanged = true);

            // Act
            _pauseSettings.SetValue(value);

            // Assert
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void Dispose_正常系_リソースが正しく解放される()
        {
            // Act
            _pauseSettings.Dispose();

            // Assert
            Assert.That(() =>
            {
                _pauseSettings.SetValue(true);
            }, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void SetValue_異常系_大量のイベント購読時の動作()
        {
            // Arrange
            int eventCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                _pauseSettings.ValueChanged.Subscribe(_ => eventCount++);
            }

            // Act
            _pauseSettings.SetValue(true);

            // Assert
            Assert.That(eventCount, Is.EqualTo(1000));
        }
    }
}
