using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CompClub
{
    /// <summary>
    /// Набор WinAPI функций, как есть (Или почти как есть)
    /// </summary>
    public static class WinAPI
    {

        /// <summary>
        ///  Устанавливает состояние показа определяемого окна.
        ///  Если функция завершилась успешно, возвращается значение
        ///  отличное от нуля. Если функция потерпела неудачу,
        ///  возвращаемое значение - ноль.
        /// </summary>
        /// <param name="hWnd">Дескриптор окна</param>
        /// <param name="nCmdShow">Определяет, как окно должно быть показано.</param>
        /// <returns>
        ///  Если функция завершилась успешно, возвращается значение
        ///  отличное от нуля. Если функция потерпела неудачу,
        ///  возвращаемое значение - ноль.
        ///  </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        ///  Устанавливает состояние показа определяемого окна.
        ///  Если функция завершилась успешно, возвращается значение
        ///  отличное от нуля. Если функция потерпела неудачу,
        ///  возвращаемое значение - ноль.
        /// </summary>
        /// <param name="hWnd">Дескриптор окна</param>
        /// <param name="nCmdShow">Определяет, как окно должно быть показано.</param>
        /// <returns>
        ///  Если функция завершилась успешно, возвращается значение
        ///  отличное от нуля. Если функция потерпела неудачу,
        ///  возвращаемое значение - ноль.
        ///  </returns>
        public static bool ShowWindow(IntPtr hWnd, Consts.SHOWWINDOW nCmdShow)
        {
            return ShowWindow(hWnd, (int)nCmdShow);
        }

        /// <summary>
        /// Установить окно на передний план
        /// </summary>
        /// <param name="hWnd">Handle окна</param>
        /// <returns>Удачность</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        /// <summary>
        /// Набор констант
        /// </summary>
        public static class Consts
        {

            /// <summary>
            /// Параметры к функции ShowWindow. 
            /// Внимание! Некоторые параметры имеют одинаковое значение
            /// (Почему? За ответом к дяде Биллу)
            /// </summary>
            public enum SHOWWINDOW : uint
            {
                /// <summary>
                /// Скрывает окно и активизирует другое окно
                /// </summary>
                SW_HIDE = 0,
                /// <summary>
                /// Активизирует и отображает окно.
                /// Если окно свернуто или развернуто,
                /// Windows восстанавливает его в 
                /// первоначальном размере и позиции. 
                /// Прикладная программа должна установить 
                /// этот флажок при отображении окна впервые
                /// </summary>
                SW_SHOWNORMAL = 1,
                SW_NORMAL = 1,
                /// <summary>
                /// Активизирует окно и отображает его как свернутое окно
                /// </summary>
                SW_SHOWMINIMIZED = 2,
                /// <summary>
                /// Активизирует окно и отображает его как развернутое окно
                /// </summary>
                SW_SHOWMAXIMIZED = 3,
                /// <summary>
                /// Развертывает определяемое окно
                /// </summary>
                SW_MAXIMIZE = 3,
                /// <summary>
                /// Отображает окно в его самом современном размере и позиции. 
                /// Активное окно остается активным
                /// </summary>
                SW_SHOWNOACTIVATE = 4,
                /// <summary>
                /// Активизирует окно и отображает его текущие размеры и позицию
                /// </summary>
                SW_SHOW = 5,
                /// <summary>
                /// Свертывает определяемое окно и активизирует следующее окно 
                /// верхнего уровня в Z-последовательности
                /// </summary>
                SW_MINIMIZE = 6,
                /// <summary>
                /// Отображает окно как свернутое окно. Активное окно остается активным
                /// </summary>
                SW_SHOWMINNOACTIVE = 7,
                /// <summary>
                /// Отображает окно в его текущем состоянии. Активное окно остается активным
                /// </summary>
                SW_SHOWNA = 8,
                /// <summary>
                /// Активизирует и отображает окно. 
                /// Если окно свернуто или развернуто, 
                /// Windows восстанавливает в его первоначальных 
                /// размерах и позиции. Прикладная программа должна 
                /// установить этот флажок при восстановлении свернутого окна
                /// </summary>
                SW_RESTORE = 9,
                /// <summary>
                /// Устанавливает состояние показа, основанное на флажке SW_
                /// , определенном в структуре STARTUPINFO, 
                /// переданной в функцию CreateProcess программой, 
                /// которая запустила прикладную программу
                /// </summary>
                SW_SHOWDEFAULT = 10,
                /// <summary>
                /// Windows 2000/XP: Свертывает окно, даже если поток,
                /// который владеет окном, зависает. Этот флажок должен 
                /// быть использоваться только при свертывании окон 
                /// другого потока
                /// </summary>
                SW_FORCEMINIMIZE = 11,
                SW_MAX = 11,
            }
        }
    }
}
