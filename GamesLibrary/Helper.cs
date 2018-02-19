using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GamesLibrary
{
    static class Helper
    {
        /// <summary>
        /// Указывает на то, является ли заданная строка пустой строке или содержащей пробелы.
        /// </summary>
        /// <param name="s">Строка для проверки.</param>
        /// <returns></returns>
        public static bool StringIsEmpty(this string s) => string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        public static string GetTime(int time)
        {
            if (time <= 0)
                return null;
            TimeSpan ts = TimeSpan.FromSeconds(time);

            string h = GetDeclension(ts.Hours, "час", "часа", "часов");
            string m = GetDeclension(ts.Minutes, "минута", "минуты", "минут");
            string s = GetDeclension(ts.Seconds, "секунда", "секунды", "секунд");

            List<string> times = new List<string>();

            if (ts.Hours > 0)
                times.Add($"{ts.Hours} {h}");
            if (ts.Minutes > 0)
                times.Add($"{ts.Minutes} {m}");
            if (ts.Seconds > 0)
                times.Add($"{ts.Seconds} {s}");

            StringBuilder res = new StringBuilder();
            foreach (var item in times)
            {
                if (res.Length > 0)
                    res.Append(" ");
                res.Append(item);
            }

            return res.ToString();
        }

        /// <summary>
        /// Возвращает слова в падеже, зависимом от заданного числа.
        /// </summary>
        /// <param name="number">Число от которого зависит выбранное слово</param>
        /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
        /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
        /// <param name="plural">Множественное число слова. Например "дней"</param>
        /// <returns></returns>
        public static string GetDeclension(int number, string nominativ, string genetiv, string plural)
        {
            number = number % 100;
            if (number >= 11 && number <= 19)
                return plural;

            var i = number % 10;
            switch (i)
            {
                case 1:
                    return nominativ;
                case 2:
                case 3:
                case 4:
                    return genetiv;
                default:
                    return plural;
            }

        }

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Icon to ImageSource.
        /// </summary>
        /// <returns></returns>
        public static ImageSource ToImageSource(this Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
                throw new Win32Exception();

            return wpfBitmap;
        }
    }
}
