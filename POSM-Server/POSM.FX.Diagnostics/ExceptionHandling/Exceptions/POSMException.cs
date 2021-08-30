namespace POSM.FX.Diagnostics.ExceptionHandling.Exceptions
{
    [Serializable]
    public class POSMException : Exception
    {
        public string Code { get; }

        public POSMException()
        {
        }

        public POSMException(POSMExceptionCodeEnum code, string message) : base(message)
        {
            Code = code.ToString();
        }
    }
}
