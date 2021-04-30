using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Volo.Abp;

namespace Programarcr.Contabilidad
{
    [DebuggerStepThrough]
    public  static class Validacion
    {
        public static T NotNull<T>(T value, [InvokerParameterName][NotNull] string parameterName)
        {
            return Check.NotNull<T>(value, parameterName);
        }

        public static string NotNull(
            string value,
            [InvokerParameterName][NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            return Check.NotNull(value, parameterName, maxLength, minLength);
        }

        public static string NotNullOrWhiteSpace(
            string value,
            [InvokerParameterName][NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            return Check.NotNullOrWhiteSpace(value, parameterName, maxLength, minLength);
        }

        public static string NotNullOrEmpty(
            string value,
            [InvokerParameterName][NotNull] string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            return Check.NotNullOrEmpty(value, parameterName, maxLength, minLength);
        }

        public static T NotNullOrZero<T>(T value, [InvokerParameterName][NotNull] string parameterName)
        {
            T r = Check.NotNull(value, parameterName);

            if(EqualityComparer<T>.Default.Equals(r))
            {
                throw new ArgumentException(parameterName + " can not be null or zero!", parameterName);
            }

            return r;
        }

        public static T Positive<T>(T value, [InvokerParameterName][NotNull] string parameterName)
        {
            value = Check.NotNull(value, parameterName);

            if(Comparer<T>.Default.Compare(value, default(T)) != 1 )
            {
                throw new ArgumentException(parameterName + " can not be null, negative or zero!", parameterName);
            }

            return value;
        }

    }
}
