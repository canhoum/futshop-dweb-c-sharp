namespace futshop_dweb.Data
{
    /// <summary>
    /// Interface com serviços de controlo da aplicação
    /// </summary>
    public interface IUserService
    {
        bool IsAuthenticated { get; }

        bool IsAdmin { get; }

        bool addedToCart { get; }

        bool noItems { get; }

        void rmCart();

        void removeCart(int id);

        bool finishedOrder { get; }

        void resetFinishedOrder();

        int getUserID();
    }
}
