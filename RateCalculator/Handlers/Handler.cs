using RateCalculator.Models;

namespace RateCalculator.Handlers
{
    public interface IHandler
    {
        void HandleRequest(QuoteModel quote);

        void SetSuccessor(IHandler successor);
    }

    public abstract class Handler
    {
        protected IHandler successor;

        public void SetSuccessor(IHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(QuoteModel request);
    }
}
