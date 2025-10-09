using NUnit.Framework;
using System;
using Project.Core.Scripts.Domain.Setting.Model;
using UniRx;

namespace Project.Core.Scripts.Tests.EditMode.Domain.Settings.Model
{
    public sealed class SoundSettingsTest
    {
        private SoundSettings _soundSettings;

        [SetUp]
        public void SetUp()
        {
            _soundSettings = new SoundSettings();
        }

        [TearDown]
        public void TearDown()
        {
            _soundSettings.Dispose();
        }

        [Test]
        public void SetValues_正常系_値が正しく設定される()
        {
            // Arrange
            float expectedVolume = 0.5f;
            bool expectedMuted = true;
            bool valueChanged = false;

            _soundSettings.ValueChanged.Subscribe(_ => valueChanged = true);

            // Act
            _soundSettings.SetValues(expectedVolume, expectedMuted);

            // Assert
            Assert.That(_soundSettings.Volume, Is.EqualTo(expectedVolume));
            Assert.That(_soundSettings.Muted, Is.EqualTo(expectedMuted));
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void SetValues_正常系_変更された値と変更される前の値が同じ場合でもイベントが発火される()
        {
            // Arrange
            float volume = 0.5f;
            bool muted = true;
            bool valueChanged = false;

            _soundSettings.SetValues(volume, muted);
            _soundSettings.ValueChanged.Subscribe(_ => valueChanged = true);

            // Act
            _soundSettings.SetValues(volume, muted);

            // Assert
            Assert.That(valueChanged, Is.True);
        }

        [Test]
        public void Dispose_正常系_リソースが正しく解放される()
        {
            // Act
            _soundSettings.Dispose();

            // Assert
            Assert.That(() =>
            {
                _soundSettings.SetValues(0.5f, true);
            }, Throws.TypeOf<ObjectDisposedException>());
        }

        [Test]
        public void SetValues_異常系_Volumeが範囲外の値を設定する()
        {
            // Arrange
            float invalidVolume = 1.5f;
            bool muted = false;

            // Act & Assert
            Assert.DoesNotThrow(() => _soundSettings.SetValues(invalidVolume, muted));
            Assert.That(_soundSettings.Volume, Is.EqualTo(invalidVolume));
        }
    }
} 