using System;


namespace GameGenerator.Core.Exceptions
{
    public class EntryUpdateErrorException : Exception
    {
        public EntryUpdateErrorException()
            : base()
        {
        }

        public EntryUpdateErrorException(string message)
            : base(message)
        {
        }

        public EntryUpdateErrorException(string message, Exception innerException)
            : base(message)
        {
        }
    }
}
