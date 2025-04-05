
namespace MedCore.Domain.Base
{
    public static class MethValidate
    {
        //public static bool IsNull(object obj)
        //{
        //    string mensaje = $"El campo {obj} no puede ser nulo.";

        //    LoggerAssist.Error(mensaje);
        //    return new OperationResult(bool=false,"gvhjshhfjk");

        //}
        public static bool IsNotNull(object obj)
        {
            return obj != null;
        }
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static bool IsNotEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        public static bool IsZero(int num)
        {
            return num == 0;
        }
        public static bool IsNotZero(int num)
        {
            return num != 0;
        }
        public static bool IsNegative(int num)
        {
            return num < 0;
        }
        public static bool IsPositive(int num)
        {
            return num > 0;
        }
        public static bool IsNegative(decimal num)
        {
            return num < 0;
        }
        public static bool IsPositive(decimal num)
        {
            return num > 0;
        }
        public static bool IsNegative(float num)
        {
            return num < 0;
        }
        public static bool IsPositive(float num)
        {
            return num > 0;
        }
        public static bool IsNegative(double num)
        {
            return num < 0;
        }
        public static bool IsPositive(double num)
        {
            return num > 0;
        }
        public static bool IsNegative(long num)
        {
            return num < 0;
        }
        public static bool IsPositive(long num)
        {
            return num > 0;
        }
        public static bool IsNegative(short num)
        {
            return num < 0;
        }
        public static bool IsPositive(short num)
        {
            return num > 0;
        }
        public static bool IsNegative(byte num)
        {
            return num < 0;
        }
        public static bool IsPositive(byte num)
        {
            return num > 0;
        }
        public static bool IsNegative(sbyte num)
        {
            return num < 0;
        }
        public static bool IsPositive(sbyte num)
        {
            return num > 0;
        }
        public static bool IsNegative(ushort num)
        {
            return num < 0;
        }
        public static bool IsPositive(ushort num)
        {
            return num > 0;
        }
        public static bool IsNegative(uint num)
        {
            return num < 0;
        }
    }
}
