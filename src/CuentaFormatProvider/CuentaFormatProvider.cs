using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentaFormatProvider: ICuentaFormatProvider
    {
        private string[] levels;
        private int totalLenght;

        public CuentaFormatProvider(IOptions<CuentaOptions> options)
        {
            LoadFormat(options.Value);
        }

        public string GetLevelMask(int level)
        {
            return "".PadLeft(levels[level].Length, '0');
        }

        public int Levels { get { return levels.Length; }  }
        public int GetLevelWidth(int level)
        {
            return levels[level].Length;
        }

        public int GetLevelOf(string numeroCuenta)
        {
            if (!IsValid(numeroCuenta))
                throw new NumeroCuentaNoValidaException(numeroCuenta, nameof(numeroCuenta));

            var intArray = ToIntArray(numeroCuenta);

            for (int i = intArray.Length -1; i > 0; i--)
            {
                if (intArray[i] != 0)
                    return i;
            }

            return 0;
        }

        public string GetParent(string numeroCuenta)
        {
            string parent = null;

            int level = GetLevelOf(numeroCuenta);
            if (level == 0)
                throw new NumeroCuentaNoHijaException(numeroCuenta, nameof(numeroCuenta));
            

            var intArray = ToIntArray(numeroCuenta);

            for (int i = 0; i < intArray.Length ;  i++)
            {
                if (i < level)
                {
                    parent += intArray[i].ToString(GetLevelMask(i));
                }
                else
                {
                    parent += GetLevelMask(i);
                }
            }

            return parent;
        }

        public bool IsValid(string numeroCuenta)
        {
            var cuenta = RemoveFormat(numeroCuenta);

            if (cuenta.Length != totalLenght)
                return false;

            try
            {
                var r = ToIntArray(cuenta);

                if (r[0] == 0)
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string RemoveFormat(string numeroCuenta)
        {
            return numeroCuenta.Replace("-", "");
        }

        public string AddFormat(string numeroCuenta)
        {
            if (numeroCuenta.Contains('-'))
                return numeroCuenta;

            int anterior = 0;
            string result = null;

            for (int i = 0; i < levels.Length; i++)
            {
                result+= numeroCuenta.Substring(anterior, levels[i].Length);
                if (i < levels.Length - 1)
                    result += "-";
                anterior += levels[i].Length;
            }

            return result;

        }
        private int[] ToIntArray(string numeroCuenta)
        {
            numeroCuenta = RemoveFormat(numeroCuenta);

            int anterior = 0;
            int[] result = new int[levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                result[i] = int.Parse(numeroCuenta.Substring(anterior, levels[i].Length));
                anterior += levels[i].Length;
            }

            return result;
        }

        private void LoadFormat(CuentaOptions value)
        {
            levels = value.Mask.Split('-');

            totalLenght = 0;
            for (int i = 0; i < levels.Length; i++)
            {
                totalLenght += levels[i].Length;
            }
        }
        
        public string GetPrefixAccountFromParent(string idAccount)
        {
            var level = GetLevelOf(idAccount);
            var formated = AddFormat(idAccount);
            var splits = formated.Split('-');
            var stringBuilder = new StringBuilder(string.Empty);
            for (int i = 0; i <= level; i++)
            {
                stringBuilder.Append($"{splits[i]}");
            }
            return stringBuilder.ToString();
        }
        
        public string SetFormatedPrefixAccountFromParent(string idAccount)
        {
            var level = GetLevelOf(idAccount);
            var formated = AddFormat(idAccount);
            var splits = formated.Split('-');
            var stringBuilder = new StringBuilder(string.Empty);
            for (int i = 0; i <= level; i++)
            {
                stringBuilder.Append($"{splits[i]}-");
            }
            return stringBuilder.ToString();
        }
        
        public string SetFormatedSufixAccountFromParent(string idAccount)
        {
            var levelNewAccount = GetLevelOf(idAccount) + 1;
            var formated = AddFormat(idAccount);
            var splits = formated.Split('-');
            var stringBuilder = new StringBuilder(string.Empty);
            for (int i = levelNewAccount + 1; i < 7; i++)
            {
                stringBuilder.Append($"-{splits[i]}");
            }
            return stringBuilder.ToString();
        }
    }
}
