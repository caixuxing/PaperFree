namespace Trasen.PaperFree.Domain.Common.Abstract
{
    public interface IHandler<T>
    {
        bool HandleRequest(T data);

        IHandler<T> SetNext(IHandler<T> handler);
    }

    public class ValidationHandler<T> : IHandler<T>
    {
        private Func<T, bool> _validationFunc;
        private IHandler<T> _nextHandler;

        public ValidationHandler(Func<T, bool> validationFunc)
        {
            _validationFunc = validationFunc;
        }

        public bool HandleRequest(T data)
        {
            if (!_validationFunc(data))
            {
                Console.WriteLine("Validation failed.");
                return false;
            }

            if (_nextHandler != null)
            {
                return _nextHandler.HandleRequest(data);
            }

            return true;
        }

        public IHandler<T> SetNext(IHandler<T> handler)
        {
            _nextHandler = handler;
            return handler;
        }
    }

    public class Client
    {
        public void Main()
        {
            IHandler<string> validationHandler = new ValidationHandler<string>(ggg)
                .SetNext(new ValidationHandler<string>(gggTT))
                .SetNext(new ValidationHandler<string>(gggTT));
            bool result = validationHandler.HandleRequest("user@example.com");
            Console.WriteLine("Validation result: " + result);
        }

        public bool ggg(string tt)
        {
            return true;
        }

        public bool gggTT(string tt)
        {
            return true;
        }
    }
}