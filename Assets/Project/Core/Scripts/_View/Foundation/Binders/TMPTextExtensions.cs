using System;
using UniRx;
using TMPro;

namespace Project.Core.Scripts.View.Foundation.Binders
{
    /// <summary>
    /// TextMeshProのテキストコンポーネントに対する拡張メソッドを提供するクラス
    /// Reactive Extensionsを使用して、Observableな値の変更をテキストに反映します
    /// </summary>
    public static class TMPTextExtensions
    {
        /// <summary>
        /// 文字列のObservableをTMP_Textのテキストにバインドします
        /// </summary>
        /// <param name="self">対象のTMP_Textコンポーネント</param>
        /// <param name="source">バインドする文字列のObservable</param>
        /// <returns>バインドの解除に使用するIDisposable</returns>
        public static IDisposable SetTextSource(this TMP_Text self, IObservable<string> source)
        {
            return source
                .Subscribe(x => { self.text = x; })
                .AddTo(self);
        }

        /// <summary>
        /// 整数値のObservableをTMP_Textのテキストにバインドします
        /// オプションで数値から文字列への変換関数を指定可能です
        /// </summary>
        /// <param name="self">対象のTMP_Textコンポーネント</param>
        /// <param name="source">バインドする整数値のObservable</param>
        /// <param name="converter">数値から文字列への変換関数（省略時はToString()を使用）</param>
        /// <returns>バインドの解除に使用するIDisposable</returns>
        public static IDisposable SetTextSource(this TMP_Text self, IObservable<int> source,
            Func<int, string> converter = null)
        {
            return source
                .Subscribe(x =>
                {
                    var text = converter == null ? x.ToString() : converter(x);
                    self.text = text;
                })
                .AddTo(self);
        }

        /// <summary>
        /// 浮動小数点値のObservableをTMP_Textのテキストにバインドします
        /// オプションで数値から文字列への変換関数を指定可能です
        /// </summary>
        /// <param name="self">対象のTMP_Textコンポーネント</param>
        /// <param name="source">バインドする浮動小数点値のObservable</param>
        /// <param name="converter">数値から文字列への変換関数（省略時はToString()を使用）</param>
        /// <returns>バインドの解除に使用するIDisposable</returns>
        public static IDisposable SetTextSource(this TMP_Text self, IObservable<float> source,
            Func<float, string> converter = null)
        {
            return source
                .Subscribe(x =>
                {
                    var text = converter == null ? x.ToString() : converter(x);
                    self.text = text;
                })
                .AddTo(self);
        }

        /// <summary>
        /// 浮動小数点値（0.0～1.0）のObservableをパーセンテージ表示のテキストにバインドします
        /// オプションで値から文字列への変換関数を指定可能です
        /// </summary>
        /// <param name="self">対象のTMP_Textコンポーネント</param>
        /// <param name="source">バインドする浮動小数点値のObservable（0.0～1.0）</param>
        /// <param name="converter">値から文字列への変換関数（省略時は100倍して整数に変換）</param>
        /// <returns>バインドの解除に使用するIDisposable</returns>
        public static IDisposable SetPercentageTextSource(this TMP_Text self, IObservable<float> source,
            Func<float, string> converter = null)
        {
            return source
                .Subscribe(x =>
                {
                    var text = converter == null ? ((x * 100)).ToString("F0") + "%" : converter(x);
                    self.text = text;
                })
                .AddTo(self);
        }
    }
}
